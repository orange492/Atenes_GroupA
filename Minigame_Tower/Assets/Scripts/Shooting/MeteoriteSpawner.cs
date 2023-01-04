using System.Collections;
using UnityEngine;

public class MeteoriteSpawner : MonoBehaviour
{
	[SerializeField]
	private	StageData	stageData;
	[SerializeField]
	private	GameObject	alertLinePrefab;
	[SerializeField]
	private	GameObject	meteoritePrefab;
	[SerializeField]
	private	float		minSpawnTime = 1.0f;
	[SerializeField]
	private	float		maxSpawnTime = 4.0f;

	private void Awake()
	{
		StartCoroutine("SpawnMeteorite");
	}

	private IEnumerator SpawnMeteorite()
	{
		while ( true )
		{
			// x 위치는 스테이지의 크기 범위 내에서 임의의 값을 선택
			float positionX = Random.Range(stageData.LimitMin.x, stageData.LimitMax.x);
			// 경고선 오브젝트 생성
			GameObject alertLineClone = Instantiate(alertLinePrefab, new Vector3(positionX, 0, 0), Quaternion.identity);
			
			// 1초간 대기
			yield return new WaitForSeconds(1.0f);

			// 경고선 오브젝트 삭제
			Destroy(alertLineClone);

			// 메테오 오브젝트 생성
			Vector3 meteoritePosition = new Vector3(positionX, stageData.LimitMax.y + 1.0f, 0);
			Instantiate(meteoritePrefab, meteoritePosition, Quaternion.identity);

			// 대기 시간 설정 (minSpawnTime ~ maxSpawnTime)
			float spawnTime = Random.Range(minSpawnTime, maxSpawnTime);
			// 해당 시간만큼 대기 후 다음 로직 실행
			yield return new WaitForSeconds(spawnTime);
		}
	}
}


/*
 * File : MeteoriteSpawner.cs
 * Desc
 *	: minSpawnTime ~ maxSpawnTime 시간마다 장애물을 생성
 *	
 * Functions
 *	: SpawnMeteorite() - 적 생성
 *	
 */