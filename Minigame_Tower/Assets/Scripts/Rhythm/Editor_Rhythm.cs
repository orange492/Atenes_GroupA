using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.EventSystems;
using System.IO;
using System.Linq;

public class Editor_Rhythm : MonoBehaviour //, IPointerDownHandler
{
    [SerializeField]
    GameObject Mouse;
    [SerializeField]
    Transform tMap;
    [SerializeField]
    GameObject pBlock;
    [SerializeField]
    TextMeshProUGUI bpmTxt;
    [SerializeField]
    TextMeshProUGUI startTimeTxt;
    [SerializeField]
    Slider slider;

    public List<NoteBlock_rhythm>[] noteBlocks;
    public Dictionary<int, int>[] NoteDic;

    Button[] musicButton;
    //GameObject oLine;
    public bool isPlay { get; set; }
    bool isDrag;
    bool isMaking;
    int[] makingIndex;
    float scrollingSpeed;
    float startX;
    float startMouseX;

    private void Awake()
    {
        makingIndex = new int[2];
        NoteDic = new Dictionary<int, int>[2];
        NoteDic[0] = new Dictionary<int, int>();
        NoteDic[1] = new Dictionary<int, int>();
        noteBlocks = new List<NoteBlock_rhythm>[2];
        noteBlocks[0] = new List<NoteBlock_rhythm>();
        noteBlocks[1] = new List<NoteBlock_rhythm>();
        scrollingSpeed = Manager_Rhythm.SPEED;
        isPlay = false;
        isDrag = false;
        isMaking = false;
        musicButton = new Button[3];
        musicButton[0] = transform.Find("PlayBtn").GetComponent<Button>();
        musicButton[0].onClick.AddListener(PlayButton);
        musicButton[1] = transform.Find("ResetBtn").GetComponent<Button>();
        musicButton[1].onClick.AddListener(ResetButton);
        musicButton[2] = transform.Find("SubmitBtn").GetComponent<Button>();
        musicButton[2].onClick.AddListener(ConfirmButton);
        //BgButton.onClick.AddListener(BGClick);
    }

    private void Start()
    {
        LoadGameData();
    }

    //public void OnPointerDown(PointerEventData eventData)
    //{
    //    // 슬라이더의 조절 영역을 상대적 좌표로 계산합니다.
    //    float relativePosition = eventData.position.x / slider.GetComponent<RectTransform>().sizeDelta.x;
    //
    //    // 슬라이더의 값을 상대적 좌표에 해당하도록 조절합니다.
    //    slider.value = relativePosition;
    //}

    public void OnPointerDown(PointerEventData eventData)
    {
        //Sound_Rhythum.Inst.PauseMusic();
        // 클릭한 위치에 해당하는 수치로 슬라이더의 값을 설정합니다.
        //slider.value = eventData.position.x / Screen.width;
        //Sound_Rhythum.Inst.ChangeTime(slider.value * Sound_Rhythum.Inst.MusicTime());
    }

    private void OnEnable()
    {
        Setting();
    }

    public void Setting()
    {
            if (SceneManager.GetActiveScene().name != "Rhythm")
            {
                return;
            }
            bpmTxt.text = Sound_Rhythum.Inst.bpm.ToString();
            startTimeTxt.text = Sound_Rhythum.Inst.startTime.ToString();
            CreateMap();
    }

    private void OnDisable()
    {
        if (SceneManager.GetActiveScene().name != "Rhythm")
        {
            return;
        }
    }

    private void Update()
    {
        slider.value = Sound_Rhythum.Inst.MusicTime() / Sound_Rhythum.Inst.MusicLength();
        if (isPlay)
        {
            tMap.transform.Translate(-scrollingSpeed * Time.deltaTime * transform.right);
        }
        if (isDrag)
        {
            float Move = Mouse.transform.position.x - startMouseX;
            tMap.position = new Vector3(startX + Move, tMap.position.y, tMap.position.z);
            Sound_Rhythum.Inst.PauseMusic();
        }
    }
    void SaveGameData()
    {
        // Serializable로 되어 있는 클래스 만들기        
        SaveData saveData = new();                      // 해당 클래스의 인스턴스 만들기
        saveData.bpm = Sound_Rhythum.Inst.bpm;          // 인스턴스에 데이터 기록
        saveData.startTime = Sound_Rhythum.Inst.startTime;
        saveData.NoteList1 = NoteDic[0].Keys.ToList();
        saveData.NoteList2 = NoteDic[1].Keys.ToList();
        //saveData.NoteList2 = new Dictionary<int, int>(NoteDic[1]);

        string json = JsonUtility.ToJson(saveData);     // 해당 클래스를 json형식의 문자열로 변경

        string path = $"{Application.dataPath}/Save/";  // 파일을 저장할 폴더를 지정
        if (!Directory.Exists(path))            // 해당 폴더가 없으면
        {
            Directory.CreateDirectory(path);    // 해당 폴더를 새로 만든다.
        }

        string fullPath = $"{path}{Sound_Rhythum.Inst.ClipName()}.json";   // 폴더이름과 파일이름을 합쳐서
        File.WriteAllText(fullPath, json);      // 파일에 json형식의 문자열로 변경한 내용을 저장        
    }

    public void LoadGameData()
    {
        string path = $"{Application.dataPath}/Save/";      // 경로 확인용
        string fullPath = $"{path}{Sound_Rhythum.Inst.ClipName()}.json";               // 전체 경로 확인용

        if (Directory.Exists(path) && File.Exists(fullPath)) // 해당 폴더가 있고 파일도 있으면
        {
            string json = File.ReadAllText(fullPath);       // json형식의 데이터 읽기
            SaveData loadData = JsonUtility.FromJson<SaveData>(json);   //SaveData 타입에 맞게 파싱
            Sound_Rhythum.Inst.bpm = loadData.bpm;                                // 읽어온 데이터로 최고점수 기록 변경
            Sound_Rhythum.Inst.startTime = loadData.startTime;
            if(loadData.NoteList1 != null && loadData.NoteList2 != null)
            {
                NoteDic[0] = loadData.NoteList1.ToDictionary(x => x, x => x); //  new Dictionary<int, int>(loadData.NoteDic1);
                NoteDic[1] = loadData.NoteList2.ToDictionary(x => x, x => x);
            }
        }
        else
        {
            Debug.Log("No Sheet");
            //highScores = new int[] { 0, 0, 0, 0, 0 };
            //highScorerNames = new string[] { "임시 이름1", "임시 이름2", "임시 이름3", "임시 이름4", "임시 이름5" };
        }
    }

    void PlayButton()
    {
        Sound_Rhythum.Inst.ChangeTime((-tMap.transform.position.x + Sound_Rhythum.Inst.startTime) * 0.1f);
        if (Sound_Rhythum.Inst.PlayOrPauseButton())
        {
            isPlay = true;
            musicButton[0].gameObject.GetComponentInChildren<TextMeshProUGUI>().text = "l l";
        }
        else
        {
            isPlay = false;
            musicButton[0].gameObject.GetComponentInChildren<TextMeshProUGUI>().text = "►";
        }
    }

    void ResetButton()
    {
        isPlay = false;
        Sound_Rhythum.Inst.Clear();
        Sound_Rhythum.Inst.ChangeTime(0);
        musicButton[0].gameObject.GetComponentInChildren<TextMeshProUGUI>().text = "►";
        CreateMap();
    }

    void ConfirmButton()
    {
        SaveGameData();
        Sound_Rhythum.Inst.Clear();
        TowerManager.Inst.MoveScene(-1);
    }

    public void TestButton()
    {
        isDrag = true;
    }

    public void CreateMap()
    {
        DeleteLine();
        float totalDistance = scrollingSpeed * 60f;
        float objectWidth = totalDistance / Sound_Rhythum.Inst.bpm;
        pBlock.transform.GetChild(0).transform.localScale = new Vector3(objectWidth, pBlock.transform.localScale.y, pBlock.transform.localScale.z);
        pBlock.transform.GetChild(1).transform.localScale = new Vector3(objectWidth, pBlock.transform.localScale.y, pBlock.transform.localScale.z);
        for (int i = 0; i < Sound_Rhythum.Inst.bpm * 5; i++)
        {
            GameObject obj = Instantiate(pBlock, tMap);
            obj.transform.Translate(i * objectWidth, 0, 0);
            Block_Rhythm block_Rhythm = obj.GetComponent<Block_Rhythm>();
            block_Rhythm.Init(this, i);
        }
        tMap.transform.Translate(-Sound_Rhythum.Inst.startTime, 0, 0);
    }

    internal void UnClick()
    {
        if (Manager_Rhythm.Inst.mode != Manager_Rhythm.MODE.EDIT)
        {
            return;
        }
        Sound_Rhythum.Inst.ChangeTime((-tMap.transform.position.x + Sound_Rhythum.Inst.startTime) * 0.1f);
        if (isPlay)
        {
            Sound_Rhythum.Inst.ResumeMusic();
        }
        isDrag = false;

        if (isMaking)
        {
            isMaking = false;
            RaycastHit2D hitInformation = Physics2D.Raycast(Mouse.transform.position, Camera.main.transform.forward);
            if (hitInformation.collider != null)
            {
                if (hitInformation.collider.CompareTag("Block"))
                {
                    NoteBlock_rhythm noteBlock = hitInformation.collider.GetComponent<NoteBlock_rhythm>();
                    if (makingIndex == noteBlock.index)
                    {
                        noteBlock.MakeNote();
                    }
                    else
                    {
                        noteBlock.MakeLongNote(makingIndex[1] - noteBlock.index[1]);
                    }
                }
                else
                {
                    noteBlocks[makingIndex[0]][makingIndex[0]].Cancle();
                }
            }
        }
    }

    internal void Click()
    {
        if (Manager_Rhythm.Inst.mode != Manager_Rhythm.MODE.EDIT)
        {
            return;
        }
        //if(isPlay)
        //{
        //    Sound_Rhythum.Inst.PauseMusic();
        //}
        //Vector2 touchPos = Camera.main.ScreenToWorldPoint(inputActions.Mouse.Pos.ReadValue<Vector2>());
        // 콜라이더에 걸린 오브젝트를 가져올 수 있음
        int layerMask = 1 << LayerMask.NameToLayer("UI");
        RaycastHit2D hitInformation = Physics2D.Raycast(Mouse.transform.position, Camera.main.transform.forward, Mathf.Infinity, layerMask);

        if (hitInformation.collider != null)
        {
            if (hitInformation.collider.CompareTag("Block"))
            {
                NoteBlock_rhythm noteBlock = hitInformation.collider.gameObject.GetComponent<NoteBlock_rhythm>();
                if(noteBlock.Click())
                {
                    NoteDic[noteBlock.index[0]].Add(noteBlock.index[1], 0);
                }
                else
                {
                    NoteDic[noteBlock.index[0]].Remove(noteBlock.index[1]);
                }
                int[] temp = noteBlock.index;
                makingIndex = noteBlock.index;
                isMaking = true;
            }

        }
        else
        {
            hitInformation = Physics2D.Raycast(Mouse.transform.position, Camera.main.transform.forward);
            if (hitInformation.collider != null)
            {
                if (hitInformation.collider.CompareTag("Board"))
                {
                    if (isPlay)
                    {
                        Sound_Rhythum.Inst.PauseMusic();
                    }
                    else
                    {
                        Sound_Rhythum.Inst.PlayMusicSound();
                    }
                    startX = tMap.position.x;
                    startMouseX = Mouse.transform.position.x;
                    isDrag = true;
                }
            }

        }
    }

    //public void OnValueChange()
    //{
    //    Sound_Rhythum.Inst.PauseMusic();
    //}

    public void BGClick()
    {
        if (isPlay)
        {
            Sound_Rhythum.Inst.PauseMusic();
        }
    }

    void DeleteLine()
    {
        tMap.transform.position = new Vector3(0, 0, 0);
        Transform[] childList = tMap.gameObject.GetComponentsInChildren<Transform>();

        if (childList != null)
        {
            for (int i = 1; i < childList.Length; i++)
            {
                if (childList[i] != transform)
                    Destroy(childList[i].gameObject);
            }
        }
    }

    public void BpmChange(bool add)
    {
        if (add)
        {
            Sound_Rhythum.Inst.bpm++;
        }
        else
        {
            Sound_Rhythum.Inst.bpm--;
        }
        bpmTxt.text = Sound_Rhythum.Inst.bpm.ToString();
    }
    public void StartTimeChange(bool add)
    {
        if (add)
        {
            Sound_Rhythum.Inst.startTime += 0.1f;
        }
        else
        {
            Sound_Rhythum.Inst.startTime -= 0.1f;
        }
        startTimeTxt.text = string.Format("{0:F1}", Sound_Rhythum.Inst.startTime);
    }

    public void InputFieldChange(bool bpm)
    {
        //if(bpm)
        //{
        //    int a = Convert.ToInt32(bpmTxt.text);
        //    Sound_Rhythum.Inst.bpm = a;
        //}
        //else
        //{
        //    Sound_Rhythum.Inst.startTime = Convert.ToInt32(startTimeTxt.text);
        //}

    }
}
