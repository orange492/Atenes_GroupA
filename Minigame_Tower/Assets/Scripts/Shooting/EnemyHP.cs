using System.Collections;
using UnityEngine;

public class EnemyHP : MonoBehaviour
{
    [SerializeField]
    private float           maxHP = 4;      // 최대 체력
    private float           currentHP;      // 현재 체력
    private ShootingEnemy           enemy;
    private SpriteRenderer  spriteRenderer;

    public  float MaxHP => maxHP;
    public  float CurrentHP => currentHP;

    private void Awake()
    {
        currentHP       = maxHP;            // 현재 체력을 최대 체력과 같게 설정
        enemy           = GetComponent<ShootingEnemy>();
        spriteRenderer  = GetComponent<SpriteRenderer>();
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
            // 적 캐릭터 사망
            enemy.OnDie();
        }
    }

    private IEnumerator HitColorAnimation()
    {
        // 적의 색상을 빨간색으로
        spriteRenderer.color = Color.red;
        // 0.05초 동안 대기
        yield return new WaitForSeconds(0.05f);
        // 적의 색상을 원래 색상인 하얀색으로
        spriteRenderer.color = Color.white;
    }
}


/*
 * File : EnemyHP.cs
 * Desc
 *	: 적 캐릭터의 체력
 *	
 * Functions
 *	: TakeDamage() - 체력 감소
 *	: HitColorAnimation() - 색상을 빨간색으로 잠시 바꿈
 */