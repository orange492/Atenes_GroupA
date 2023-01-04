using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 이 스크립트를 가지는 게임 오브젝트는 반드시 Animator를 가진다.
[RequireComponent(typeof(Animator))]
public class Explosion : MonoBehaviour
{
    Animator anim;
    private void Awake()
    {
        anim = GetComponent<Animator>(); //폭발애니메이션 실행위한 컴포넌트 불러오기
    }

    /// <summary>
    /// 애니메이션 작동 후 바로 삭제하는 함수
    /// </summary>
    private void OnEnable()
    {
        // 이 게임 오브젝트가 활성화가 되면 -> OnEnable() 함수 이용
        // anim.GetCurrentAnimatorClipInfo(0)[0].clip.length초 후에 이 게임 오브젝트를 삭제하라
        Destroy(this.gameObject, anim.GetCurrentAnimatorClipInfo(0)[0].clip.length);
    }
}
