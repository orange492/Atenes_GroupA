using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
	[SerializeField]
	private	int			damage = 1;
	[SerializeField]
	private	GameObject	explosionPrefab;		// 폭발 효과

	private void OnTriggerEnter2D(Collider2D collision)
	{
		// 발사체에 부딪힌 오브젝트의 태그가 "Player"이면
		if ( collision.CompareTag("Player") )
		{
			// 부딪힌 오브젝트 체력 감소 (플레이어)
			collision.GetComponent<PlayerHP>().TakeDamage(damage);
			// 내 오브젝트 삭제 (발사체)
			Destroy(gameObject);
		}
	}

	/// <summary>
	/// 폭탄에 의해 발사체가 삭제될 때 호출되는 함수로 발사체 폭발 효과를 보여준다
	/// </summary>
	public void OnDie()
	{
		// 폭발 효과 생성
		Instantiate(explosionPrefab, transform.position, Quaternion.identity);
		// 적/보스 발사체 삭제
		Destroy(gameObject);
	}
}


/*
 * File : EnemyProjectile.cs
 * Desc
 *	: 보스, 적의 공격 발사체
 *	
 * Functions
 *	: OnTriggerEnter2D() - 플레이어와 충돌했을 때 처리
 *	: OnDie() - 폭탄에 의해 발사체가 삭제될 때 호출되는 함수
 *	
 */