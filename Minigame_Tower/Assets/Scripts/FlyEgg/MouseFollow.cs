using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PlayerInputActions;

public class MouseFollow : MonoBehaviour
{
    PlayerInputActions inputActions;

    private void Awake()
    {
        inputActions = new PlayerInputActions();
    }

    private void OnEnable()
    {
        inputActions.Input.Enable();
    }
    private void Update()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(inputActions.Input.Pos.ReadValue<Vector2>());
        mousePos = new Vector3(mousePos.x, mousePos.y, 10);
        transform.position = mousePos;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name);
    }

}
