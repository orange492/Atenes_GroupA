using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class Lava : MonoBehaviour
{
    public float speed = 0.005f;
    public float move_term = 0.25f;
    public float boost_term = 3.0f;
    public float boost_speed = 4.0f;

    GameObject Player;
    BoxCollider2D boxCollider;
    private void Awake()
    {
        Player = FindObjectOfType<JUMP>().gameObject;
        boxCollider = GetComponent<BoxCollider2D>();
    }
    public void Start()
    {
        StartCoroutine(Move());
    }

    public Vector2 GetTopPosition()
    {
        Vector2 topPos;
        topPos = boxCollider.bounds.max;
        return topPos;
    }

    IEnumerator Move()
    {
        while (true)
        {
            float cur_speed = speed;
            if (Player.transform.position.y > GetTopPosition().y + boost_term)
            {
                cur_speed *= boost_speed;
            }

            transform.position = new Vector2(transform.position.x, transform.position.y + cur_speed);
            yield return new WaitForSeconds(move_term);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            UnityEngine.Debug.Log("GameOver");
            Destroy(collision.gameObject);
        }
    }
}