using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using System.Numerics;
using Vector3 = UnityEngine.Vector3;
using Vector2 = UnityEngine.Vector2;
using Quaternion = UnityEngine.Quaternion;

public class Propeller : MonoBehaviour
{
    bool machineOn = false;

    Transform propellar;
    float propellerRotateSpeed = 1.0f;
    public float force = 4.3f;
    public float rotateSpeed = 1.0f;
    Egg egg;

    public bool MachineOn
    {
        get => machineOn;
        set => machineOn = value;
    }



    private void Awake()
    {
        propellar = transform.GetChild(0).transform;
    }
    private void Start()
    {
        egg = FindObjectOfType<Egg>();
        
    }

    void FixedUpdate()
    {
        
        if(machineOn)
        {
            propellerRotateSpeed += 5 * Time.fixedDeltaTime;
            if (propellerRotateSpeed > 15.0f)
            {
                propellerRotateSpeed = 15.0f;
            }
        }
        else
        {
                propellerRotateSpeed -= Time.fixedDeltaTime * 3.0f;
                if (propellerRotateSpeed < 0.0f)
                {
                    propellerRotateSpeed = 0.0f;
                }
        }
        propellar.Rotate(transform.forward, propellerRotateSpeed);
        egg.EggMove(propellerRotateSpeed * transform.up / force, ForceMode2D.Force);
    }



}
