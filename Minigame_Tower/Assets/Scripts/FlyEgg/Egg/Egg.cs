using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class Egg : MonoBehaviour
{

    Rigidbody2D rigid;
    float mag = 0.0f;
    DG.Tweening.Sequence scaleSquence;
    public Action onParachuteSeparate;
    Vector3 eggPos=Vector3.zero;
    Vector3 movingPos=Vector3.zero;

    public Vector3 MovingPos => movingPos;

    public Rigidbody2D Rigid
    {
        get => rigid;
        set => rigid = value;
    }
    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        scaleSquence = DOTween.Sequence().SetAutoKill(false).Pause();
        scaleSquence.Append(transform.DOScale(0.1f,0.2f));
        scaleSquence.Append(transform.DOScale(1.0f, 0.2f));
        scaleSquence.OnComplete(() => { scaleSquence.Rewind(); });
    }

    private void Update()
    {
        
    }

    private void FixedUpdate()
    {
        mag = rigid.velocity.magnitude;
        eggPos -= transform.position;
        movingPos = -eggPos;
        eggPos = transform.position;
    }




    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Ground"))
        {
            onParachuteSeparate?.Invoke();
        }
        if (mag > 15.0f)
        {
            
            Debug.Log("알이 깨졌음");
            //Mathf.Abs(rigid.velocity.x) + Mathf.Abs(rigid.velocity.y);
        }
        
    }

    public void EggMove(UnityEngine.Vector2 moveDir,ForceMode2D forceMode2D=ForceMode2D.Force)
    {
        rigid.AddForce(moveDir,forceMode2D);
    }

    public void EggRotate(float force)
    {
        rigid.AddTorque(force,ForceMode2D.Force);
    }

    public void ScaleEffect()
    {
        if (!scaleSquence.IsPlaying())
        {
            scaleSquence.Play();
        }
    }


}
