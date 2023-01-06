using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Speed : MonoBehaviour
{
    TextMeshProUGUI text;
    Egg egg;
    private void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        egg = FindObjectOfType<Egg>();
        EggGameManager.Inst.onModeChange += ModeChange;
        gameObject.SetActive(false);
    }
    private void ModeChange(EggGameManager.Mode obj)
    {
        gameObject.SetActive(obj == EggGameManager.Mode.Play);
    }
    private void Update()
    {
        if (EggGameManager.Inst.mode == EggGameManager.Mode.Play)
        {
            text.text = $"{egg.Rigid.velocity.magnitude:F2}cm/s";
        }
    }
}
