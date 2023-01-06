using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Rocket : MonoBehaviour
{


    Toggle toggle;
    Egg egg;


    Fuel fuel;
    public Rocket rocket;


    private void Awake()
    {
        toggle = GetComponent<Toggle>();
        egg = FindObjectOfType<Egg>();
        toggle.onValueChanged.AddListener(MachineOnOff);
        fuel = FindObjectOfType<Fuel>();
    }

    private void MachineOnOff(bool toggleOn)
    {
        if (EggGameManager.Inst.mode != EggGameManager.Mode.Play)
        {
            toggle.isOn = false;
            return;
        }
        if (toggleOn&&!egg.IsDead)
        {
            rocket.MachineOn = true;
            fuel.EnergyCost += 10.0f;
        }
        else
        {
            rocket.MachineOn = false;
            fuel.EnergyCost -= 10.0f;
        }

        if (fuel.CurrentFuel < 0.00025f)
        {
            toggle.isOn = false;
        }
    }

    private void Update()
    {
        if (fuel.CurrentFuel < 0.00025f || egg.IsDead)
        {
            toggle.isOn = false;
        }
    }

}
