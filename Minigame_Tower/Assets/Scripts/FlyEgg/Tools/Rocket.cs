using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    bool machineOn = false;

  float force = 15.0f;
    Egg egg;

    public bool MachineOn
    {
        get => machineOn;
        set
        {
            machineOn = value;
            if (machineOn)
            {
                fire.SetActive(true);
            }
            else
            {
                fire.SetActive(false);
            }
        }
    }

    [SerializeField]
    GameObject fire;

    private void Start()
    {
        fire.SetActive(false);
        egg = FindObjectOfType<Egg>();
    }

    private void FixedUpdate()
    {
        if (machineOn)
        {
            egg.EggMove(-transform.up*force, ForceMode2D.Force);
        }
    }


}
