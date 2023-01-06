using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static PlayerInputActions;

public class Test : MonoBehaviour
{

<<<<<<< Updated upstream
    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
<<<<<<< Updated upstream
       
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 dir =  Camera.main.ScreenToWorldPoint(inputActions.Input.Pos.ReadValue<Vector2>())- transform.position ; // 너가 원하는 지점 좌표
     
        dir = Vector3.Slerp(transform.up, dir, 0.1f);
        Debug.Log(dir);
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward) * Quaternion.Euler(0, 0, -90.0f);
        transform.rotation = rotation;
    }

    private void OnEnable()
    {
        inputActions.Input.Enable();
=======
        rigid.velocity = Vector2.up * 1000.0f;
>>>>>>> Stashed changes
    }
=======
>>>>>>> Stashed changes
}
