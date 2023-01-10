using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Linq;
using System;

/*
난이도 : TowerManager.Inst.GetDifficulty()
클리어했을 때 : TowerManager.Inst.Clear();
실패했을 때 : TowerManager.Inst.GameOver();
*/

public class TowerManager : SingletonPuzzle<TowerManager>
{
    Dictionary<int, string> scene = new Dictionary<int, string>()
    {
     {0, "TowerScene"},
     {1, "DefenceScene"},
     {2, "Test_Table"}, // 블랙잭 노멀모드
     {3, "Test_Puzzle"},
     {4, "Test_Hard"}, //블랙잭 하드모드
     {5, "Rhythm"},
     {6, "EggLevel1" },
     {7, "EggLevel2" },
     {8, "EggLevel3" }
    };
    Dictionary<int, string> gameName = new Dictionary<int, string>()
    {
     {0, "미니게임 타워"},
     {1, "디펜스 게임"},
     {2, "블랙잭 게임"},
     {3, "퍼즐 게임"},
     {4, "블랙잭 게임(하드)"},
     {5, "리듬 게임"},
     {6, "에그 게임 lv1" },
     {7, "에그 게임 lv2" },
     {8, "에그 게임 lv3" }
    };
    Dictionary<int, bool> isVertical = new Dictionary<int, bool>()
    {
     {0, true},
     {1, true},
     {2, false},
     {3, true},
     {4, false},
     {5, false},
     {6, false},
     {7, false},
     {8, false}

    };
    [SerializeField]
    GameObject pFloor;
    [SerializeField]
    Sprite[] sGame;
    [SerializeField]
    GameObject oImage;
    [SerializeField]
    GameObject oPopup;
    [SerializeField]
    GameObject oGameOver;
    [SerializeField]
    GameObject oCloseBtn;
    [SerializeField]
    GameObject oCloseBtn2;
    CanvasScaler canvasScaler;
    bool init = false;

    public struct sFloor
    {
        public int game;
        public int difficulty;
        public string goal;
        public bool vertical;
        public sFloor(int _game, int _diff, string _goal, Dictionary<int, bool> _vertical)
        {
            game = _game;
            difficulty = _diff;
            goal = _goal;
            vertical = _vertical[_game];
        }
    }
    List<sFloor> floor = new List<sFloor>();
    int gold;

    protected override void Initialize()
    {
        canvasScaler = gameObject.GetComponent<CanvasScaler>();
        canvasScaler.matchWidthOrHeight = 0f;
        oPopup.GetComponent<RectTransform>().offsetMin = new Vector3(0, 0, 0);

        if (SceneManager.GetActiveScene().name != scene[0])
        {
            return;
        }

        if (!init)
        {
            floor.Add(new sFloor(1, 0, "10웨이브", isVertical));
            floor.Add(new sFloor(2, 0, "500라이프", isVertical)); //블랙잭 노멀모드
            floor.Add(new sFloor(3, 0, "500점", isVertical));
            floor.Add(new sFloor(5, 0, "신나는 음악", isVertical));
            floor.Add(new sFloor(1, 1, "20웨이브", isVertical));
            floor.Add(new sFloor(4, 0, "300라이프", isVertical)); //블랙잭 하드모드
            floor.Add(new sFloor(3, 0, "1000점", isVertical));
            floor.Add(new sFloor(6, 0, "1레벨", isVertical));
            floor.Add(new sFloor(7, 1, "2레벨", isVertical));
            floor.Add(new sFloor(8, 2, "3레벨", isVertical));
            SetSave();
            init = true;
        }

        PrintFloor();
    }

    public void OpenPopup(bool open)
    {
        oPopup.SetActive(open);
        if(SceneManager.GetActiveScene().name == "EggLevel1" || SceneManager.GetActiveScene().name == "EggLevel2" || SceneManager.GetActiveScene().name == "EggLevel3")
        {
            oCloseBtn2.SetActive(true);
        }
        if(!open)
        {
            oGameOver.SetActive(false);
            oCloseBtn.SetActive(false);
            oCloseBtn2.SetActive(false);
        }
    }

    public void GameOver()
    {
        if(PlayerPrefs.GetInt("isVertical")==0)
        {
            canvasScaler.matchWidthOrHeight = 0.5f;
            oPopup.GetComponent<RectTransform>().offsetMin = new Vector3(0,-400,0);
        }
        oGameOver.SetActive(true);
        oCloseBtn.SetActive(true);
        OpenPopup(true);
    }

    public int GetDifficulty()
    {
        return PlayerPrefs.GetInt("difficulty");
    }

    void SetSave()
    {
        for (int i = 0; i < floor.Count; i++)
        {
            PlayerPrefs.SetInt(i.ToString() + "clear", 0);
        }
        PlayerPrefs.SetInt("height", 4);
        PlayerPrefs.SetInt("currentFloor", -1);
        PlayerPrefs.SetInt("difficulty", 0);
    }

    public void ClickFloor(int index)
    {
        PlayerPrefs.SetInt("currentFloor", index);
        PlayerPrefs.SetInt("difficulty", floor[index].difficulty);
        PlayerPrefs.SetInt("isVertical", Convert.ToInt32(floor[index].vertical));
        MoveScene(floor[index].game);
    }

    public void MoveScene(int num)
    {
        if (num == -1)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        else
        {
            SceneManager.LoadScene(scene[num]);
        }
        if (num == 0)
        {
            OptionBtn(false);
        }
        else
        {
            OptionBtn(true);
        }
        OpenPopup(false);
    }

    public void OptionBtn(bool act)
    {
        oImage.SetActive(act);
    }

    public void Clear()
    {
        MoveScene(0);

        if(PlayerPrefs.GetInt(PlayerPrefs.GetInt("currentFloor").ToString() + "clear") == 1)
        {
            return;
        }
        PlayerPrefs.SetInt(PlayerPrefs.GetInt("currentFloor").ToString() + "clear", 1);
        if (PlayerPrefs.GetInt("height") < floor.Count)
        {
            PlayerPrefs.SetInt("height", PlayerPrefs.GetInt("height") + 1);
        }
    }

    void PrintFloor()
    {
        Transform tower = GameObject.Find("Tower").transform;
        int index = 0;
        while (index < PlayerPrefs.GetInt("height"))
        {
            Floor temp = Instantiate(pFloor, tower).GetComponent<Floor>();
            temp.Init(index, sGame[floor[index].game], gameName[floor[index].game], floor[index].goal, PlayerPrefs.GetInt(index.ToString() + "clear"));
            temp.transform.SetSiblingIndex(0);
            index++;
        }
    }
}
