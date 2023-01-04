using System.Collections;
using UnityEngine;

public enum AttackType { CircleFire = 1, SingleFireToCenterPosition }

public class BossWeapon : MonoBehaviour
{
	[SerializeField]
	private	GameObject projectilePrefab;   // 공격할 때 생성되는 발사체 프리팹

	public void StartFiring(AttackType attackType)
	{
		// attackType 열거형의 이름과 같은 코루틴을 실행
		StartCoroutine(attackType.ToString());
	}

	public void StopFiring(AttackType attackType)
	{
		// attackType 열거형의 이름과 같은 코루틴을 중지
		StopCoroutine(attackType.ToString());
	}

	private IEnumerator CircleFire()
	{
		float attackRate	= 0.5f;			// 공격 주기
		int	  count			= 30;			// 발사체 생성 개수
		float intervalAngle	= 360 / count;	// 발사체 사이의 각도
		float weightAngle	= 0;			// 가중되는 각도 (항상 같은 위치로 발사하지 않도록 설정)

		// 원 형태로 방사하는 발사체 생성 (count 개수만큼)
		while ( true )
		{
			for ( int i = 0; i < count; ++ i )
			{
				// 발사체 생성
				GameObject clone = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
				// 발사체 이동 방향 (각도)
				float angle = weightAngle + intervalAngle * i;
				// 발사체 이동 방향 (벡터)
				float x = Mathf.Cos(angle * Mathf.PI / 180.0f);	// Cos(각도), 라디안 단위의 각도 표현을 위해 PI / 180을 곱함
				float y = Mathf.Sin(angle * Mathf.PI / 180.0f);	// Sin(각도), 라디안 단위의 각도 표현을 위해 PI / 180을 곱함
				// 발사체 이동 방향 설정
				clone.GetComponent<Movement2D>().MoveTo(new Vector2(x, y));
			}

			// 발사체가 생성되는 시작 각도 설정을 위한 변수
			weightAngle += 1;

			// attackRate 시간만큼 대기
			yield return new WaitForSeconds(attackRate);
		}
	}

	private IEnumerator SingleFireToCenterPosition()
	{
		Vector3	targetPosition	= Vector3.zero;	// 목표 위치 (중앙)
		float	attackRate		= 0.1f;

		while ( true )
		{
			// 발사체 생성
			GameObject clone = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
			// 발사체 이동 방향
			Vector3 direction = (targetPosition - clone.transform.position).normalized;
			// 발사체 이동 방향 설정
			clone.GetComponent<Movement2D>().MoveTo(direction);

			// attackRate 시간만큼 대기
			yield return new WaitForSeconds(attackRate);
		}
	}
}


/*
 * File : BossWeapon.cs
 * Desc
 *	: 보스 캐릭터의 공격 관리. 발사체 생성
 *	
 * Functions
 *	: StartFiring() - 공격 시작
 *	: StopFiring() - 공격 중지
 *	: CircleFire() - 원형 방사
 *	: SingleFireToCenterPosition() - 중앙위치를 지나는 발사체 하나 생성
 *	
 */