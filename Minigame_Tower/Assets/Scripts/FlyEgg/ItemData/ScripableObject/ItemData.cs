using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item Data", menuName = "Scriptable Object/Item Data", order = 1)]
public class ItemData :ScriptableObject
{
    [Header("아이템 기본 데이터")]
    public int id = 0;                 // 아이템 ID
    public string itemName = "아이템";   // 아이템의 이름
    public GameObject modelPrefab;      // 아이템의 외형을 표시할 프리팹(드랍때 보일 모델)
    public Sprite itemIcon;             // 아이템이 인벤토리에서 보일 스프라이트
    public int value;                  // 아이템의 가격
    public bool isNeedPosition;     // 인벤토리 한칸에 들어갈 수 있는 최대 누적 갯수
    public GameObject buttonPrefab;
    public string itemDescription;      // 아이템의 상세 설명
}
