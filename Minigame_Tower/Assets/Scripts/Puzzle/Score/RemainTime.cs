using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class RemainTime : MonoBehaviour
{
     Slider slider;
    
    BlockController blockController;
    bool IsTimeOver = false;

    


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


        slider.value -= Time.deltaTime;
  
    }
   

    


}
