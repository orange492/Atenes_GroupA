using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DetectionButton : MonoBehaviour
{
    Button detectionButton;
    public Detection detection;
    

    private void Awake()
    {
        
        detectionButton = GetComponent<Button>();
        detectionButton.onClick.AddListener(OnDetect);
    }
    private void Start()
    {
    }

    private void OnDetect()
    {
        if(EggGameManager.Inst.mode != EggGameManager.Mode.Play)
        {
            return;
        }
        if (!detection.IsOnDetective)
        {
            detection.IsOnDetective = true;
        }


    }
}
