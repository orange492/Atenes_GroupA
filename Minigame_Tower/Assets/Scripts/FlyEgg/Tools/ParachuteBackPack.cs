using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParachuteBackPack : MonoBehaviour
{

    bool isOnParachute = false;
    UI_Parachute UI_Parachute;
    float moveSpeed = 10.0f;
    bool isDeadOn = false;
    Egg egg;
    private void Start()
    {
        UI_Parachute = FindObjectOfType<UI_Parachute>();
        UI_Parachute.onParachute += OnParachute;
        egg = FindObjectOfType<Egg>();
    }

    private void OnParachute()
    {
        isOnParachute = true;
        this.transform.SetParent(null);
    }

    // Update is called once per frame
    void Update()
    {
        if (!egg.IsDead)
        {
            if (isOnParachute)
            {
                //if (transform.position.y > -4)
                //{
                //    transform.position -= moveSpeed * Time.deltaTime * Vector3.up;
                //    transform.Rotate(Vector3.forward, 1.0f);
                //}
                if (!isDeadOn)
                {
                    transform.GetComponent<CapsuleCollider2D>().isTrigger = false;
                    transform.GetComponent<Rigidbody2D>().isKinematic = false;
                    isDeadOn = true;
                }
            }
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
    public void RigidOn()
    {
        transform.GetComponent<CapsuleCollider2D>().isTrigger = false;
        transform.GetComponent<Rigidbody2D>().isKinematic = false;
        transform.GetComponent<Rigidbody2D>().velocity = egg.LastVelocity;

    }




}
