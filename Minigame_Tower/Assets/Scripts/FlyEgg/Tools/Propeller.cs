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

    Vector3 offSet;
    ToolSlot toolSlot;
    public bool MachineOn
    {
        get => machineOn;
        set => machineOn = value;
    }

    bool isDeadOn = false;



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
        if (!egg.IsDead)
        {
            if (machineOn)
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
        else
        {
            if (!isDeadOn)
            {
                RigidOn();
                isDeadOn = true;
                
            }
        }
    }

    private void OnMouseDown()
    {
        toolSlot = transform.parent.GetComponent<ToolSlot>();
        if (toolSlot != null)
        {
            Vector3 mousePosition = new Vector3(Input.mousePosition.x,
    Input.mousePosition.y, -0.1f);
            Vector3 objPosition = Camera.main.ScreenToWorldPoint(mousePosition);
            offSet = transform.position - (Vector3)objPosition;
        }
    }


    private void OnMouseUp()
    {
        if (toolSlot != null)
        {
            transform.position = transform.parent.position;
        }
    }
    void OnMouseDrag()
    {
        Vector3 mousePosition = new Vector3(Input.mousePosition.x,
Input.mousePosition.y, -0.1f);
        Vector3 objPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        transform.position = objPosition + offSet;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {

        if (collision.transform.CompareTag("Trash"))
        {
            Debug.Log("in");
             if (toolSlot != null)
            {
                toolSlot.DestroyItem();
            }
        }

    }

    public void RigidOn()
    {
        transform.GetComponent<CapsuleCollider2D>().isTrigger = false;
        transform.GetComponent<Rigidbody2D>().isKinematic = false;
        transform.GetComponent<Rigidbody2D>().velocity = egg.LastVelocity;

    }



}
