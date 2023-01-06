using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI_Retry : MonoBehaviour
{
    Button button;
    public GameObject eggPrefab;
    private void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(Retry);
    }

    private void Retry()
    {
        EggGameManager.Inst.mode =EggGameManager.Mode.ReadyToPlay;
    }
}
