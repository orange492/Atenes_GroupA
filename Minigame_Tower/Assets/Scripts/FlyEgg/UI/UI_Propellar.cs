using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Propellar : MonoBehaviour
{
   
   
    Button propellarButton;
    Egg egg;
   


    private void Awake()
    {
        propellarButton = GetComponent<Button>();
        egg = FindObjectOfType<Egg>();
    }

}
