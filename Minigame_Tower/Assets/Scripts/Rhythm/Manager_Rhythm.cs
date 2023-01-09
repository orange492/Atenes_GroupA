//#define TEST

using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Manager_Rhythm : SingletonPuzzle<Manager_Rhythm>
{
    const float MAX_HP = 100;
    const float DECREASE_HP = 10;
    const float TIMER = 0.5f;
    public const float SPEED = 10f;

    Dictionary<int, string> judgeTxt = new Dictionary<int, string>
    {
        { 0, "Miss" },
        { 1, "Good" },
        { 2, "Great" },
        { 3, "Perfect" }
    };

    public enum MODE
    {
        NONE,
        PLAY,
        EDIT
    }

    Editor_Rhythm editor_Rhythm;
    TextMeshProUGUI comboTxt;
    TextMeshProUGUI[] JudgeTxt = new TextMeshProUGUI[2];
    Slider hpBar;

    GameObject oCombo;
    GameObject oEditCanvas;
    NoteSpawner noteSpawner;
    Player_Rhythm player;
    Button editButton;
    float maxHp;
    float currentHp;
    public MODE mode;


    int combo;
    float[] timer = new float[2];

    Dictionary<int, int>[] NoteDic;
    List<float>[] noteList = new List<float>[2];
    int up = 0;
    int down = 0;

    protected override void Initialize()
    {
        if (SceneManager.GetActiveScene().name != "Rhythm")
        {
            return;
        }
        editor_Rhythm = FindObjectOfType<Editor_Rhythm>();
        editButton = GameObject.Find("EditButton").GetComponent<Button>();
        editButton.onClick.AddListener(OpenEditCanvas);
        oEditCanvas = GameObject.Find("NoteCanvas");
        oEditCanvas.transform.root.gameObject.SetActive(false);
        comboTxt = GameObject.Find("NumberTxt").GetComponent<TextMeshProUGUI>();
        JudgeTxt[0] = GameObject.Find("UpTxt").GetComponent<TextMeshProUGUI>();
        JudgeTxt[1] = GameObject.Find("DownTxt").GetComponent<TextMeshProUGUI>();
        for (int i = 0; i < 2; i++)
        {
            JudgeTxt[i].text = "";
        }
        hpBar = FindObjectOfType<Slider>();

        oCombo = comboTxt.transform.parent.gameObject;
        noteSpawner = FindObjectOfType<NoteSpawner>();
        player = FindObjectOfType<Player_Rhythm>();
        for (int i = 0; i < timer.Length; i++)
        {
            timer[i] = -1;
        }
        maxHp = MAX_HP;
        currentHp = maxHp;
        ResetCombo();
        SetHpBar();
        mode = MODE.PLAY;
        //OpenMap();
        ChangeMapToTime();
    }

    void ChangeMapToTime()
    {
        editor_Rhythm.LoadGameData();
        NoteDic = editor_Rhythm.NoteDic;
        noteList[0] = new List<float>();
        noteList[1] = new List<float>();
        foreach (var item in NoteDic[0])
        {
            noteList[0].Add(-0.464f + ((item.Key - 4) * 0.149333f));
        }
        foreach (var item in NoteDic[1])
        {
            noteList[1].Add(-0.464f + ((item.Key - 4) * 0.149333f));
        }
        noteList[0].Sort();
        noteList[1].Sort();
        up = 0;
        down = 0;
    }

    void Start()
    {

    }

    void Update()
    {
        if (SceneManager.GetActiveScene().name != "Rhythm")
        {
            return;
        }
        for (int i = 0; i < timer.Length; i++)
        {
            if (timer[i] > 0)
            {
                timer[i] -= Time.deltaTime;
            }
            else if (timer[i] == -1)
            {
                continue;
            }
            else
            {
                JudgeTxt[i].text = "";
                timer[i] = -1;
            }
        }
        if (mode == MODE.EDIT)
            return;
        if (noteList[0].Count != 0 && noteList[0].Count > up)
        {
            if (noteList[0][up] < Sound_Rhythum.Inst.MusicTime())
            {
                CreateNote(0);
                up++;
            }

        }
        if (noteList[1].Count != 0 && noteList[1].Count > down)
        {
            if (noteList[1][down] < Sound_Rhythum.Inst.MusicTime())
            {
                CreateNote(1);
                down++;
            }
        }


    }

    public void Click()
    {
        editor_Rhythm.Click();
    }

    public void UnClick()
    {
        editor_Rhythm.UnClick();
    }


    public void CreateNote(int value)
    {
        noteSpawner.CreateNote(value);
    }

    public void ClickKey(int loca)
    {
        if (mode != MODE.NONE)
        {

            player.ClickKey(loca);
            foreach (Note Value in noteSpawner.noteDic[player.loca].Values)
            {
                if (Mathf.Abs(player.player.position.x - Value.transform.position.x) < 0.5f)
                {
                    SetJudgeTxt(loca, 3);
                    SetCombo();
                    Value.Remove();
                    break;
                }
                else if (Mathf.Abs(player.player.position.x - Value.transform.position.x) < 0.10f)
                {
                    SetJudgeTxt(loca, 2);
                    SetCombo();
                    Value.Remove();
                    break;
                }
                else if (Mathf.Abs(player.player.position.x - Value.transform.position.x) < 1.5f)
                {
                    SetJudgeTxt(loca, 1);
                    ResetCombo();
                    Value.Remove();
                    break;
                }
            }
        }
        //else if(mode == MODE.EDIT)
        //{
        //    if(loca==0)
        //    {
        //        Sound_Rhythum.Inst.startTime--;
        //    }
        //    else
        //    {
        //        Sound_Rhythum.Inst.startTime++;
        //    }
        //}
    }

    public void SetJudgeTxt(int loca, int judgement)
    {
        JudgeTxt[loca].text = judgeTxt[judgement];
        timer[loca] = TIMER;
    }

    void SetCombo(bool reset = false)
    {
        combo++;
        comboTxt.text = combo.ToString();
        if (combo == 5)
        {
            oCombo.gameObject.SetActive(true);
        }
    }

    void ResetCombo()
    {
        combo = 0;
        oCombo.gameObject.SetActive(false);
    }

    void SetHpBar()
    {
        hpBar.value = currentHp / maxHp;
#if !TEST
        if (hpBar.value <= 0)
        {
            TowerManager.Inst.GameOver();
            mode = MODE.NONE;
            Sound_Rhythum.Inst.Clear();
        }
#endif
    }

    public void MissNote(int loca)
    {
        SetJudgeTxt(loca, 0);
        ResetCombo();
        currentHp -= DECREASE_HP;
        SetHpBar();
    }

    public void OpenEditCanvas()
    {
        mode = MODE.EDIT;
        oEditCanvas.transform.root.gameObject.SetActive(true);
        //oEditCanvas.transform.Translate(0, 10, 0);
        TowerManager.Inst.OptionBtn(false);
        //editor_Rhythm.isPlay = false;
        Sound_Rhythum.Inst.Clear();
    }

    public void OpenMap()
    {
        oEditCanvas.transform.root.gameObject.SetActive(true);
        oEditCanvas.transform.Translate(0, -10, 0);
        editor_Rhythm.Setting();
        editor_Rhythm.isPlay = true;
    }
}
