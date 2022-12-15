using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Unity.VisualScripting;

public class Spring : MonoBehaviour
{
    DG.Tweening.Sequence rotatioonSequence;
    private void Start()
    {
        rotatioonSequence = DOTween.Sequence().SetAutoKill(false).Pause();
        rotatioonSequence.Append(transform.DORotate(Vector3.forward * 90.0f, 0.1f));
        rotatioonSequence.Append(transform.DORotate(Vector3.forward, 1.0f));
        rotatioonSequence.OnComplete(() => { rotatioonSequence.Rewind(); });

    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        Rigidbody2D targetRigid = collision.GetComponent<Rigidbody2D>();
        if (targetRigid != null)
        {
            if (!rotatioonSequence.IsPlaying())
            {
                rotatioonSequence.Play();
                ThrowTarget(targetRigid);
            }
            
        }    
    }

    

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<Rigidbody2D>() != null)
        {
        }
    }

    void ThrowTarget(Rigidbody2D rigid)
    {
        rigid.AddTorque(1.0f,ForceMode2D.Impulse);
        rigid.AddForce(Vector2.up*10.0f,ForceMode2D.Impulse);
        rigid.AddForce(Vector2.left*20.0f,ForceMode2D.Impulse);
    }
}
