using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Rule : MonoBehaviour
{
    Button ruleButton;
    GameObject ruleBook;
    bool ruleOnOff = false;
   

    private void Awake()
    {
        ruleButton = transform.GetChild(0).GetComponent<Button>();
        ruleBook = transform.GetChild(1).gameObject;
        ruleButton.onClick.AddListener(RuleOnOff);
       
    }

    void RuleOnOff()
    {
        ruleOnOff = !ruleOnOff;
        ruleBook.SetActive(ruleOnOff);
    }


}
