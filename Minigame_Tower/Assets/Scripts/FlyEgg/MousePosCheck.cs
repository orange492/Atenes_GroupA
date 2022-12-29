using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MousePosCheck : MonoBehaviour
{
    bool mouseOn = false;
    public bool MouseOn => mouseOn;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Mouse"))
        {
            mouseOn = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Mouse"))
        {
            mouseOn = false;
        }
    }
}
