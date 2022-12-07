using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParachuteBackPack : MonoBehaviour
{

    bool isOnParachute = false;
    UI_Parachute UI_Parachute;
    float moveSpeed = 5.0f;

    private void Start()
    {
        UI_Parachute = FindObjectOfType<UI_Parachute>();
        UI_Parachute.onParachute += OnParachute;
    }

    private void OnParachute()
    {
        isOnParachute = true;
        this.transform.SetParent(null);
    }

    // Update is called once per frame
    void Update()
    {
        if (isOnParachute)
        {
            if (transform.position.y > -4)
            {
                transform.position -= moveSpeed * Time.deltaTime * Vector3.up;
                transform.Rotate(Vector3.forward, 1.0f);
            }
        }
    }


}
