using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class RemainTime : MonoBehaviour
{
     Slider slider;
    
    BlockController blockController;
    bool isGameOver = false;



    public bool IsGameOver
    {
        get => isGameOver;
        set { isGameOver = value;
            if (isGameOver)
            {
                blockController.IsGameOver = true;
                PuzzleGameManager.Inst.GameOverPanel.GameOver();
            }
        }
    }

    float remainTime;

    public float RemainTimeProperty
    {
        get => remainTime;
        set
        {
            remainTime = value;
            if (remainTime <= 0)
            {
                IsGameOver = true;
                
            }
        }
    }

    


    private void Awake()
    {
        slider = transform.GetChild(0).GetComponent<Slider>();
    }
    private void Start()
    {
        blockController = FindObjectOfType<BlockController>();
        
    }


    private void Update()
    {

        if (!isGameOver)
        {
            slider.value -= Time.deltaTime;
            RemainTimeProperty = slider.value;
        }
    }
   

    


}
