using UnityEngine;

public class ParticleAutoDestroyer : MonoBehaviour
{
	private	ParticleSystem particle;

	private void Awake()
	{
		particle = GetComponent<ParticleSystem>();
	}

	private void Update()
	{
		// 파티클이 재생중이 아니면 삭제
		if ( particle.isPlaying == false )
		{
			Destroy(gameObject);
		}
	}
}


/*
 * File : ParticleAutoDestroyer.cs
 * Desc
 *	: 파티클 오브젝트에 부착해서 사용
 *	: 파티클의 재생이 완료되면 오브젝트 삭제
 *	
 */