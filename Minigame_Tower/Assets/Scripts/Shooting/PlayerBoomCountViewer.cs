using UnityEngine;
using TMPro;

public class PlayerBoomCountViewer : MonoBehaviour
{
    [SerializeField]
    private Weapon          weapon;
    private TextMeshProUGUI textBoomCount;

    private void Awake()
    {
        textBoomCount = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        textBoomCount.text = "x "+weapon.BoomCount;
    }
}


/*
 * File : PlayerBoomCountViewer.cs
 * Desc
 *	: 플레이어의 폭탄 개수 정보를 Text UI에 업데이트
 *	
 */