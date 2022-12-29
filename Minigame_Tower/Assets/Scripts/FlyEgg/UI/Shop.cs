using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
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

    DG.Tweening.Sequence moneyLackSequence;
    DG.Tweening.Sequence moneyRefundSequence;

    int slingShotCount = 0;

    Color defaultColor;
    Color cannotPurchaseColor;



    float moneyRemain = 40.0f;


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

    }
    private void Start()
    {
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
    private void PurchaseSlingShot()
    {
        if (moneyRemain < items[0].value)
        {
            MoneyLack();
            return;
        }

        if (slingShotCount > 0)
        {
            return;
        }

        GameObject slingshot= Instantiate(items[0].modelPrefab);
        slingshot.transform.position = new Vector3(0, -1.0f, 0);
        Purchase(items[0].value);
        slingShotCount++;
        purchaseButtons[0].transform.GetComponent<Image>().color = cannotPurchaseColor;
    }

    public void SellSlingShot()
    {
        slingShotCount--;
        Purchase(-items[0].value);
        purchaseButtons[0].transform.GetComponent<Image>().color = defaultColor;
    }


    private void OnDrawMode()
    {
        ShopClose();
        shopOpenButton.gameObject.SetActive(false);
        drawButton.gameObject.SetActive(true);
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
