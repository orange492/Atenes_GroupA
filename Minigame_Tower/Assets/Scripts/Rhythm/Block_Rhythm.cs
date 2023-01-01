using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block_Rhythm : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Line"))
        {
            Sound_Rhythum.Inst.PlayNoteSound();
        }
    }
}
