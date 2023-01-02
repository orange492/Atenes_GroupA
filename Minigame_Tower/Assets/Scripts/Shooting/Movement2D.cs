using UnityEngine;

public class Movement2D : MonoBehaviour
{
	[SerializeField]
	private	float	moveSpeed = 0.0f;
	[SerializeField]
	private	Vector3	moveDirection = Vector3.zero;

	private void Update()
	{
		transform.position += moveDirection * moveSpeed * Time.deltaTime;
	}

	public void MoveTo(Vector3 direction)
	{
		moveDirection = direction;
	}
}


/*
 * File : Movement2D.cs
 * Desc
 *	: 이동이 가능한 모든 오브젝트에게 부착
 *	
 * Functions
 *	: MoveTo() - 외부에서 호출해 이동 방향을 설정
 *	
 */