using System.Collections;
using UnityEngine;

public class BossHP : MonoBehaviour
{
    [SerializeField]
    private float            maxHP = 1000;      // 최대 체력
    private float            currentHP;         // 현재 체력
    private SpriteRenderer   spriteRenderer;
    private BigBoss             boss;

    public  float MaxHP => maxHP;
    public  float CurrentHP => currentHP;

    private void Awake()
    {
        currentHP       = maxHP;               // 현재 체력을 최대 체력과 같게 설정
        spriteRenderer  = GetComponent<SpriteRenderer>();
        boss            = GetComponent<BigBoss>();
    }

    public void TakeDamage(float damage)
    {
        // 현재 체력을 damage만큼 감소
        currentHP -= damage;

        StopCoroutine("HitColorAnimation");
        StartCoroutine("HitColorAnimation");

        // 체력이 0이하 = 플레이어 캐릭터 사망
        if ( currentHP <= 0 )
        {
            // 체력이 0이면 OnDie() 함수를 호출해서 죽었을 때 처리를 한다
            boss.OnDie();
        }
    }

    private IEnumerator HitColorAnimation()
    {
        // 플레이어의 색상을 빨간색으로
        spriteRenderer.color = Color.red;
        // 0.05초 동안 대기
        yield return new WaitForSeconds(0.05f);
        // 플레이어의 색상을 원래 색상인 하얀색으로
        // (원래 색상이 하얀색이 아닐 경우 원래 색상 변수 선언)
        spriteRenderer.color = Color.white;
    }
}


/*
 * File : BossHP.cs
 * Desc
 *	: 플레이어 캐릭터의 체력
 *	
 * Functions
 *	: TakeDamage() - 체력 감소
 *	
 */