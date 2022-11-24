using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Floor : MonoBehaviour
{
    Button btn;
    [SerializeField]
    Image gameImage;
    [SerializeField]
    TextMeshProUGUI NameTxt;
    [SerializeField]
    TextMeshProUGUI GoalTxt;
    [SerializeField]
    GameObject oClear;
    bool clear;

    public void Init(int index, Sprite spr, string name, string goal, int clear)
    {
        btn = this.GetComponent<Button>();
        btn.onClick.AddListener(() => TowerManager.Inst.ClickFloor(index));
        gameImage.sprite = spr;
        NameTxt.text = name;
        GoalTxt.text = goal;
        if(clear==1)
        {
            oClear.SetActive(true);
        }
    }
    void Start()
    {
        
    }


    void Update()
    {
        
    }
}
