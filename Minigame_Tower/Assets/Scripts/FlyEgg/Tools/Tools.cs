using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class Tools : MonoBehaviour
{

    Egg egg;

    ToolSlot[] toolSlots;
    bool isAllSlotUsed = false;

    public bool IsAllSlotUsed
    {
        get => isAllSlotUsed;
        set => isAllSlotUsed = value;
    }


    private void Awake()
    {
        toolSlots = new ToolSlot[8];
        for (int i = 0; i < transform.childCount; i++)
        {
            toolSlots[i] = transform.GetChild(i).GetComponent<ToolSlot>();
            toolSlots[i].SlotIndex = i;

        }
    }

    private void Start()
    {
        egg = FindObjectOfType<Egg>();
       
    }

   

    private void Update()
    {
        if (!egg.IsDead)
        {
            transform.position = egg.transform.position;
        }
        IsAllSlotUsed = toolSlots.All(x => x.IsSlotUsed);
    }

    public void SlotUse(int index,bool isUse=true)
    {
        toolSlots[index].IsSlotUsed = isUse;
    }




}
