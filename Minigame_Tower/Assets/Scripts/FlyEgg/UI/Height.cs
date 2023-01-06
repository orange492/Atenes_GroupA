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
    }

    private void Update()
    {
        if (!egg.IsDead)
        {
            slider.value = (egg.transform.position.y +4.45f)*0.5f;
        }
    }
}
