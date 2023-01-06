using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Propellar : MonoBehaviour
{
   
   
    Toggle toggle;
    Egg egg;
  

    Fuel fuel;
    public Propeller propeller;


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
            propeller.MachineOn = true;
            fuel.EnergyCost += 100.0f;
        }
        else
        {
            propeller.MachineOn = false;
            fuel.EnergyCost -= 100.0f;
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
