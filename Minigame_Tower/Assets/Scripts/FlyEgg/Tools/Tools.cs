using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Tools : MonoBehaviour
{

    Egg egg;
  
    private void Start()
    {
        egg = FindObjectOfType<Egg>();

    }

    private void FixedUpdate()
    {
    }

    private void Update()
    {
        
        transform.position = egg.transform.position;
    }


}
