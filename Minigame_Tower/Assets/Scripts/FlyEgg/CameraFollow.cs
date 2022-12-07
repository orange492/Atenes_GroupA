using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class CameraFollow : MonoBehaviour
{
    public float moveSpeed = 3.0f;
  Transform target;
    Vector3 offset=Vector3.zero;
    SlingShot slingShot;
    Vector3 pos;

    private void Awake()
    {
        target = FindObjectOfType<Egg>().transform;
        offset = transform.position - target.position;
        slingShot = FindObjectOfType<SlingShot>();
        pos = transform.position;
    }
    private void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        if (slingShot == null || !slingShot.isEggOnSlingShot)
        {
            // transform.position = Vector3.Lerp(transform.position, target.position + offset, moveSpeed * Time.deltaTime);
            transform.position = target.position + offset;
        }
        else
        {
            transform.position = pos;
        }
    }
}
