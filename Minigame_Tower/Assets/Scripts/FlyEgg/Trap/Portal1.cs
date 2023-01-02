using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal1 : MonoBehaviour
{
    PortalParent portalParent;
    Egg egg;
    Portal2 portal2;

    bool isReceived = false;
    
    public bool IsReceived
    {
        get => isReceived;
        set => isReceived = value;
    }
    private void Awake()
    {
        portalParent = GetComponentInParent<PortalParent>();
        egg = FindObjectOfType<Egg>();
        portal2 = portalParent.transform.GetChild(1).GetComponent<Portal2>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isReceived)
        {
            if (collision.CompareTag("Egg"))
            {
                egg.Rigid.position = portalParent.Portal2Transform.position; //+ egg.MovingPos.normalized;
                egg.ScaleEffect();
                portal2.IsReceived = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Egg"))
        {
            isReceived = false;
        }
    }
}
