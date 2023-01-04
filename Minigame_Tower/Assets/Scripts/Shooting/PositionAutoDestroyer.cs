using UnityEngine;

public class PositionAutoDestroyer : MonoBehaviour
{
	[SerializeField]
	private	StageData	stageData;
	private	float		destroyWeight = 2.0f;

	private void LateUpdate()
	{
		if ( transform.position.y < stageData.LimitMin.y - destroyWeight ||
			 transform.position.y > stageData.LimitMax.y + destroyWeight ||
			 transform.position.x < stageData.LimitMin.x - destroyWeight ||
			 transform.position.x > stageData.LimitMax.x + destroyWeight )
		{
			Destroy(gameObject);
		}
	}
}


/*
 * File : PositionAutioDestroyer.cs
 * Desc
 *  : 화면 밖으로 나갈 수 있는 오브젝트에 부착해서 사용
 *	: 오브젝트가 일정 범위 바깥으로 나가면 삭제
 *	
 */