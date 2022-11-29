using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class RemainScore : MonoBehaviour
{
     Slider slider;
    
    BlockController blockController;
    bool isClear = false;

    public bool IsClear 
    {
        get =>isClear;
        set
        {
            isClear = value;
            if (isClear)
            {
                PuzzleGameManager.Inst.RemainTime.IsGameOver = true;
            }
        }

    }
    float clearScore = 10000;




    private void Awake()
    {
        slider = transform.GetChild(0).GetComponent<Slider>();
    }
    private void Start()
    {
        blockController = FindObjectOfType<BlockController>();
        blockController.onScoreChange += RemainScoreUpdate;
        slider.maxValue = clearScore;
    }

    

    private void Update()
    {


        
  
    }

    private void RemainScoreUpdate(int obj)
    {
        slider.value = obj;
        if (slider.value >= clearScore)
        {
            IsClear = true;
        }
    }

    public int RemainScoreReturn()
    {
        return (int)(clearScore - slider.value);
    }
    


}