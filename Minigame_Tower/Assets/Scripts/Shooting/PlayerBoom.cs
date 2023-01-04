using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerBoom : MonoBehaviour
{
	[SerializeField]
	private	AnimationCurve	curve;

	[SerializeField]
	private	int				damage = 100;		// 폭탄 데미지
	private	float			boomDelay = 0.5f;	// 폭탄 이동 시간 (0.5초 후 폭발)
	private	Animator		animator;
   
	public GameObject BoomEffect;
    private void Awake()
	{
		animator	= GetComponent<Animator>();
        
        //StartCoroutine("MoveToCenter");
    }

	
	private IEnumerator MoveToCenter()
	{
		Vector3	startPosition	= transform.position;
		Vector3	endPosition		= Vector3.zero;
		float	current			= 0;
		float	percent			= 0;

		while ( percent < 1 )
		{
			current += Time.deltaTime;
			percent = current / boomDelay;

			// boomDelay에 설정된 시간동안 startPosition부터 endPosition까지 이동
			// curve에 설정된 그래프처럼 처음엔 빠르게 이동하고, 목적지에 다다를수록 천천히 이동
			transform.position = Vector3.Lerp(startPosition, endPosition, curve.Evaluate(percent));

			yield return null;
		}

		// 이동이 완료된 후 애니메이션 변경
		animator.SetTrigger("onBoom");
		// 사운드 변경
		
	}

    IEnumerator boomCoroutine()
    {
        yield return new WaitForSeconds(4.0f);
        BoomEffect.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {


        if (collision.gameObject.CompareTag("Enemy"))
        {
            //Debug.Log(collision);
            //collision.gameObject.SetActive(false);

            // 적들 총알 찾아서 폭탄에 닫으면 없애버리기
           // GameObject[] enemys = GameObject.FindGameObjectsWithTag("Enemy"); 
            
            //for (int i = 0; i < enemys.Length; i++)
            //{
            //    enemys[i].gameObject.SetActive(false);

            //}
            Destroy(collision.gameObject); // 충돌하는 모든 물체 삭제 -> 애니메이션까지 정지시킴
            StartCoroutine(boomCoroutine());

        }
        else if (collision.gameObject.CompareTag("Meteorite"))
        {
            //Debug.Log(collision);
            //collision.gameObject.SetActive(false);

            // 적들 총알 찾아서 폭탄에 닫으면 없애버리기
            // GameObject[] enemys = GameObject.FindGameObjectsWithTag("Enemy"); 

            //for (int i = 0; i < enemys.Length; i++)
            //{
            //    enemys[i].gameObject.SetActive(false);

            //}
            Destroy(collision.gameObject); // 충돌하는 모든 물체 삭제 -> 애니메이션까지 정지시킴
            StartCoroutine(boomCoroutine());

        }



    }
 //   public void OnBoom()
	//{
	//	// 현재 게임 내에서 "Enemy" 태그를 가진 모든 오브젝트 정보를 가져온다
	//	GameObject[] enemys = GameObject.FindGameObjectsWithTag("Enemy");
	//	// 현재 게임 내에서 "Meteorite" 태그를 가진 모든 오브젝트 정보를 가져온다
	//	GameObject[] meteorites = GameObject.FindGameObjectsWithTag("Meteorite");

	//	// 모든 적 파괴
	//	for ( int i = 0; i < enemys.Length; ++ i )
	//	{
	//		enemys[i].GetComponent<ShootingEnemy>().OnDie();
	//	}

	//	// 모든 운석 파괴
	//	for ( int i = 0; i < meteorites.Length; ++ i )
	//	{
	//		meteorites[i].GetComponent<Meteorite>().OnDie();
	//	}
		
	//	// 현재 게임 내에 존재하는 적, 보스의 발사체를 모두 파괴
	//	GameObject[] projectils = GameObject.FindGameObjectsWithTag("EnemyProjectile");
	//	for ( int i = 0; i < projectils.Length; ++ i)
	//	{
	//		projectils[i].GetComponent<EnemyProjectile>().OnDie();
	//	}

	//	// 현재 게임 내에서 "Boss" 태그를 가진 오브젝트 정보를 가져온다
	//	GameObject boss = GameObject.FindGameObjectWithTag("Boss");
	//	if ( boss != null )
	//	{
	//		// 보스의 체력을 damage만큼 감소시킨다
	//		boss.GetComponent<BossHP>().TakeDamage(damage);
	//	}

	//	// Boom 오브젝트 삭제
	//	Destroy(gameObject);
	//}



}


/*
 * File : PlayerBoom.cs
 * Desc
 *	: 플레이어의 폭탄으로 지정된 위치로 이동 후 적, 운석 모두 삭제
 *
 * Functions
 *	: MoveToCenter() - 지정된 위치(Vector3.zero)로 폭탄 오브젝트 이동
 *	: OnBoom() - 현재 게임 월드에 존재하는 적, 운석을 찾아 모두 삭제
 *	
 */