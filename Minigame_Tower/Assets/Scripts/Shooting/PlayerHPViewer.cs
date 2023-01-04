using UnityEngine;
using UnityEngine.UI;

public class PlayerHPViewer : MonoBehaviour
{
    [SerializeField]
    private PlayerHP playerHP;
    private Slider   sliderHP;

    private void Awake()
    {
        sliderHP = GetComponent<Slider>();
    }

    /// <summary>
    /// Tip. 더 정확한 방법으로는 이벤트를 이용해 체력 정보가 바뀔때만 UI 정보 갱신
    /// </summary>
    private void Update()
    {
        // Slider UI에 현재 체력 정보를 업데이트
        sliderHP.value = playerHP.CurrentHP / playerHP.MaxHP;
    }
}


/*
 * File : PlayerHPView.cs
 * Desc
 *	: 플레이어의 체력 정보를 Slider UI에 업데이트
 *	
 */