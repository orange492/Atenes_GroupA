using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI_Play : MonoBehaviour
{
    Button button;
    Shop shop;
    private void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(Play);
    }
    private void Start()
    {
        shop = FindObjectOfType<Shop>();
        EggGameManager.Inst.onModeChange += ModeChange;
    }

    private void ModeChange(EggGameManager.Mode obj)
    {
     
            gameObject.SetActive(obj == EggGameManager.Mode.ReadyToPlay);

    }

    private void Play()
    {  

        EggGameManager.Inst.mode = EggGameManager.Mode.Play;
    }
}
