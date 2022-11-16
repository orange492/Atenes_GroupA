using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TowerManager : Singleton<TowerManager>
{
    Dictionary<int, string> scene = new Dictionary<int, string>()
    {
     {0, "TowerScene"},
     {1, "DefenceScene"},
     {2, "Test_Table"},
     {3, "Test_Puzzle"},
     {4, "JumpScene"},
     {5, "PlatformerScene"}
};

    // Start is called before the first frame update
    void Awake()
    {
        DontDestroyOnLoad(gameObject.transform.parent);
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
