using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Fever : MonoBehaviour
{
     Slider slider;
    int feverCount=0;
    BlockController blockController;
    bool isFevering = false;

    public bool IsFevering
    {
        get => isFevering;
        set
        {
            isFevering = value;
            if (IsFevering)
            {
                blockController.OnFever();
            }
            else
            {
                blockController.OffFever();
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
        blockController.onFeverChange += FeverChange;

    }


    private void Update()
    {
       
        if (isFevering)
        {

            slider.value -= Time.deltaTime * 100.0f;
            if (slider.value <= 0)
            {
                IsFevering = false;
            }
        }
    }
    private void FeverChange(int fever)
    {
        if (!isFevering) 
        {
            feverCount += fever;
            if (feverCount > 1000)
            {
                feverCount = 1000;
            }
            slider.value = feverCount;
            

            if (feverCount >= 1000)
            {
                IsFevering = true;
                feverCount = 0;
            }
        }
    }

    


}
