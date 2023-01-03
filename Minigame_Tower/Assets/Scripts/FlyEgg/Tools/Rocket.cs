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
    [SerializeField]
    GameObject fire;

    private void Start()
    {
        fire.SetActive(false);
        egg = FindObjectOfType<Egg>();
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
        if (toolSlot != null)
        {
            Vector3 mousePosition = new Vector3(Input.mousePosition.x,
    Input.mousePosition.y, -0.1f);
            Vector3 objPosition = Camera.main.ScreenToWorldPoint(mousePosition);
            offSet = transform.position - (Vector3)objPosition;
        }
    }


    private void OnMouseUp()
    {
        if (toolSlot != null)
        {
            transform.position = transform.parent.position;
        }
    }
    void OnMouseDrag()
    {
        Vector3 mousePosition = new Vector3(Input.mousePosition.x,
Input.mousePosition.y, -0.1f);
        Vector3 objPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        transform.position = objPosition + offSet;
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
    { BoxCollider2D box = transform.GetComponent<BoxCollider2D>();
        box.isTrigger = false;
        box.size = new Vector2(0.5f, 0.31f);
        box.offset = new Vector2(0, 0.5f);
        transform.GetComponent<Rigidbody2D>().isKinematic = false;
        transform.GetComponent<Rigidbody2D>().velocity = egg.LastVelocity;
       

    }

}
