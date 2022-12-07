using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class Egg : MonoBehaviour
{

    Rigidbody2D rigid;
   

    Transform tools;

    public Action onParachuteSeparate;


    public Rigidbody2D Rigid
    {
        get => rigid;
        set => rigid = value;
    }
    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        tools = transform.GetChild(0).transform;
    }

    private void Update()
    {
  
    }

    private void FixedUpdate()
    {
        
    }




    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Ground"))
        {
            onParachuteSeparate?.Invoke();
        }
        if (rigid.velocity.magnitude > 10.0f)
        {
            
            Debug.Log("알이 깨졌음");
            //Mathf.Abs(rigid.velocity.x) + Mathf.Abs(rigid.velocity.y);
        }
        
    }

    public void EggMove(UnityEngine.Vector2 moveDir,ForceMode2D forceMode2D=ForceMode2D.Impulse)
    {
        rigid.AddForce(moveDir,forceMode2D);
    }


}
