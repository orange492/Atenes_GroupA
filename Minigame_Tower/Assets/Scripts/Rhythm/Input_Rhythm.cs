using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Input_Rhythm : MonoBehaviour
{
    InputActions inputActions;

    void Awake()
    {
        inputActions = new InputActions();

    }

    private void OnEnable()
    {
        inputActions.Touch.Enable();
        inputActions.Touch.Test.performed += TestClick; 
        inputActions.Touch.Test2.performed += Test2Click; 
        inputActions.Touch.Up.performed += UpClick; 
        inputActions.Touch.Down.performed += DownClick; 
    }

    private void OnDisable()
    {
        inputActions.Touch.Test.performed -= TestClick;
        inputActions.Touch.Test2.performed -= Test2Click;
        inputActions.Touch.Up.performed -= UpClick;
        inputActions.Touch.Down.performed -= DownClick;
        inputActions.Touch.Disable();
    }
    private void TestClick(InputAction.CallbackContext content)
    {
        Manager_Rhythm.Inst.CreateNote(0);
    }
    private void Test2Click(InputAction.CallbackContext content)
    {
        Manager_Rhythm.Inst.CreateNote(1);
    }

    private void UpClick(InputAction.CallbackContext content)
    {
        Manager_Rhythm.Inst.ClickKey(0);
    }

    private void DownClick(InputAction.CallbackContext content)
    {
        Manager_Rhythm.Inst.ClickKey(1);
    }
}
