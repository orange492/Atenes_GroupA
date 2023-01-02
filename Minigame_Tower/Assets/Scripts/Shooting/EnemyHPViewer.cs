using UnityEngine;
using UnityEngine.UI;

public class EnemyHPViewer : MonoBehaviour
{
    private EnemyHP enemyHP;
    private Slider  hpSlider;

    public void Setup(EnemyHP enemyHP)
    {
        this.enemyHP = enemyHP;
        hpSlider     = GetComponent<Slider>();
    }

    private void Update()
    {
        hpSlider.value = enemyHP.CurrentHP / enemyHP.MaxHP;
    }
}


/*
 * File : EnemyHPView.cs
 * Desc
 *	: 적의 체력 정보를 Slider UI에 업데이트
 *	
 */