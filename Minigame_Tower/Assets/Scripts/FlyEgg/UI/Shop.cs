using DG.Tweening;
using Sirenix.Serialization;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEditor.Purchasing;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    GameObject shopInside;
    Button shopOpenButton;
    Button shopCloseButton;

    Button[] purchaseButtons;

    ItemData[] items;
    Image[] images;
    TextMeshProUGUI[] names;
    TextMeshProUGUI[] prices;
    TextMeshProUGUI moneyRemainText;



    public GameObject parachuteBackPack;

    TextMeshProUGUI warningText;

    float lastItemValue;
    public float LastItemValue
    {
        get => lastItemValue;
        set => lastItemValue = value;
    }

    DG.Tweening.Sequence moneyLackSequence;
    DG.Tweening.Sequence moneyRefundSequence;
    DG.Tweening.Sequence warningTextSequence;
    ToolSlot[] toolSlots;
    int slingShotCount = 0;
    int radarCount = 0;
    int parachuteCount = 0;

    Canvas canvas;

    Tools tools;

    Color defaultColor;
    Color cannotPurchaseColor;

    MouseFollow mouseFollow;

    GameObject lastItem;

    public GameObject LastItem
    {
     get=> lastItem;
        set => lastItem = value;
    }

    float moneyRemain = 10000.0f;

    bool isOnDrawMode = false;

    public bool IsOnDrawMode
    {
        get => isOnDrawMode;
        set
        {
            isOnDrawMode = value;
            if (isOnDrawMode)
            {
                EggGameManager.Inst.mode = EggGameManager.Mode.Editting;
            }
            else
            {
                EggGameManager.Inst.mode = EggGameManager.Mode.ReadyToPlay;
            }
        }
    }


    bool isOnChangeMode = false;

    bool isItemOnMouse = false;
    public bool IsItemOnMouse
    {
        get => isItemOnMouse;
        set { 
            isItemOnMouse = value;
            if (isItemOnMouse)
            {
                EggGameManager.Inst.mode = EggGameManager.Mode.Editting;
            }
            else
            {
                EggGameManager.Inst.mode = EggGameManager.Mode.ReadyToPlay;
            }
        }
       
    }
    public bool IsOnChangeMode
    {
        get => isOnChangeMode;
        set => isOnChangeMode = value;
    }


    public float MoneyRemain => moneyRemain;

    DrawButton drawButton;
    private void Awake()
    {
        shopInside = transform.GetChild(0).gameObject;
        shopOpenButton = transform.GetChild(1).GetComponent<Button>();
        shopCloseButton = transform.GetChild(2).GetComponent<Button>();
        shopOpenButton.onClick.AddListener(ShopOpen);
        shopCloseButton.onClick.AddListener(ShopClose);
        ShopClose();

        purchaseButtons = new Button[8];
        moneyRemainText = transform.GetChild(3).GetComponent<TextMeshProUGUI>();
        moneyRemainText.text = $"${moneyRemain:F2}";
        warningText = transform.GetChild(4).GetComponent<TextMeshProUGUI>();
        warningText.color = Color.clear;

    }
    private void Start()
    {
        canvas = FindObjectOfType<Canvas>();
        mouseFollow = FindObjectOfType<MouseFollow>();
        tools = FindObjectOfType<Tools>();
        EggGameManager.Inst.onModeChange += ModeChange;
        toolSlots = new ToolSlot[8];
        for (int i = 0; i < transform.childCount; i++)
        {
            toolSlots[i] = tools.transform.GetChild(i).GetComponent<ToolSlot>();
            toolSlots[i].SlotIndex = i;

        }
        for (int i = 0; i < 8; i++)
        {
            purchaseButtons[i] = shopInside.transform.GetChild(0).GetChild(i).GetComponent<Button>();
        }

        items = new ItemData[5];
        for (int i = 0; i < 5; i++)
        {
            items[i]=EggGameManager.Inst.ItemData[(uint)i];
        }

        images = new Image[8];
        names = new TextMeshProUGUI[8];
        prices = new TextMeshProUGUI[8];


        for (int i = 0; i < 8; i++)
        {
            images[i] = purchaseButtons[i].transform.GetChild(1).GetComponent<Image>();
            names[i] = purchaseButtons[i].transform.GetChild(2).GetComponent<TextMeshProUGUI>();
            prices[i] = purchaseButtons[i].transform.GetChild(3).GetComponent<TextMeshProUGUI>();
        }

        for (int i = 0; i < 5; i++)
        {
            images[i].sprite = items[i].itemIcon;
            names[i].text = items[i].itemName;
            prices[i].text = $"${items[i].value}";

        }

        purchaseButtons[6].onClick.AddListener(OnDrawMode);
        drawButton = FindObjectOfType<DrawButton>();
        drawButton.gameObject.SetActive(false);

        purchaseButtons[0].onClick.AddListener(PurchaseSlingShot);
        purchaseButtons[1].onClick.AddListener(purchaseParachute);
        purchaseButtons[2].onClick.AddListener(PurchasePropeller);
        purchaseButtons[3].onClick.AddListener(PurchaseRocket);
        purchaseButtons[4].onClick.AddListener(PurchaseDetection);

        moneyLackSequence = DOTween.Sequence().SetAutoKill(false).Pause();
        moneyLackSequence.Append(moneyRemainText.DOColor(Color.red, 0.1f));
        moneyLackSequence.Append(moneyRemainText.DOColor(Color.white, 0.1f));
        moneyLackSequence.OnComplete(() => { moneyLackSequence.Rewind(); });

        moneyRefundSequence = DOTween.Sequence().SetAutoKill(false).Pause();
        moneyRefundSequence.Append(moneyRemainText.DOColor(Color.blue, 0.1f));
        moneyRefundSequence.Append(moneyRemainText.DOColor(Color.white, 0.1f));
        moneyRefundSequence.OnComplete(() => { moneyRefundSequence.Rewind(); });


        defaultColor = purchaseButtons[0].transform.GetComponent<Image>().color;
        cannotPurchaseColor = new Color(255, 0, 0, 208);

    }

    private void ModeChange(EggGameManager.Mode obj)
    {
        if (obj == EggGameManager.Mode.Play)
        {
            ShopClose();
            shopOpenButton.gameObject.SetActive(false);
            transform.GetChild(3).gameObject.SetActive(false);
        }
        if (obj == EggGameManager.Mode.ReadyToPlay)
        {
            shopOpenButton.gameObject.SetActive(true);
            transform.GetChild(3).gameObject.SetActive(true);
        }
    }

    private void PurchaseSlingShot()
    {
        if (!isOnChangeMode)
        {
            if (moneyRemain < items[0].value)
            {
                MoneyLack();
                return;
            }

            if (slingShotCount > 0)
            {
                SellSlingShot();
                return;
            }

            GameObject slingshot = Instantiate(items[0].modelPrefab);
            slingshot.transform.position = new Vector3(0, -1.0f, 0);
            Purchase(items[0].value);
            slingShotCount++;
            purchaseButtons[0].transform.GetComponent<Image>().color = cannotPurchaseColor;
        }
    }

    public void SellSlingShot()
    {
        Destroy( FindObjectOfType<SlingShot>().gameObject);
        slingShotCount--;
        Purchase(-items[0].value);
        purchaseButtons[0].transform.GetComponent<Image>().color = defaultColor;
    }

    void PurchasePropeller()
    {
        if (!IsOnChangeMode)
        {
            if (moneyRemain < items[2].value)
            {
                MoneyLack();
                return;
            }
            IsOnChangeMode = true;
            GameObject propeller = Instantiate(items[(int)ItemIDCode.Propeller].modelPrefab, mouseFollow.transform);
            
            GameObject propellerButton = Instantiate(items[(int)ItemIDCode.Propeller].buttonPrefab, canvas.transform.GetChild(0).transform);
            LastItem = propellerButton;
            UI_Propellar uI_Propellar = propellerButton.GetComponent<UI_Propellar>();
            uI_Propellar.propeller = propeller.GetComponent<Propeller>();
            propellerButton.SetActive(false);
            lastItemValue = items[(int)ItemIDCode.Propeller].value;
            Purchase(items[2].value);
            ShopClose();
            IsItemOnMouse = true;
        }
    }

    void PurchaseRocket()
    {
        if (!IsOnChangeMode)
        {
            if (moneyRemain < items[3].value)
            {
                MoneyLack();
                return;
            }
            IsOnChangeMode = true;
            GameObject rocket = Instantiate(items[(int)ItemIDCode.Rocket].modelPrefab, mouseFollow.transform);
            GameObject rocketButton = Instantiate(items[(int)ItemIDCode.Rocket].buttonPrefab, canvas.transform.GetChild(0).transform);
            LastItem = rocketButton;
            UI_Rocket uI_rocket = rocketButton.GetComponent<UI_Rocket>();
            uI_rocket.rocket = rocket.GetComponent<Rocket>();
            rocketButton.SetActive(false); 
            lastItemValue = items[(int)ItemIDCode.Rocket].value;
            Purchase(items[3].value);
            ShopClose();
            IsItemOnMouse = true;
        }
    }

    void PurchaseDetection()
    {
        if (moneyRemain < items[4].value)
        {
            MoneyLack();
            return;
        }
        if (radarCount > 0)
        {
            radarCount--;
            toolSlots[0].DestroyItem();
            Purchase(-items[(int)ItemIDCode.Radar].value);
            tools.SlotUse(0, false);
            purchaseButtons[4].transform.GetComponent<Image>().color = defaultColor;
            return;
        }

        if (toolSlots[0].IsSlotUsed)
        {
            warningTextOn("레이더 슬롯이 사용중입니다!");
            return;
        }
        GameObject radar = Instantiate(items[(int)ItemIDCode.Radar].modelPrefab, tools.transform.GetChild(0));
        GameObject radarbutton = Instantiate(items[(int)ItemIDCode.Radar].buttonPrefab,canvas.transform.GetChild(0).GetChild(0).transform);
        radarbutton.GetComponent<DetectionButton>().detection = radar.GetComponent<Detection>();
        tools.SlotUse(0); 
        toolSlots[0].button = radarbutton;
        radarCount++;
        purchaseButtons[4].transform.GetComponent<Image>().color = cannotPurchaseColor;
        Purchase(items[(int)ItemIDCode.Radar].value);
    }

    void purchaseParachute()
    {
        if (moneyRemain < items[1].value)
        {
            MoneyLack();
            return;
        }
        if (parachuteCount > 0)
        {
            parachuteCount--;
            toolSlots[2].DestroyItem();
            Purchase(-items[(int)ItemIDCode.Parachute].value);
            tools.SlotUse(2,false);
            purchaseButtons[1].transform.GetComponent<Image>().color = defaultColor;
            return;
        }
        if (toolSlots[2].IsSlotUsed)
        {
            warningTextOn("낙하산 슬롯이 사용중입니다!");
            return;
        }

        GameObject parachute = Instantiate(items[(int)ItemIDCode.Parachute].modelPrefab, tools.transform.GetChild(2));
        Instantiate(parachuteBackPack, tools.transform.GetChild(2));
        GameObject parachuteButton = Instantiate(items[(int)ItemIDCode.Parachute].buttonPrefab,canvas.transform.GetChild(0).GetChild(2).transform);
        //parachuteButton.GetComponent<UI_Parachute>().pa = parachute.GetComponent<Parachute>();
        tools.SlotUse(2);
        toolSlots[2].button = parachuteButton;
        parachuteCount++;
        purchaseButtons[1].transform.GetComponent<Image>().color = cannotPurchaseColor;
        Purchase(items[(int)ItemIDCode.Parachute].value);
    }

  

    void warningTextOn(string text)
    {
        StopAllCoroutines();
        warningText.text = text;
        warningText.color = Color.red;
        StartCoroutine(warningTextOff());
    }

    IEnumerator warningTextOff()
    {
        yield return new WaitForSeconds(1.0f);
        warningText.color = Color.clear;
    }

    public void LastItemNull()
    {
        lastItem = null;
    }


    private void OnDrawMode()
    {
        ShopClose();
        shopOpenButton.gameObject.SetActive(false);
        drawButton.gameObject.SetActive(true);
        IsOnDrawMode = true ;
    }

    private void ShopClose()
    {
        shopInside.SetActive(false);
        shopCloseButton.gameObject.SetActive(false);
        shopOpenButton.gameObject.SetActive(true);
    }

    private void ShopOpen()
    {
        shopInside.SetActive(true);
        shopOpenButton.gameObject.SetActive(false);
        shopCloseButton.gameObject.SetActive(true);
    }

    public void ShopOpenButtonActivate()
    {
        shopOpenButton.gameObject.SetActive(true);
    }

    public void Purchase(float price)
    {
        moneyRemain -= price;
        moneyRemainText.text = $"${moneyRemain:F2}";
    }

   public void MoneyLack()
    {
        if (!moneyLackSequence.IsPlaying())
        {
            moneyLackSequence.Play();
        }
    }
    
    public void MoneyRefund()
    {
        if (!moneyRefundSequence.IsPlaying())
        {
            moneyRefundSequence.Play();
        }
    }
    


}
