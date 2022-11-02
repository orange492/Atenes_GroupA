using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultPannel : MonoBehaviour
{
    ResultPannel resultPannel;
    Button nextButton;
    CanvasGroup canvasGroup;
    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();

        resultPannel = GetComponent<ResultPannel>();
        nextButton = GetComponent<Button>();
       
        
    }


}
