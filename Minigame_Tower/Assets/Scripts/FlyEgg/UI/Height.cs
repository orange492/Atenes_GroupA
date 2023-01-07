using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Height : MonoBehaviour
{
    Egg egg;
    Slider slider;

    private void Awake()
    {
        slider = GetComponent<Slider>();
    }
    private void Start()
    {
        egg = FindObjectOfType<Egg>();
        transform.parent.gameObject.SetActive(false); 
        if (EggGameManager.Inst != null)
        {
            EggGameManager.Inst.onModeChange += ModeChange;
        }
    }
  

    private void OnDestroy()
    {
        if (EggGameManager.Inst != null)
        {
            EggGameManager.Inst.onModeChange -= ModeChange;
            Debug.Log("헤이트 디스트");
        }
    }
    private void ModeChange(EggGameManager.Mode obj)
    {
        transform.parent.gameObject.SetActive(obj == EggGameManager.Mode.Play);
    }

    private void Update()
    {
        if (!egg.IsDead)
        {
            slider.value = (egg.transform.position.y +4.45f)*0.5f;
        }
    }
}
