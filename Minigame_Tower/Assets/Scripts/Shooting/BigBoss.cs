using System.Collections;
using UnityEngine;

public enum BossState { MoveToAppearPoint = 0, Phase01, Phase02, Phase03 }

public class BigBoss : MonoBehaviour
{
	[SerializeField]
	private	StageData		 stageData;
	[SerializeField]
	private	GameObject		 explosionPrefab;
	[SerializeField]
	private	PlayerController playerController;
	[SerializeField]
	private	string			 nextSceneName;		// 다음 씬 이름 (다음 스테이지 or 게임 클리어)
	[SerializeField]
	private	float			 bossAppearPoint = 2.5f;
	private	BossState		 bossState = BossState.MoveToAppearPoint;
	private	Movement2D		 movement2D;
	private	BossWeapon		 bossWeapon;
	private	BossHP			 bossHP;

	private void Awake()
	{
		movement2D	= GetComponent<Movement2D>();
		bossWeapon	= GetComponent<BossWeapon>();
		bossHP		= GetComponent<BossHP>();
		Phase01();

    }

	public void ChangeState(BossState newState)
	{
		// Tip. 열거형 변수.ToString()을 하게 되면 열거형에 정의된
		// 변수 이름을 string으로 받아오게 된다.
		// ex) bossState가 현재 BossState.MoveToAppearPoint이면 "MoveToAppearPoint"
		
		// 이를 이용해 열거형의 이름과 코루틴 이름을 일치시켜
		// 열거형 변수에 따라 코루틴 함수 재생을 제어할 수 있다.

		// 이전에 재생중이던 상태 종료
		StopCoroutine(bossState.ToString());
		// 상태 변경
		bossState = newState;
		// 새로운 상태 재생
		StartCoroutine(bossState.ToString());
	}

	private IEnumerator MoveToAppearPoint()
	{
		// 이동방향 설정 [코루틴 실행 시 1회 호출]
		movement2D.MoveTo(Vector3.down);

		while (true)
		{
			if ( transform.position.y <= bossAppearPoint )
			{
				// 이동방향을 (0, 0, 0)으로 설정해 멈추도록 한다.
				movement2D.MoveTo(Vector3.zero);
				// Phase01 상태로 변경
				ChangeState(BossState.Phase01);
			}

			yield return null;
		}
	}

	private IEnumerator Phase01()
	{
		// 원 형태의 방사 공격 시작
		bossWeapon.StartFiring(AttackType.CircleFire);

		while ( true )
		{
			// 보스의 현재 체력이 70% 이하가 되면
			if ( bossHP.CurrentHP <= bossHP.MaxHP * 0.7f )
			{
				// 원 방사 형태의 공격 중지
				bossWeapon.StopFiring(AttackType.CircleFire);
				// Phase02로 변경
				ChangeState(BossState.Phase02);
			}
			yield return null;
		}
	}

	private IEnumerator Phase02()
	{
		// 플레이어 위치를 기준으로 단일 발사체 공격 시작
		bossWeapon.StartFiring(AttackType.SingleFireToCenterPosition);

		// 처음 이동 방향을 오른쪽으로 설정
		Vector3 direction = Vector3.right;
		movement2D.MoveTo(direction);

		while ( true )
		{
			// 좌-우 이동 중 양쪽 끝에 도달하게 되면 방향을 반대로 설정
			if ( transform.position.x <= stageData.LimitMin.x ||
				 transform.position.x >= stageData.LimitMax.x )
			{
				direction *= -1;
				movement2D.MoveTo(direction);
			}

			// 보스의 현재 체력이 30% 이하가 되면
			if ( bossHP.CurrentHP <= bossHP.MaxHP * 0.3f )
			{
				// 플레이어 위치를 기준으로 단일 발사체 공격 시작
				bossWeapon.StopFiring(AttackType.SingleFireToCenterPosition);
				// Phase03으로 변경
				ChangeState(BossState.Phase03);
			}

			yield return null;
		}
	}

	private IEnumerator Phase03()
	{
		// 원 방사 형태의 공격 시작
		bossWeapon.StartFiring(AttackType.CircleFire);
		// 플레이어 위치를 기준으로 단일 발사체 공격 시작
		bossWeapon.StartFiring(AttackType.SingleFireToCenterPosition);

		// 처음 이동 방향을 오른쪽으로 설정
		Vector3 direction = Vector3.right;
		movement2D.MoveTo(direction);

		while ( true )
		{
			// 좌-우 이동 중 양쪽 끝에 도달하게 되면 방향을 반대로 설정
			if ( transform.position.x <= stageData.LimitMin.x ||
				 transform.position.x >= stageData.LimitMax.x )
			{
				direction *= -1;
				movement2D.MoveTo(direction);
			}

			yield return null;
		}
	}

	public void OnDie()
	{
		// 보스 파괴 연출
	 	GameObject clone = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
		// 파괴연출 후 씬 전환을 위한 설정
		clone.GetComponent<BossExplosion>().Setup(playerController, nextSceneName);
		// 보스 오브젝트 삭제
		Destroy(gameObject);
	}
}


/*
 * File : Boss.cs
 * Desc
 *	: 보스 오브젝트를 제어하고, 각 페이즈별 보스의 행동 설정
 *	
 * Functions
 *  : ChangeState() - 열거형 이름을 기반으로 코루틴 함수 중지/실행
 *	: MoveToAppearPoint() - (0, LimitMax.y+1, 0) 위치에서 등장 후 아래로 이동 (0, 2.5, 0)
 *	: Phase01() - 이동 : X, 공격 : 원형 방사
 *	: Phase02() - 이동 : 좌-우, 공격 : 단일 발사(보스위치->중앙위치)
 *	: Phase03() - 이동 : 좌-우, 공격 : Phase01 + Phase02
 *	: OnDie() - 보스 사망 시 호출
 */