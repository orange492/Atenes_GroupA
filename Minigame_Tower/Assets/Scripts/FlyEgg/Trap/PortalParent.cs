using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalParent : MonoBehaviour
{
    Transform portal1Transform;
    Transform portal2Transform;

    public Transform Portal1Transform => portal1Transform;
    public Transform Portal2Transform => portal2Transform;


    private void Awake()
    {
        portal1Transform = transform.GetChild(0).transform;
        portal2Transform = transform.GetChild(1).transform;
    }

}
