using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TowerManager : Singleton<TowerManager>
{
    Dictionary<int, string> scene = new Dictionary<int, string>()
    {
     {0, "DefenceScene"},
     {1, "Test_Table"},
     {2, "Test_Puzzle"},
     {3, "JumpScene"},
     {4, "PlatformerScene"},
     {5, "TowerScene"}
};

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MoveScene(int num)
    {
        SceneManager.LoadScene(scene[num]);
    }

}
