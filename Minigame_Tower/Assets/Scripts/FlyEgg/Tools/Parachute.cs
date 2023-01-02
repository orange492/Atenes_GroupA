using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UIElements;

public class Parachute : MonoBehaviour
{
    Egg egg;
    Vector3 movingPos = Vector3.zero;
    bool isParachuteOn = false;
    bool isParachuteSeparate = false;

    float moveSpeed = 1.0f;
    float rotateSpeed = 0.5f;
    float parachuteDownDegree = 55.0f;




    UI_Parachute uI_Parachute;
    // Start is called before the first frame update
    void Start()
    {
        this.gameObject.SetActive(false);
        egg = FindObjectOfType<Egg>();
        uI_Parachute = FindObjectOfType<UI_Parachute>();
        uI_Parachute.onParachute += OnParachute;
        egg.onParachuteSeparate += onParachuteSeparate;
    }

    // Update is called once per frame

    private void Update()
    {
        if (isParachuteSeparate)
        {
            if (transform.position.y > -4.0f)
            {
                transform.position -= moveSpeed * Time.deltaTime * Vector3.up;
                transform.Rotate(Vector3.forward, 0 - transform.rotation.z);
            }
            else
            {
                if (transform.rotation.eulerAngles.z < 180.0f)
                {
                    if (transform.rotation.eulerAngles.z < parachuteDownDegree)
                    {
                        transform.Rotate(Vector3.forward, rotateSpeed);
                    }
                }
                else
                {
                    if (transform.rotation.eulerAngles.z > 360-parachuteDownDegree)
                    {
                        transform.Rotate(Vector3.forward, -rotateSpeed);
                    }
                }
            }
        }
    }
    void FixedUpdate()
    {
        if (isParachuteOn)
        {
            movingPos -= egg.transform.position;
            transform.rotation = quaternion.Euler(0, 0, -(float)Math.Atan2(movingPos.x, movingPos.y));
            if (egg.Rigid.velocity.magnitude > 5.0f)
            {
                float stretchScale = egg.Rigid.velocity.magnitude * 0.1f;
                if (stretchScale < 1.0f)
                {
                    stretchScale = 1.0f;
                }
                if (stretchScale>1.5f)
                {
                    stretchScale = 1.5f;
                }
                transform.localScale = new Vector3(1/stretchScale, 1*stretchScale, 1);
            }
            else
            {
                transform.localScale = Vector3.one;
            }
                 
            movingPos = egg.transform.position;
        }

    }

    void OnParachute()
    {
        isParachuteOn = true;
        this.gameObject.SetActive(true);
        egg.Rigid.drag *= 5.0f;
    }

    void onParachuteSeparate()
    {
        if (isParachuteOn)
        {
            isParachuteOn = false;
            isParachuteSeparate = true;
            //transform.rotation = quaternion.Euler(0, 0, Mathf.PI / 180 * 55.0f);
            //transform.position = new UnityEngine.Vector3(transform.position.x, -4, 0);
            egg.Rigid.drag = 0.1f;
            transform.localScale = Vector3.one;
            transform.SetParent(null);
        }
    }
}
