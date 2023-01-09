using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Unity.VisualScripting;

public class Spring : MonoBehaviour, ITrap
{
    DG.Tweening.Sequence rotatioonSequence;
    BoxCollider2D boxCollider;
    Mesh mesh;
    MeshFilter mf;

    Transform detectiveArea;
    bool isActive = false;
    bool isDetected = false;

    float timeElapse = 0.0f;
    float blinkTime = 0.2f;
    private void Start()
    {
        rotatioonSequence = DOTween.Sequence().SetAutoKill(false).Pause();
        rotatioonSequence.Append(transform.DORotate(Vector3.forward * 90.0f, 0.1f));
        rotatioonSequence.Append(transform.DORotate(Vector3.forward, 1.0f));
        rotatioonSequence.OnComplete(() => { rotatioonSequence.Rewind(); });
        boxCollider = GetComponent<BoxCollider2D>();
        detectiveArea = transform.GetChild(0).transform;
        mf = detectiveArea.GetComponent<MeshFilter>();

        mesh = boxCollider.CreateMesh(false, false);
        mf.mesh = mesh;
        detectiveArea.gameObject.SetActive(isActive);
    }


    private void Update()
    {
        if (isDetected)
        {
            if (timeElapse > blinkTime)
            {
                isActive = !isActive;
                detectiveArea.gameObject.SetActive(isActive);
                timeElapse = 0.0f;
                blinkTime -= 0.01f;
            }
            if (blinkTime < 0.0f)
            {
                isDetected = false;
                blinkTime = 0.2f;
            }
        }
        else
        {
            isActive = false;
            detectiveArea.gameObject.SetActive(isActive);
        }


        timeElapse += Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Egg"))
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
    }



    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<Rigidbody2D>() != null)
        {
        }
    }

    void ThrowTarget(Rigidbody2D rigid)
    {
        rigid.AddTorque(1.0f, ForceMode2D.Impulse);
        rigid.AddForce(Vector2.up * 20.0f, ForceMode2D.Impulse);
        rigid.AddForce(Vector2.left * 50.0f, ForceMode2D.Impulse);
    }

    public void Visualize()
    {
        isDetected = true;
    }

 
}
