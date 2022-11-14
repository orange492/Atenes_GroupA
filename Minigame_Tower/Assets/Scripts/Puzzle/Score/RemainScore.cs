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




    private void Awake()
    {
        slider = transform.GetChild(0).GetComponent<Slider>();
    }
    private void Start()
    {
        blockController = FindObjectOfType<BlockController>();
        blockController.onScoreChange += RemainScoreUpdate;
    }

    

    private void Update()
    {


        
  
    }

    private void RemainScoreUpdate(int obj)
    {
        slider.value = obj;
    }
    


}
