using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class EggController : MonoBehaviour
{
    Egg egg;
    PlayerInputActions inputActions;
    Vector2 onClickPosition;
    Vector2 offClickPostion;
    

    private void Awake()
    {
        egg = transform.GetComponent<Egg>();
        inputActions = new PlayerInputActions();
    }

    private void OnEnable()
    {
       
    }


    private void OnDisable()
    {
      
    }
    private void OnClick(InputAction.CallbackContext obj)
    {
        onClickPosition= Mouse.current.position.ReadValue();

    }
    private void OffClick(InputAction.CallbackContext obj)
    {
        offClickPostion = Mouse.current.position.ReadValue();
        //egg.EggMove((onClickPosition-offClickPostion)*10.0f);
        MoveDirReset();
    }

    void MoveDirReset()
    {
        onClickPosition = Vector2.zero;
        offClickPostion = Vector2.zero;
    }



}
