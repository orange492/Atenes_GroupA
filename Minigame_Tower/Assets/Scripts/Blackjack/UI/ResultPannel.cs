using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using static System.Net.Mime.MediaTypeNames;
using System;

public class ResultPannel : MonoBehaviour
{
    ResultPannel resultPannel;
    Button nextButton;
    CanvasGroup canvasGroup;


    private void Start()
    {
        Close();
        //BlackjackManager.Inst.Player.onDead += Open(); // 이코드 왜안되는지 확인 필요!
    }
    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();

        resultPannel = GetComponent<ResultPannel>();
        nextButton = GetComponent<Button>();
        nextButton.onClick.AddListener(OnClick_Next);

    }
    void Close()
    {
        canvasGroup.alpha = 0.0f;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }

    void Open()
    {
        StartCoroutine(OpenDelay());
    }

    void OnClick_Next()
    {
        
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);   // 현재 열린 씬을 새로 열기
    }

    IEnumerator OpenDelay()
    {
        yield return new WaitForSeconds(0.1f);

        canvasGroup.alpha = 1.0f;
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
    }


}
