using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI_Retry : MonoBehaviour
{
    Button button;
    private void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(Retry);
        
    }

    private void Start()
    {
        gameObject.SetActive(false);
        if (EggGameManager.Inst != null)
        {
            EggGameManager.Inst.onModeChange += ModeChange;
        }
    }
    private void ModeChange(EggGameManager.Mode obj)
    {
        gameObject.SetActive(obj == EggGameManager.Mode.Play||obj == EggGameManager.Mode.Die);
    }
    private void OnDestroy()
    {
        if (EggGameManager.Inst != null)
        {
            EggGameManager.Inst.onModeChange -= ModeChange;
        }
    }

    private void Retry()
    {
        EggGameManager.Inst.mode = EggGameManager.Mode.ReadyToPlay;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
