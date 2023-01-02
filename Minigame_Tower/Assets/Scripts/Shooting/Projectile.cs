using UnityEngine;

public class Projectile : MonoBehaviour
{
	[SerializeField]
	private	int	damage = 1;

	private void OnTriggerEnter2D(Collider2D collision)
	{
		// 발사체에 부딪힌 오브젝트의 태그가 "Enemy" 이면
		if ( collision.CompareTag("Enemy") )
		{
			// 부딪힌 오브젝트 체력 감소 (적)
			collision.GetComponent<EnemyHP>().TakeDamage(damage);
			// 내 오브젝트 삭제 (발사체)
			Destroy(gameObject);
		}
		// 발사체에 부딪힌 오브젝트의 태그가 "Boss" 이면
		else if ( collision.CompareTag("Boss") )
		{
			// 부딪힌 오브젝트 체력 감소 (보스)
			collision.GetComponent<BossHP>().TakeDamage(damage);
			// 내 오브젝트 삭제 (발사체)
			Destroy(gameObject);
		}
	}
}


/*
 * File : Projectile.cs
 * Desc
 *	: 플레이어 캐릭터의 공격 발사체
 *	
 * Functions
 *	: OnTriggerEnter2D() - 적과 충돌했을 때 처리
 *	
 */