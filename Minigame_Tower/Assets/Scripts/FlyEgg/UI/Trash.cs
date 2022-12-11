using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Trash : MonoBehaviour
{
    [SerializeField]
    Material[] materials;
    SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Line"))
        {
            spriteRenderer.material = materials[0];
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Line"))
        {
            spriteRenderer.material = materials[1];
        }
    }

    public void MaterialChange()
    {
        spriteRenderer.material = materials[1];
    }
    

}
