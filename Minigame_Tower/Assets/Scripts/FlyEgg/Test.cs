using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static PlayerInputActions;

public class Test : MonoBehaviour
{
    PlayerInputActions inputActions;
    // Start is called before the first frame update
    private void Awake()
    {
        inputActions = new PlayerInputActions();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 dir =  Camera.main.ScreenToWorldPoint(inputActions.Input.Pos.ReadValue<Vector2>())- transform.position ; // 너가 원하는 지점 좌표

        //dir = Vector3.Slerp(transform.up, dir, 0.1f);
        float angle = Vector3.SignedAngle(Vector3.up, dir - transform.up, -Vector3.forward);
        //Debug.Log(dir);
        //Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward) * Quaternion.Euler(0, 0, -90.0f);
        transform.Rotate(Vector3.forward, angle);
    }

    private void OnEnable()
    {
        inputActions.Input.Enable();
    }
}
