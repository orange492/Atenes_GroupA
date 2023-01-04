using UnityEngine;
using UnityEngine.UI;

public class BossHPViewer : MonoBehaviour
{
    [SerializeField]
    private BossHP  bossHP;
    private Slider  sliderHP;

    private void Awake()
    {
        sliderHP = GetComponent<Slider>();
    }

    private void Update()
    {
        // Slider UI에 체력 정보를 업데이트
        sliderHP.value = bossHP.CurrentHP / bossHP.MaxHP;
    }
}


/*
 * File : BossHPViewer.cs
 * Desc
 *	: 보스의 체력 정보를 Slider UI에 업데이트
 *	
 */