using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal1 : MonoBehaviour
{
    PortalParent portalParent;
    Egg egg;
    private void Awake()
    {
        portalParent = GetComponentInParent<PortalParent>();
        egg = FindObjectOfType<Egg>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Egg"))
        {
            egg.Rigid.position = portalParent.Portal2Transform.position + egg.MovingPos.normalized ;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        
    }
}
