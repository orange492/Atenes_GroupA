using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Manager_Rhythm : SingletonPuzzle<Manager_Rhythm>
{
    Dictionary<int, string> judgeTxt = new Dictionary<int, string>
    {
        { 0, "Miss" },
        { 1, "Good" },
        { 2, "Great" },
        { 3, "Perfect" }
    };

    [SerializeField]
    TextMeshProUGUI comboTxt;
    [SerializeField]
    TextMeshProUGUI[] JudgeTxt;

    GameObject oCombo;
    NoteSpawner noteSpawner;
    Player_Rhythm player;
    int combo;
    float[] timer = new float[3];

    protected override void Initialize()
    {
        oCombo = comboTxt.transform.parent.gameObject;
        noteSpawner = FindObjectOfType<NoteSpawner>();
        player = FindObjectOfType<Player_Rhythm>();
        for (int i = 0; i < timer.Length; i++)
        {
            timer[i] = -1;
        }
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
                if (i == 2)
                {
                    oCombo.gameObject.SetActive(false);
                }
                else
                {
                    JudgeTxt[i].gameObject.SetActive(false);
                }
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
        foreach (Note Value in noteSpawner.noteDic[player.loca].Values)
        {
            if (Mathf.Abs(player.player.position.x - Value.transform.position.x) < 0.5f)
            {
                SetCombo();
                Value.Remove();
                JudgeTxt[loca].text = "Great";
                timer[loca] = 1;
                break;
            }
        }

        player.ClickKey(loca);
    }

    public void SetJudgeTxt(int loca, int judgement)
    {
        JudgeTxt[loca].gameObject.SetActive(true);
        JudgeTxt[loca].text = judgeTxt[judgement];
        timer[loca] = 1;
    }

    void SetCombo(bool reset = false)
    {
        if (reset)
        {
            combo = 0;
            timer[3] = 1;
        }
        else
        {
            combo++;
        }
        comboTxt.text = combo.ToString();
    }
}
