//#define TEST

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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

    enum MODE
    {
        NONE,
        PLAY,
        EDIT
    }

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
    MODE mode;


    int combo;
    float[] timer = new float[2];

    protected override void Initialize()
    {
        if (SceneManager.GetActiveScene().name != "Rhythm")
        {
            return;
        }
        editButton = GameObject.Find("EditButton").GetComponent<Button>();
        editButton.onClick.AddListener(OpenEditCanvas);
        oEditCanvas = GameObject.Find("NoteCanvas");
        oEditCanvas.SetActive(false);
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
    }

    void Start()
    {
       
    }

    void Update()
    {
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
                if (Mathf.Abs(player.player.position.x - Value.transform.position.x) < 0.3f)
                {
                    SetJudgeTxt(loca, 3);
                    SetCombo();
                    Value.Remove();
                    break;
                }
                else if (Mathf.Abs(player.player.position.x - Value.transform.position.x) < 0.6f)
                {
                    SetJudgeTxt(loca, 2);
                    SetCombo();
                    Value.Remove();
                    break;
                }
                else if (Mathf.Abs(player.player.position.x - Value.transform.position.x) < 0.9f)
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
        Sound_Rhythum.Inst.Clear();
        oEditCanvas.SetActive(true);
    }
}
