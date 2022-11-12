using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using TMPro;
using UnityEngine;
using UnityEngine.XR;

public class Combo : MonoBehaviour
{
    int combo=0;
    TextMeshProUGUI comboNumberText;
    GameObject comboObject;
    float timeRemain;
    BlockController blockController;
 GameObject comboText;

    public int ComboProperty
    {
        get => combo;
        set
        {
            if (combo != value)
            {
                combo = value;
                blockController.ComboAddtionalScore(combo);
                if (combo == 0)
                {
                    comboText.SetActive(false);
                }
                else
                {
                    comboText.SetActive(true);
                }
            }

        }
    }

    private void Awake()
    {
        comboObject = transform.GetChild(0).gameObject;
        comboNumberText = comboObject.GetComponent<TextMeshProUGUI>();
        comboText = transform.GetChild(1).gameObject;
    }
    private void Start()
    {
        blockController = FindObjectOfType<BlockController>();
        blockController.onComboChange += ComboRefresh;
        comboNumberText.text = combo.ToString();
        comboObject.SetActive(false);
    }

    private void Update()
    {
        timeRemain += Time.deltaTime;
        if (timeRemain > 3.0f)
        {
            ComboProperty = 0;
            comboNumberText.text = combo.ToString();
            comboObject.SetActive(false);
        }
    }

    public void ComboRefresh()
    {
        timeRemain = 0.0f;
        ComboProperty++;
        comboNumberText.text = combo.ToString();
        comboObject.SetActive(true);
    }

}
