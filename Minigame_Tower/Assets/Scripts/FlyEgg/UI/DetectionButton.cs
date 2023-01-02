using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DetectionButton : MonoBehaviour
{
    Button detectionButton;
    Detection detection;
    

    private void Awake()
    {
        
        detectionButton = GetComponent<Button>();
        detectionButton.onClick.AddListener(OnDetect);
    }
    private void Start()
    {
        detection = FindObjectOfType<Detection>();
    }

    private void OnDetect()
    {
        if (!detection.IsOnDetective)
        {
            detection.IsOnDetective = true;
        }


    }
}
