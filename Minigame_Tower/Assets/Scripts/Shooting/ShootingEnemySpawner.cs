using System.Collections;
using UnityEngine;

public class ShootingEnemySpawner : MonoBehaviour
{
	[SerializeField]
	private	StageData		stageData;				// 적 생성을 위한 스테이지 크기 정보
	[SerializeField]
	private	GameObject		enemyPrefab;			// 복제해서 생성할 적 캐릭터 프리팹
	[SerializeField]
	private	GameObject		enemyHPSliderPrefab;	// 적 체력을 나타내는 Slider UI 프리팹
	[SerializeField]
	private	Transform		canvasTransform;		// UI를 표현하는 Canvas 오브젝트의 Transform
	//[SerializeField]
	//private	BGMController	bgmController;			// 배경음악 설정 (보스 등장 시 변경)
	[SerializeField]
	private	GameObject		textBossWarning;		// 보스 등장 텍스트 오브젝트
	[SerializeField]
	private	GameObject		boss;					// 보스 오브젝트
	[SerializeField]
	private	GameObject		panelBossHP;			// 보스 체력 패널 오브젝트
	[SerializeField]
	private	float			spawnTime;				// 생성 주기
	[SerializeField]
	private	int				maxEnemyCount = 100;	// 현재 스테이지의 최대 적 생성 숫자

	private void Awake()
	{
		// 보스 등장 텍스트 비활성화
		textBossWarning.SetActive(false);
		// 보스 체력 패널 비활성화
		panelBossHP.SetActive(false);
		// 보스 오브젝트 비활성화
		//boss.SetActive(false);

		StartCoroutine("SpawnEnemy");
		//SpawnBoss();

    }

	private IEnumerator SpawnEnemy()
	{
		int currentEnemyCount = 0;	// 적 생성 숫자 카운트

		while ( true )
		{
			// x 위치는 스테이지의 크기 범위 내에서 임의의 값을 선택
			float	positionX = Random.Range(stageData.LimitMin.x, stageData.LimitMax.x);
			// 적 생성 위치
			Vector3	position  = new Vector3(positionX, stageData.LimitMax.y+1.0f, 0.0f);
			// 적 캐릭터 생성
			GameObject enemyClone = Instantiate(enemyPrefab, position, Quaternion.identity);
			
			// 적 생성 숫자 증가
			currentEnemyCount ++;
			// 적을 최대 숫자까지 생성하면 적 생성 코루틴 중지, 보스 생성 코루틴 실행
			if ( currentEnemyCount == maxEnemyCount )
			{
				StartCoroutine("SpawnBoss");
				break;
			}

			// spawnTime만큼 대기
			yield return new WaitForSeconds(spawnTime);
		}
	}

	private void SpawnEnemyHPSlider(GameObject enemy)
	{
		// 적 체력을 나타내는 Slider UI 생성
		GameObject sliderClone = Instantiate(enemyHPSliderPrefab);
		// Slider UI 오브젝트를 parent("Canvas" 오브젝트)의 자식으로 설정
		// Tip. UI는 캔버스의 자식오브젝트로 설정되어 있어야 화면에 보인다
		sliderClone.transform.SetParent(canvasTransform);
		// 계층 설정으로 바뀐 크기를 다시 (1, 1, 1)로 설정
		sliderClone.transform.localScale = Vector3.one;

		// Slider UI가 쫓아다닐 대상을 본인으로 설정
		sliderClone.GetComponent<SliderPositionAutoSetter>().Setup(enemy.transform);
		// Slider UI에 자신의 체력 정보를 표시하도록 설정
		sliderClone.GetComponent<EnemyHPViewer>().Setup(enemy.GetComponent<EnemyHP>());
	}

	private IEnumerator SpawnBoss()
	{
		// 보스 등장 BGM 설정
		//bgmController.ChangeBGM(BGMType.Boss);	// bgmController.ChangeBGM(1); 보다 가독성이 좋다.
		// 보스 등장 텍스트 활성화
		textBossWarning.SetActive(true);
		// 1초 대기
		yield return new WaitForSeconds(1.0f);

		// 보스 등장 텍스트 비활성화
		textBossWarning.SetActive(false);
		// 보스 체력 패널 활성화
		panelBossHP.SetActive(true);
		// 보스 오브젝트 활성화
		boss.SetActive(true);
		// 보스의 첫 번째 상태인 지정된 위치로 이동 실행
		boss.GetComponent<BigBoss>().ChangeState(BossState.MoveToAppearPoint);
	}
}


/*
 * File : EnemySpawner.cs
 * Desc
 *	: 일정시간마다 적 캐릭터 생성
 *	
 * Functions
 *	: SpawnEnemy() - 적 생성
 *	: SpawnEnemyHPSlider() - 적 체력 표시 Slider UI 생성
 *	: SpawnBoss() - 보스 생성
 *	
 */