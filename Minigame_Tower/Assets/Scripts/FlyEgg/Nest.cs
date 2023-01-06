using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nest : MonoBehaviour
{
    Egg egg;
    bool isGameClear = false;
    private void Start()
    {
        egg = FindObjectOfType<Egg>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Egg"))
        {
            if (egg.Mag < 5.0f)
            {
                isGameClear = true;
                egg.Rigid.velocity = Vector2.zero;
                egg.Rigid.angularVelocity = 0.0f;
            }
        }
    }

 

    private void Update()
    {
        if (isGameClear)
        {

            egg.transform.position = Vector3.Lerp(egg.transform.position, transform.position, Time.deltaTime);
        }
    }
}
