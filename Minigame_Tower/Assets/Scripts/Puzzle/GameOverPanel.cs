using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverPanel : MonoBehaviour
{

    TextMeshProUGUI GameOverText;
    TextMeshProUGUI resultText;
    Button retryButton;
    Transform gameOverPanel;

    private void Awake()
    {
        gameOverPanel = transform.GetChild(0).transform;
        GameOverText = gameOverPanel.GetChild(0).GetComponent<TextMeshProUGUI>();
        resultText = gameOverPanel.GetChild(1).GetComponent<TextMeshProUGUI>();
        retryButton = gameOverPanel.GetChild(2).GetComponent<Button>();
        retryButton.onClick.AddListener(OnRetry);
    }

    private void OnRetry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }



    // Start is called before the first frame update
    void Start()
    {
        this.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GameOver()
    {
        this.gameObject.SetActive(true);
        if (!PuzzleGameManager.Inst.RemainScore.IsClear)
        {
            resultText.text = $"남은 점수:{PuzzleGameManager.Inst.RemainScore.RemainScoreReturn()}점";
        }
        else
        {
            GameOverText.text = "GameClear!";
            resultText.text = $"남은 시간: {(int)PuzzleGameManager.Inst.RemainTime.RemainTimeProperty}초";
        }
    }
}
