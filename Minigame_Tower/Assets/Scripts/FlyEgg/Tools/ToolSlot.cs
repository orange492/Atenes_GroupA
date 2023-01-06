using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
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
        Debug.Log("툴슬롯 온마우스");
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
        itemValue = 0.0f;
        isSlotUsed = false;
    }

}
