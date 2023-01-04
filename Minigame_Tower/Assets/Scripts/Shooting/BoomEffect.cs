using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BoomEffect : MonoBehaviour
{
    public float speed = 0.2f;
    public float lifeTime = 4.0f;
    public GameObject hitEffect;


    private void Start()
    {
        
        // this는 이 클래스의 인스턴스(자기 자신)
    }

    private void Awake()
    {
        
    }
    private void Update()
    {
        //transform.Translate(speed * Time.deltaTime * new Vector3(1,0) );
        transform.Translate(speed * Time.deltaTime * Vector3.up, Space.Self);  // Space.Self : 자기 기준, Space.World : 씬 기준
    }

    // 충돌한 대상이 컬라이더일 때 실행
    //private void OnCollisionEnter2D(Collision2D collision)
    //{

    //    if (collision.gameObject.CompareTag("EnemyBullet"))
    //    {
    //        hitEffect.transform.parent = null; //이펙트를 부모관계(총알) 끊어버리기
    //        hitEffect.transform.position = collision.contacts[0].point; // 충돌한 대상의 위치에서 위치 시키기

    //        // collision.contacts[0].normal : 법선 백터(노멀 백터)
    //        // 노멀 백터 : 특정 평면에 수직인 백터
    //        // 노멀 백터는 반사를 계산하기 위해 반드시 필요하다. => 반사를 이용해서 그림자를 계산한다. 물리적인 반사도 계산한다.
    //        // 노멀 백터를 구하기 위해 백터의 외적을 사용한다.
    //        hitEffect.gameObject.SetActive(true);
    //        GameObject[] bullets = GameObject.FindGameObjectsWithTag("EnemyBullet");
    //        for (int i = 0; i < bullets.Length; i++)
    //        {
    //            Destroy(bullets[i]);
    //        }
    //        //Destroy(collision.gameObject); // 충돌하는 모든 물체 삭제 -> 애니메이션까지 정지시킴

    //    }


    //    // 매우 좋지 못한 코드
    //    //if (collision.gameObject.tag == "Enemy") 
    //    //{
    //    //}
    //    // 1. CompareTag는 숫자와 숫자를 비교하지만 == 은 문자열 비교라서 더 느리다.
    //    // 2. 필요 없는 가비지가 생긴다.

    //}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        
            if (collision.gameObject.CompareTag("EnemyBullet"))
            {
            //Debug.Log(collision);
            //collision.gameObject.SetActive(false);

            // 적들 총알 찾아서 폭탄에 닫으면 없애버리기
            GameObject[] bullets = GameObject.FindGameObjectsWithTag("EnemyBullet");
            for (int i = 0; i < bullets.Length; i++)
            {
                bullets[i].gameObject.SetActive(false);
            }
            Destroy(collision.gameObject); // 충돌하는 모든 물체 삭제 -> 애니메이션까지 정지시킴
        }
        
    }
}