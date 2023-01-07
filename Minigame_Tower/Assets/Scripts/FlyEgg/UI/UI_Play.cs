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
    CameraFollow cameraFollow;
    private void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(Play);
    }
    private void Start()
    {
        shop = FindObjectOfType<Shop>();


        cameraFollow = FindObjectOfType<CameraFollow>(); 
        if (EggGameManager.Inst != null)
        {
            EggGameManager.Inst.onModeChange += ModeChange;
        }

    }

    private void ModeChange(EggGameManager.Mode obj)
    {


        gameObject.SetActive(obj == EggGameManager.Mode.ReadyToPlay);



        Debug.Log("reday");
        gameObject.SetActive(obj == EggGameManager.Mode.ReadyToPlay);


    }
    private void OnEnable()
    {
     
    }


    private void OnDestroy()
    {
        if (EggGameManager.Inst != null)
        {
            EggGameManager.Inst.onModeChange -= ModeChange;
        }
    }


    private void Play()
    {

        EggGameManager.Inst.mode = EggGameManager.Mode.Play;
        cameraFollow.SlinShotSet();

    }
}
