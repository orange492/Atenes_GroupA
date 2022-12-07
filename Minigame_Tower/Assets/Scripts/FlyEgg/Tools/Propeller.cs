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
    public bool isButtonDowning;

    Transform propellar;
    float propellerRotateSpeed = 1.0f;
    float force = 4.3f;
    public float rotateSpeed = 1.0f;
    Egg egg;


    Vector3 movingPos = Vector3.zero;


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
        if (!isButtonDowning)
        {
            propellerRotateSpeed -= Time.fixedDeltaTime * 3.0f;
            if (propellerRotateSpeed < 0.0f)
            {
                propellerRotateSpeed = 0.0f;
            }
            
            //float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            //Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            //transform.rotation = rotation;
            

            //transform.rotation = Quaternion.LookRotation(dir);
            
        }
        else
        {
            propellerRotateSpeed += 5 * Time.fixedDeltaTime;
            if (propellerRotateSpeed > 15.0f)
            {
                propellerRotateSpeed = 15.0f;
            }
            transform.Rotate(Vector3.forward, 0 - transform.rotation.z);
            
        }

        movingPos -= egg.transform.position;
        Vector3 dir = -movingPos;

        dir = Vector3.Slerp(transform.right, dir, 0.1f);
        Debug.Log(dir);
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward) * Quaternion.Euler(0, 0, 0);
        transform.rotation = rotation;

     
        movingPos = egg.transform.position;

        propellar.Rotate(transform.forward, propellerRotateSpeed);
        egg.EggMove(propellerRotateSpeed * transform.up / force, ForceMode2D.Force);
    }


    private void Update()
    {
        
    }
    public void PointerDown()
    {
        isButtonDowning = true;
    }

    public void PointerUp()
    {
        isButtonDowning = false;
    }
}
