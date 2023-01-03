using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Unity.VisualScripting;
using UnityEngine;

public class Tools : MonoBehaviour
{

    Egg egg;

    ToolSlot[] toolSlots;

 



    private void Start()
    {
        egg = FindObjectOfType<Egg>();
        toolSlots = new ToolSlot[8];
        for (int i = 0; i < transform.childCount; i++)
        {
            toolSlots[i] = transform.GetChild(i).GetComponent<ToolSlot>();
            toolSlots[i].SlotIndex = i;

        }
    }

    private void FixedUpdate()
    {
    }

    private void Update()
    {
        if (!egg.IsDead)
        {
            transform.position = egg.transform.position;
        }
    }

    public void SlotUse(int index,bool isUse=true)
    {
        toolSlots[index].IsSlotUsed = isUse;
    }




}
