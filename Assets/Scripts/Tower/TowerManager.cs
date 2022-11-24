using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Linq;

public class TowerManager : SingletonPuzzle<TowerManager>
{
    Dictionary<int, string> scene = new Dictionary<int, string>()
    {
     {0, "TowerScene"},
     {1, "DefenceScene"},
     {2, "Test_Table"}, // 블랙잭 노멀모드
     {3, "Test_Puzzle"},
     {4, "JumpScene"},
     {5, "Test_Hard"} //블랙잭 하드모드
    };
    Dictionary<int, string> gmaeName = new Dictionary<int, string>()
    {
     {0, "미니게임 타워"},
     {1, "디펜스 게임"},
     {2, "블랙잭 게임"},
     {3, "퍼즐 게임"},
     {4, "점프 게임"},
     {5, "블랙잭 게임(하드)"},
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
    bool init = false;

    public struct sFloor
    {
        public int game;
        public int difficulty;
        public string goal;
        public sFloor(int _game, int _diff, string _goal)
        {
            game = _game;
            difficulty = _diff;
            goal = _goal;
        }
    }
    List<sFloor> floor = new List<sFloor>();
    int gold;

    protected override void Initialize()
    {
        if (SceneManager.GetActiveScene().name != scene[0])
        {
            return;
        }

        if (!init)
        {
            floor.Add(new sFloor(1, 0, "10웨이브"));
            floor.Add(new sFloor(1, 1, "20웨이브"));
            floor.Add(new sFloor(1, 2, "30웨이브"));
            floor.Add(new sFloor(3, 0, "500점"));
            floor.Add(new sFloor(1, 3, "40웨이브"));
            floor.Add(new sFloor(2, 0, "500라이프")); //블랙잭 노멀모드
            floor.Add(new sFloor(5, 0, "300라이프")); //블랙잭 하드모드
            SetSave();
            init = true;
        }

        PrintFloor();
    }

    public void OpenPopup(bool open)
    {
        oPopup.SetActive(open);
        if(!open)
        {
            oGameOver.SetActive(false);
            oCloseBtn.SetActive(false);
        }
    }

    public void GameOver()
    {
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
            oImage.SetActive(false);
        }
        else
        {
            oImage.SetActive(true);
        }
        OpenPopup(false);
    }

    public void Clear()
    {
        MoveScene(0);

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
            temp.Init(index, sGame[floor[index].game], gmaeName[floor[index].game], floor[index].goal, PlayerPrefs.GetInt(index.ToString() + "clear"));
            temp.transform.SetSiblingIndex(0);
            index++;
        }
    }
}
