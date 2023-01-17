using DG.Tweening;
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
    int slingShotCount = 0;
    int radarCount = 0;
    int parachuteCount = 0;

    GameObject canvas;

    Tools tools;
    ToolSlot[] toolSlots;

    Color defaultColor;
    Color cannotPurchaseColor;

    MouseFollow mouseFollow;

    GameObject lastItem;

    int item = -1;
    public int Item
    {
        get => item;
        set => item = value;
    }
    public GameObject LastItem
    {
     get=> lastItem;
        set => lastItem = value;
    }

    float moneyRemain;
    public float MoneyRemain
    {
        get => moneyRemain;
        set
        {
            moneyRemain = value;
            moneyRemainText.text = $"${moneyRemain:F2}";
        }
    }

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

        for (int i = 0; i < 8; i++)
        {
            purchaseButtons[i] = shopInside.transform.GetChild(0).GetChild(i).GetComponent<Button>();
        }

        defaultColor = purchaseButtons[0].transform.GetComponent<Image>().color;
        cannotPurchaseColor = new Color(255, 0, 0, 208);

        purchaseButtons[0].onClick.AddListener(PurchaseSlingShot);
        purchaseButtons[1].onClick.AddListener(PurchaseParachute);
        purchaseButtons[2].onClick.AddListener(PurchasePropeller);
        purchaseButtons[3].onClick.AddListener(PurchaseRocket);
        purchaseButtons[4].onClick.AddListener(PurchaseDetection);
        purchaseButtons[6].onClick.AddListener(OnDrawMode);

        moneyLackSequence = DOTween.Sequence().SetAutoKill(false).Pause();
        moneyLackSequence.Append(moneyRemainText.DOColor(Color.red, 0.1f));
        moneyLackSequence.Append(moneyRemainText.DOColor(Color.white, 0.1f));
        moneyLackSequence.OnComplete(() => { moneyLackSequence.Rewind(); });

        moneyRefundSequence = DOTween.Sequence().SetAutoKill(false).Pause();
        moneyRefundSequence.Append(moneyRemainText.DOColor(Color.blue, 0.1f));
        moneyRefundSequence.Append(moneyRemainText.DOColor(Color.white, 0.1f));
        moneyRefundSequence.OnComplete(() => { moneyRefundSequence.Rewind(); });
    }
    private void Start()
    {
        if (EggGameManager.Inst != null)
        {
            EggGameManager.Inst.onModeChange += ModeChange;
        }

        MoneyRemain = EggGameManager.Inst.Money;
        mouseFollow = FindObjectOfType<MouseFollow>();

        tools = FindObjectOfType<Tools>();
        toolSlots = new ToolSlot[8];
        for (int i = 0; i < tools.transform.childCount; i++)
        {
            toolSlots[i] = tools.transform.GetChild(i).GetComponent<ToolSlot>();
            toolSlots[i].SlotIndex = i;
        }
        canvas = GameObject.FindGameObjectWithTag("EggGameCanvas");


        items = new ItemData[5];
        for (int i = 0; i < 5; i++)
        {
            items[i] = EggGameManager.Inst.ItemData[(uint)i];
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

        drawButton = FindObjectOfType<DrawButton>();
        drawButton.gameObject.SetActive(false);

        if (EggGameManager.Inst.isDetection)
        {
            MoneyRemain += EggGameManager.Inst.ItemData[ItemIDCode.Radar].value;
            PurchaseDetection();
        }
        if (EggGameManager.Inst.isSlingShot)
        {
            MoneyRemain += EggGameManager.Inst.ItemData[ItemIDCode.SlingShot].value;
            PurchaseSlingShot();
        }

        if (EggGameManager.Inst.isParachute)
        {
            MoneyRemain += EggGameManager.Inst.ItemData[ItemIDCode.Parachute].value;
            PurchaseParachute();
        }

        if (EggGameManager.Inst.mode == EggGameManager.Mode.Intro)
        {
            shopOpenButton.gameObject.SetActive(false); 
            transform.GetChild(3).gameObject.SetActive(false);
        }
        if (EggGameManager.Inst.mode == EggGameManager.Mode.ReadyToPlay)
        {
            shopOpenButton.gameObject.SetActive(true);
        }
       
       
    }

    private void ModeChange(EggGameManager.Mode obj)
    {
        if (obj == EggGameManager.Mode.Play||obj==EggGameManager.Mode.Clear|| obj == EggGameManager.Mode.Intro)
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

    public void PurchaseSlingShot()
    {
        if (!isOnChangeMode)
        {
            if (slingShotCount > 0)
            {
                SellSlingShot();
                return;
            }
            if (moneyRemain < EggGameManager.Inst.ItemData[(uint)0].value)
            {
                MoneyLack();
                return;
            }


            GameObject slingshot = Instantiate(EggGameManager.Inst.ItemData[(uint)0].modelPrefab);
            slingshot.transform.position = new Vector3(0, -1.0f, 0);
            Purchase(EggGameManager.Inst.ItemData[(uint)0].value);
            slingShotCount++;
            EggGameManager.Inst.isSlingShot = true;
            purchaseButtons[0].transform.GetComponent<Image>().color = cannotPurchaseColor;
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

            EggGameManager.Inst.Money = moneyRemain;
        }
    }
    public void SellSlingShot()
    {
        Destroy( FindObjectOfType<SlingShot>().gameObject);
        slingShotCount--;
        Purchase(-items[0].value);
        EggGameManager.Inst.isSlingShot = false;
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

            if (tools.IsAllSlotUsed)
            {
                warningTextOn("모든 슬롯이 사용중입니다!");
                return;
            }
            Item = 1;
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
            if (tools.IsAllSlotUsed)
            {
                warningTextOn("모든 슬롯이 사용중입니다!");
                return;
            }
            Item = 2;
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

   public void PurchaseDetection()
    {
        if (radarCount > 0)
        {
            radarCount--;
            toolSlots[0].DestroyItem();
            Purchase(-EggGameManager.Inst.ItemData[ItemIDCode.Radar].value);
            tools.SlotUse(0, false);
            purchaseButtons[4].transform.GetComponent<Image>().color = defaultColor;
            EggGameManager.Inst.isDetection = false;
            return;
        }
        if (moneyRemain < EggGameManager.Inst.ItemData[ItemIDCode.Radar].value)
        {
            MoneyLack();
            return;
        }

        if (toolSlots[0].IsSlotUsed)
        {
            warningTextOn("레이더 슬롯이 사용중입니다!");
            return;
        }
        GameObject radar = Instantiate(EggGameManager.Inst.ItemData[ItemIDCode.Radar].modelPrefab, tools.transform.GetChild(0));
        GameObject radarbutton = Instantiate(EggGameManager.Inst.ItemData[ItemIDCode.Radar].buttonPrefab,canvas.transform.GetChild(0).GetChild(0).transform);
        radarbutton.GetComponent<DetectionButton>().detection = radar.GetComponent<Detection>();
        tools.SlotUse(0); 
        toolSlots[0].button = radarbutton;
        radarCount++;
        purchaseButtons[4].transform.GetComponent<Image>().color = cannotPurchaseColor;
        Purchase(EggGameManager.Inst.ItemData[ItemIDCode.Radar].value);
        EggGameManager.Inst.isDetection = true;
    }

    public void PurchaseParachute()
    {
        if (parachuteCount > 0)
        {
            parachuteCount--;
            toolSlots[2].DestroyItem();
            Purchase(-items[(int)ItemIDCode.Parachute].value);
            tools.SlotUse(2,false);
            purchaseButtons[1].transform.GetComponent<Image>().color = defaultColor;
            EggGameManager.Inst.isParachute = false;
            return;
        }
        if (moneyRemain < items[1].value)
        {
            MoneyLack();
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
        EggGameManager.Inst.isParachute = true;
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
        MoneyRemain -= price;
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
