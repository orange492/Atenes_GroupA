using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    
    int targetScore=0;
    float currentScore=0;
    float scoreChangeSpeed = 100.0f;
    
    BlockController blockController;

    TextMeshProUGUI scoreText;


    private void Start()
    {
        blockController = FindObjectOfType<BlockController>();
        Transform panel = transform.GetChild(0);
        scoreText = panel.GetChild(0).GetComponent<TextMeshProUGUI>();
        blockController.onScoreChange += RefreshScore;
    }

  

    private void Awake()
    {
       
        
    }

    private void Update()
    {
        if (currentScore < targetScore)
        {
            currentScore += Time.deltaTime * scoreChangeSpeed;
            currentScore = Math.Min(currentScore, targetScore);
            scoreText.text = $"{currentScore:f0}";

        }
    }
    private void RefreshScore(int newScore)
    {
        targetScore = newScore;
    }



}
