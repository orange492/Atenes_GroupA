using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class ResultPannel : MonoBehaviour
{
    ResultPannel resultPannel;
    Button nextButton;
    CanvasGroup canvasGroup;
    
 

    private void Start()
    {
        Close();
       
        //BlackjackManager.Inst.Player.onDead += open(); // 이코드 왜안되는지 확인 필요!
    }
    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();

        resultPannel = GetComponent<ResultPannel>();
        nextButton = GetComponent<Button>();
       
    }
    public void Close()
    {
        canvasGroup.alpha = 0.0f;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }
    //void open() 
    //{
    //    StartCoroutine(OpenDelay());
    //}
   public void Open()
    {
        canvasGroup.alpha = 1.0f;
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
    }

   public void OnClick_Next()
    {

        {
            TowerManager.Inst.Clear(); //클리어시 타워씬으로 복구
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);   //    // 현재 열린 씬을 새로 열기
            Close();
        }
    }

    //IEnumerator OpenDelay()
    //{
    //    yield return new WaitForSeconds(0.1f);

    //    canvasGroup.alpha = 1.0f;
    //    canvasGroup.interactable = true;
    //    canvasGroup.blocksRaycasts = true;
    //}


}
