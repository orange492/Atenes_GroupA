using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Net.Sockets;
using UnityEditor;
using UnityEngine;

public class ToolSlot : MonoBehaviour
{

    MouseFollow mouseFollow;
    Tools tools;



    bool isSlotUsed = false;
    public bool IsSlotUsed
    {
        get => isSlotUsed;
        set => isSlotUsed = value;
    }

    bool isItemOn = false;
    Shop shop;

    float itemValue = 0.0f;
    public float ItemValue
    {
        get => itemValue;
        set => itemValue = value;
    }

    int slotIndex = -1;

    public GameObject button;

    Canvas canvas;
    Transform controller;
    public int SlotIndex
    {
        get => slotIndex;
        set => slotIndex = value;
    }
    private void Start()
    {
        mouseFollow = FindObjectOfType<MouseFollow>();
        tools = FindObjectOfType<Tools>();
        canvas = FindObjectOfType<Canvas>();
        controller = canvas.transform.GetChild(0).transform;
        shop = FindObjectOfType<Shop>();
        switch (EggGameManager.Inst.SlotItems[slotIndex])
        {
            case EggGameManager.item.None:
                
                break;
            case EggGameManager.item.Propeller:
                EquipPropeller();
               
                break;
            case EggGameManager.item.Rocket:
                EquipRocket();
                break;
            default:
                break;
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Mouse"))
        {
            if (shop.IsOnChangeMode)
            {
                if (!isSlotUsed)
                {

                    if (collision.transform.childCount != 0)
                    {
                        GameObject item = collision.transform.GetChild(0).gameObject;
                        if (item != null)
                        {
                            item.transform.SetParent(this.transform, false);
                            isItemOn = true;
                            item.transform.position = new Vector3(item.transform.position.x, item.transform.position.y, 0.5f);
                            //transform.position = new Vector3(transform.position.x, transform.position.y, -0.5f);
                        }
                    }

                }
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {

        if (shop.IsOnChangeMode)
        {
            if (!isSlotUsed)
            {
                if (collision.CompareTag("Mouse"))
                {
                    if (collision.transform.childCount != 0)
                    {
                        GameObject item = collision.transform.GetChild(0).gameObject;
                        if (item != null)
                        {
                            item.transform.SetParent(this.transform, false);
                            isItemOn = true;
                            item.transform.position = new Vector3(item.transform.position.x, item.transform.position.y, 0.5f);
                            //transform.position = new Vector3(transform.position.x, transform.position.y, -0.5f);
                        }
                    }
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!isSlotUsed)
        {
            if (collision.CompareTag("Mouse"))
            {
                if (transform.childCount != 0)
                {
                    GameObject item = transform.GetChild(0).gameObject;
                    if (item != null)
                    {
                        item.transform.SetParent(mouseFollow.transform, false);
                        isItemOn = false;
                    }
                }
            }
        }
    }

    private void OnMouseDown()
    {
        if (isItemOn && !isSlotUsed)
        {
            GameObject item = transform.GetChild(0).gameObject;
            item.transform.position = new Vector3(item.transform.position.x, item.transform.position.y, -0.5f);
            isSlotUsed = true;
            shop.IsOnChangeMode = false;
            button = shop.LastItem;
            shop.LastItem.transform.SetParent(controller.transform.GetChild(slotIndex), false);
            shop.LastItem.SetActive(true);
            itemValue = shop.LastItemValue;
            shop.LastItemValue = 0.0f;
            shop.LastItemNull();
            shop.IsItemOnMouse = false;
            //transform.position = new Vector3(transform.position.x, transform.position.y, 0.5f);
            if (shop.Item == 1)
            {
                EggGameManager.Inst.SlotItems[slotIndex] = EggGameManager.item.Propeller;
            }
            if (shop.Item == 2)
            {
                EggGameManager.Inst.SlotItems[slotIndex] = EggGameManager.item.Rocket;
            }
            shop.Item = -1;
        }
    }

    public void DestroyItem()
    {
        shop.Purchase(-itemValue);
        shop.MoneyRefund();
        for (int i = 0; i < transform.childCount; i++)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
        Destroy(button.gameObject);
        EggGameManager.Inst.SlotItems[slotIndex] = EggGameManager.item.None;
        itemValue = 0.0f;
        isSlotUsed = false;
    }

    void EquipRocket()
    {
        Rocket rocket = Instantiate(EggGameManager.Inst.ItemData[ItemIDCode.Rocket].modelPrefab, transform).GetComponent<Rocket>();
        UI_Rocket rocketButton = Instantiate(EggGameManager.Inst.ItemData[ItemIDCode.Rocket].buttonPrefab, controller.transform.GetChild(slotIndex), false).GetComponent<UI_Rocket>();
        rocketButton.rocket = rocket;
        button = rocketButton.gameObject;
        itemValue = EggGameManager.Inst.ItemData[ItemIDCode.Rocket].value;
        rocket.transform.position = new Vector3(rocket.transform.position.x, rocket.transform.position.y, -0.5f);
        isSlotUsed = true;
    }
    void EquipPropeller()
    { 
        Propeller propeller = Instantiate(EggGameManager.Inst.ItemData[ItemIDCode.Propeller].modelPrefab, transform).GetComponent<Propeller>();
        UI_Propellar propellerButton = Instantiate(EggGameManager.Inst.ItemData[ItemIDCode.Propeller].buttonPrefab, controller.transform.GetChild(slotIndex), false).GetComponent<UI_Propellar>();
        propellerButton.propeller = propeller;
        button = propellerButton.gameObject;
        itemValue = EggGameManager.Inst.ItemData[ItemIDCode.Propeller].value;
        propeller.transform.position = new Vector3(propeller.transform.position.x, propeller.transform.position.y, -0.5f);
        isSlotUsed = true;
    }

}
