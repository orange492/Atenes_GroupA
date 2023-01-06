using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;
using Vector2 = UnityEngine.Vector2;

public class Egg : MonoBehaviour
{

    Rigidbody2D rigid;
    float mag = 0.0f;
    public float Mag => mag;
    DG.Tweening.Sequence scaleSquence;
    public Action onParachuteSeparate;
    Vector3 eggPos=Vector3.zero;
    Vector3 movingPos=Vector3.zero;

  UnityEngine.Vector2 lastVelocity;

    public Vector2 LastVelocity => lastVelocity;

    bool isDead = false;

    
    

    public bool IsDead => isDead;

    [SerializeField]
    GameObject eggDie;
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
        EggGameManager.Inst.onModeChange += ModeChange;
        rigid.isKinematic = true;
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
     
        if (mag > 20.0f)
        {
            Die();
        }
        
    }
    private void ModeChange(EggGameManager.Mode obj)
    {
        if (obj == EggGameManager.Mode.Play)
        {
            rigid.isKinematic = false;
        }
        else
        {
            rigid.isKinematic = true;
        }
    }
    void Die()
    {
      GameObject obj=  Instantiate(eggDie, transform.position, transform.rotation);
        for (int i = 0; i < eggDie.transform.childCount; i++)
        {
            Rigidbody2D rigiddieegg = obj.transform.GetChild(i).GetComponent<Rigidbody2D>();
            Debug.Log(rigiddieegg.transform.name);
            rigiddieegg.velocity =new UnityEngine.Vector2(rigid.velocity.x,-MathF.Abs(rigid.velocity.y));
            Debug.Log(rigiddieegg.velocity);
            
        }
        lastVelocity = new UnityEngine.Vector2(rigid.velocity.x, -MathF.Abs(rigid.velocity.y));

        isDead = true;
        EggGameManager.Inst.mode = EggGameManager.Mode.Die;
        //Mathf.Abs(rigid.velocity.x) + Mathf.Abs(rigid.velocity.y);
        Destroy(this.gameObject);
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

    public void RigidBodyOff()
    {
        rigid.isKinematic = true;
    }


}
