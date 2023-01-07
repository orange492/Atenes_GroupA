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
<<<<<<< Updated upstream
        EggGameManager.Inst.onModeChange += ModeChange;
=======
        cameraFollow = FindObjectOfType<CameraFollow>(); 
        if (EggGameManager.Inst != null)
        {
            EggGameManager.Inst.onModeChange += ModeChange;
        }
>>>>>>> Stashed changes
    }

    private void ModeChange(EggGameManager.Mode obj)
    {
<<<<<<< Updated upstream
     
            gameObject.SetActive(obj == EggGameManager.Mode.ReadyToPlay);
=======
        Debug.Log("reday");
        gameObject.SetActive(obj == EggGameManager.Mode.ReadyToPlay);
>>>>>>> Stashed changes

    }
    private void OnEnable()
    {
     
    }

<<<<<<< Updated upstream
    private void Play()
    {  
=======
    private void OnDestroy()
    {
        if (EggGameManager.Inst != null)
        {
            EggGameManager.Inst.onModeChange -= ModeChange;
        }
    }
>>>>>>> Stashed changes

    private void Play()
    {
        EggGameManager.Inst.mode = EggGameManager.Mode.Play;
    }
}
