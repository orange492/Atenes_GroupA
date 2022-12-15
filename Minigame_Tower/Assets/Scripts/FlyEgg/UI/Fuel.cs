using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Fuel : MonoBehaviour
{
    Slider fuelSlider;
    TextMeshProUGUI fuelText;

    
    float maxFuel=1000.0f;
    float currentFuel = 1000.0f;

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
    }

    private void Update()
    {
        CurrentFuel -= Time.deltaTime*energyCost;
        fuelText.text = $"{currentFuel:f0}/{maxFuel:f0}";
        fuelSlider.value = CurrentFuel;

    }


}
