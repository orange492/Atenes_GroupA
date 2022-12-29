using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal2 : MonoBehaviour
{
    PortalParent portalParent;
    Egg egg;
    Portal1 portal1;

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
        portal1 = portalParent.transform.GetChild(0).GetComponent<Portal1>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isReceived)
        {
            if (collision.CompareTag("Egg"))
            {
                egg.Rigid.position = portalParent.Portal1Transform.position; //+ egg.MovingPos.normalized;
                egg.ScaleEffect();
                portal1.IsReceived = true ;
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
