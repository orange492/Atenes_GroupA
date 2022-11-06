using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class JUMP : MonoBehaviour
{
    public float JumpPower = 300.0f;
    [SerializeField] private LayerMask platformLayerMask;
    BoxCollider2D boxCollider2D;
    bool die = false;  // 새로 추가된 부분

    string recentCollisionObjectName = "Ground"; // 새로 추가된 내용
    private void Awake()
    {
        boxCollider2D = GetComponent<BoxCollider2D>();
    }
    void Update()
    {
        IsGrounded();
        if (Input.GetMouseButtonDown(0) == true && die == false) // 새로 추가된 부분
        {
            if (IsGrounded())
            {
                GetComponent<Rigidbody2D>().AddForce(new Vector2(0, JumpPower));
            }
        }
    }

    public void Die() // 새로 추가된 부분
    {
        die = true;
    }

    public bool IsGrounded()
    {
        float extraHeight = 0.1f;
        RaycastHit2D raycastHit =
                       Physics2D.Raycast(
                                boxCollider2D.bounds.center,
                                Vector2.down,
                                boxCollider2D.bounds.extents.y + extraHeight,
                                platformLayerMask);

        Color rayColor;
        if (raycastHit.collider != null)
        {
            // 새로 추가된 내용
            if (raycastHit.collider.name != "Ground" &&
                raycastHit.collider.name != recentCollisionObjectName)
            {
                FindObjectOfType<ScoreText>().AddPoint();
                recentCollisionObjectName = raycastHit.collider.name;
            }

            rayColor = Color.green;
        }
        else
        {
            rayColor = Color.red;
        }

        UnityEngine.Debug.DrawRay(
                         boxCollider2D.bounds.center,
                         Vector2.down * (boxCollider2D.bounds.extents.y + extraHeight),
                         rayColor);

        UnityEngine.Debug.Log(raycastHit.collider);

        if (raycastHit.collider != null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}