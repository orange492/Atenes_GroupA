using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Parachute : MonoBehaviour
{
    Button parachuteButton;
    Egg egg;
    bool isParachuted = false;
    bool isParachuteSeparate = false;
    public Action onParachute;
    

    private void Awake()
    {
        parachuteButton = GetComponent<Button>();
        parachuteButton.onClick.AddListener(OnParachute);
        egg = FindObjectOfType<Egg>();
    }

    private void OnParachute()
    {
        if (!isParachuted)
        {
            onParachute?.Invoke();
            isParachuted = true;
        }
        else
        {
            if (!isParachuteSeparate)
            {
                isParachuteSeparate = true;
                egg.onParachuteSeparate?.Invoke();
            }
        }
        
        
    }
}
