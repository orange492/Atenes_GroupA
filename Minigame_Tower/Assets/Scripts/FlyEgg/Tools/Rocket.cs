
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Rocket : MonoBehaviour
{
    bool machineOn = false;

  float force = 15.0f;
    Egg egg;

    ToolSlot toolSlot;
    Vector3 offSet;

    MouseFollow mouse;

    public bool MachineOn
    {
        get => machineOn;
        set
        {
            machineOn = value;
            if (machineOn)
            {
                fire.SetActive(true);
            }
            else
            {
                fire.SetActive(false);
            }
        }
    }
    bool isDeadOn = false;

    Shop shop;
    [SerializeField]
    GameObject fire;

    private void Start()
    {
        fire.SetActive(false);
        egg = FindObjectOfType<Egg>();
        mouse = FindObjectOfType<MouseFollow>();
        shop = FindObjectOfType<Shop>();
    }

    private void FixedUpdate()
    {
        if (!egg.IsDead)
        {
            if (machineOn)
            {
                egg.EggMove(-transform.up * force, ForceMode2D.Force);
            }
        }
        else
        {
            if (!isDeadOn)
            {
                RigidOn();
                isDeadOn = true;
            }
        }
    }

    private void OnMouseDown()
    {
        toolSlot = transform.parent.GetComponent<ToolSlot>();
        if (toolSlot != null&& !shop.IsItemOnMouse)
        {
            Vector3 mousePosition = new Vector3(Input.mousePosition.x,
    Input.mousePosition.y, -0.1f);
            Vector3 objPosition = Camera.main.ScreenToWorldPoint(mousePosition);
            offSet = transform.position - (Vector3)objPosition;
        }
    }


    private void OnMouseUp()
    {
        if (toolSlot != null && !shop.IsItemOnMouse)
        {
            transform.position = transform.parent.position;
        }
    }
    void OnMouseDrag()
    {
        if (toolSlot != null && !shop.IsItemOnMouse)
        {
            Vector3 mousePosition = new Vector3(Input.mousePosition.x,
Input.mousePosition.y, -0.1f);
            Vector3 objPosition = Camera.main.ScreenToWorldPoint(mousePosition);
            transform.position = objPosition + offSet;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {

        if (collision.transform.CompareTag("Trash"))
        {
            Debug.Log("in");
            if (toolSlot != null)
            {
                toolSlot.DestroyItem();
            }
        }

    }
    public void RigidOn()
    {
        transform.GetComponent<PolygonCollider2D>().isTrigger = false;
        transform.GetComponent<Rigidbody2D>().isKinematic = false;
        transform.GetComponent<Rigidbody2D>().velocity = egg.LastVelocity;
       

    }

}
