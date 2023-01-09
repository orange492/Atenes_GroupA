using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;

public class Trash : MonoBehaviour
{
    [SerializeField]
    Material[] materials;
    SpriteRenderer spriteRenderer;



    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void Start()
    {
        if (EggGameManager.Inst != null)
        {
            EggGameManager.Inst.onModeChange += ModeChange;
        }
        
    }


    private void ModeChange(EggGameManager.Mode obj)
    {

        gameObject.SetActive(false);
        if (obj == EggGameManager.Mode.ReadyToPlay)
        {
            gameObject.SetActive(true);
        }
        if (obj == EggGameManager.Mode.Editting)
        {
            gameObject.SetActive(true);
        }
    }
    private void OnEnable()
    {

    }

    private void OnDestroy()
    {
        if (EggGameManager.Inst != null)
        {
            EggGameManager.Inst.onModeChange -= ModeChange;
        }
    }

    private void Update()
    {
        //transform.position = (Vector2)Camera.main.transform.position + new Vector2(16.0f, -8.2f)*Camera.main.orthographicSize*0.1f;
        //transform.localScale = new Vector3(Camera.main.orthographicSize*0.1f, Camera.main.orthographicSize*0.1f, 0);
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
