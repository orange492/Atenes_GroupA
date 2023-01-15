using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EggGameClear : MonoBehaviour
{
    Button toMainButton;
    Transform gameOverPanel;
    CanvasGroup canvasGroup;
    bool isGameClear = false;
    float timeElapse = 0.0f;

    private void Awake()
    {
        gameOverPanel = transform.GetChild(0).transform;

        toMainButton = gameOverPanel.GetChild(1).GetComponent<Button>();
        toMainButton.onClick.AddListener(OnToMain);
        canvasGroup = transform.GetComponent<CanvasGroup>();
    }
    void Start()
    {
        if (EggGameManager.Inst != null)
        {
            EggGameManager.Inst.onModeChange += ModeChange;
        }
        canvasGroup.alpha = 0.0f;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }


    private void OnToMain()
    {
        EggGameManager.Inst.mode = EggGameManager.Mode.ToMain;
        TowerManager.Inst.Clear();
        
    }

    private void OnDestroy()
    {
        if (EggGameManager.Inst != null)
        {
            EggGameManager.Inst.onModeChange -= ModeChange;
        }
    }


    // Start is called before the first frame update


    private void ModeChange(EggGameManager.Mode obj)
    {
        if (obj == EggGameManager.Mode.Clear)
        {
             isGameClear = true;
            
            canvasGroup.interactable = true;
            canvasGroup.blocksRaycasts = true;

        }
        else
        {
          canvasGroup.alpha = 0.0f;
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isGameClear)
        {
            timeElapse += Time.deltaTime;
            if (timeElapse > 2.0f)
            {
                canvasGroup.alpha += Time.deltaTime;
            }
        }
    }


}

