using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Test_SlingShot : TestBase
{
    SlingShot slingShot;

    protected override void Awake()
    {
        base.Awake();
        slingShot = FindObjectOfType<SlingShot>();
    }
    // Start is called before the first frame update
    protected override void Test1(InputAction.CallbackContext _)
    {
       
        slingShot.isEggOnSlingShot = true;
    }
}
