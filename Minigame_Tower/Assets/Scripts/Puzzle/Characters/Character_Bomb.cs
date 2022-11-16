using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_Bomb : Character_Base
{
    
   protected override void Start()
    {
     
     
        
    }

    protected override void Awake()
    {
        
        charaterType = CharaterType.Bomb;
        AnimalType = -2;
        temp = AnimalType;
    }
}
