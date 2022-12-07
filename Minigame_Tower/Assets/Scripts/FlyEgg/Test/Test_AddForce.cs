using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Test_AddForce : TestBase
{
    Egg egg;

    protected override void Awake()
    {
        base.Awake();
        egg = FindObjectOfType<Egg>();
    }
    protected override void Test1(InputAction.CallbackContext _)
    {
        Debug.Log("up");
        egg.EggMove(Vector2.up*100.0f);
    }
    protected override void Test2(InputAction.CallbackContext _)
    {
        
    }
    protected override void Test3(InputAction.CallbackContext _)
    {
      
    }
    protected override void Test4(InputAction.CallbackContext _)
    {
       
    }
    protected override void Test5(InputAction.CallbackContext _)
    {
       
    }
}
