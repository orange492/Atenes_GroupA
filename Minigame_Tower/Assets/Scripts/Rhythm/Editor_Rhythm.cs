using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.EventSystems;

public class Editor_Rhythm : MonoBehaviour //, IPointerDownHandler
{
    [SerializeField]
    GameObject Mouse;
    [SerializeField]
    GameObject oEditor;
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

    Button[] musicButton;
    //GameObject oLine;
    bool isPlay;
    bool isDrag;
    float scrollingSpeed;
    float startX;
    float startMouseX;

    private void Awake()
    {
        scrollingSpeed = Manager_Rhythm.SPEED;
        isPlay = false;
        isDrag = false;
        musicButton = new Button[2];
        musicButton[0] = transform.Find("PlayBtn").GetComponent<Button>();
        musicButton[0].onClick.AddListener(PlayButton);
        musicButton[1] = transform.Find("ResetBtn").GetComponent<Button>();
        musicButton[1].onClick.AddListener(ResetButton);
        //BgButton.onClick.AddListener(BGClick);
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
        bpmTxt.text = Sound_Rhythum.Inst.bpm.ToString();
        startTimeTxt.text = Sound_Rhythum.Inst.startTime.ToString();
        oEditor.SetActive(true);
        CreateMap();
    }

    private void OnDisable()
    {
        oEditor.SetActive(false);
    }

    private void Update()
    {
        slider.value = Sound_Rhythum.Inst.MusicTime() / Sound_Rhythum.Inst.MusicLength();
        if (isPlay)
        {
            tMap.transform.Translate(-scrollingSpeed * Time.deltaTime * transform.right);
        }
        if(isDrag)
        {
            float Move = Mouse.transform.position.x - startMouseX;
            tMap.position = new Vector3(startX + Move, tMap.position.y, tMap.position.z);
            Sound_Rhythum.Inst.ChangeTime((-tMap.transform.position.x + Sound_Rhythum.Inst.startTime)*0.1f);
            //Sound_Rhythum.Inst.PauseMusic();
        }
    }

    void PlayButton()
    {
        if(Sound_Rhythum.Inst.PlayOrPauseButton())
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
            obj.transform.Translate(i * objectWidth, 0,0);
        }
        tMap.transform.Translate(-Sound_Rhythum.Inst.startTime, 0,0);
    }

    internal void UnClick()
    {
        if (isPlay)
        {
            Sound_Rhythum.Inst.ResumeMusic();
        }
        isDrag = false;
    }

    internal void Click()
    {
        //if(isPlay)
        //{
        //    Sound_Rhythum.Inst.PauseMusic();
        //}
        //Vector2 touchPos = Camera.main.ScreenToWorldPoint(inputActions.Mouse.Pos.ReadValue<Vector2>());
        // 콜라이더에 걸린 오브젝트를 가져올 수 있음

        RaycastHit2D hitInformation = Physics2D.Raycast(Mouse.transform.position, Camera.main.transform.forward);
        if (hitInformation.collider != null)
        {
            if (hitInformation.collider.CompareTag("Board"))
            {
                if (isPlay)
                {
                    Sound_Rhythum.Inst.PauseMusic();
                }
                startX = tMap.position.x;
                startMouseX = Mouse.transform.position.x;
                isDrag = true;
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
        tMap.transform.position = new Vector3(0,0,0);
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
        if(add)
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
            Sound_Rhythum.Inst.startTime+=0.1f;
        }
        else
        {
            Sound_Rhythum.Inst.startTime-=0.1f;
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
