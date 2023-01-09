using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Fuel : MonoBehaviour
{
    Slider fuelSlider;
    TextMeshProUGUI fuelText;

    
    float maxFuel=1500.0f;
    public float currentFuel = 1500.0f;

    public float CurrentFuel
    {
        get => currentFuel;
        set

        {
            currentFuel = value;
            currentFuel= Mathf.Clamp(currentFuel, 0.0f, maxFuel);
        }
    }

    float energyCost = 0.0f;

    public float EnergyCost
    {
        get => energyCost;
        set => energyCost = value;
    }

    


    private void Awake()
    {
        fuelSlider = transform.GetChild(0).GetComponent<Slider>();
        fuelText = transform.GetChild(1).GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        currentFuel = maxFuel;
        fuelText.text = $"{currentFuel:f0}/{maxFuel:f0}";
        fuelSlider.maxValue = maxFuel;
        fuelSlider.value = CurrentFuel; 
        if (EggGameManager.Inst != null)
        {
            EggGameManager.Inst.onModeChange += ModeChange;
        }
        if (EggGameManager.Inst.mode == EggGameManager.Mode.Intro)
        {
            transform.gameObject.SetActive(false);
        }
    }
    private void OnDestroy()
    {
        if (EggGameManager.Inst != null)
        {
            EggGameManager.Inst.onModeChange -= ModeChange;
        }
    }

    private void ModeChange(EggGameManager.Mode obj)
    {
        if (obj == EggGameManager.Mode.Intro)
        {
            transform.gameObject.SetActive(false);
        }
        else
        {
            transform.gameObject.SetActive(true);
        }
    }

    private void Update()
    {
        CurrentFuel -= Time.deltaTime*energyCost;
        fuelText.text = $"{currentFuel:f0}/{maxFuel:f0}";
        fuelSlider.value = CurrentFuel;

    }


}
