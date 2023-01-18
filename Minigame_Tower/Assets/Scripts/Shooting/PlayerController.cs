using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Build;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
	[SerializeField]
	private	string		nextSceneName;
	[SerializeField]
	private	StageData	stageData;
	[SerializeField]
	private	KeyCode		keyCodeAttack = KeyCode.Space;
	[SerializeField]
	private	KeyCode		keyCodeBoom = KeyCode.Z;
	private	bool		isDie = false;
	private Movement2D	movement2D;
	private	Weapon		weapon;
	private	Animator	animator;
    private InputActions inputActions;
    private Vector3 dir;
	


    private	int			score;
	public	int			Score
	{
		// score 값이 음수가 되지 않도록
		set => score = Mathf.Max(0, value);
		get => score;
	}

  

    private void Awake()
	{
        inputActions = new InputActions();
        movement2D	= GetComponent<Movement2D>();
		weapon		= GetComponent<Weapon>();
		animator	= GetComponent<Animator>();
	}

    private void OnDisable()
    {
        inputActions.Shooting.Move.canceled -= OnMove;    // 연결해 놓은 함수 해제(안전을 위해)
        inputActions.Shooting.Move.performed -= OnMove;
        inputActions.Shooting.Disable();  // 오브젝트가 사라질때 더 이상 입력을 받지 않도록 비활성화
    }

    private void OnEnable()
    {
        inputActions.Shooting.Enable();
        inputActions.Shooting.Move.performed += OnMove;   // Move액션이 performed 일 때 OnMove 함수 실행하도록 연결
        inputActions.Shooting.Move.canceled += OnMove;    // Move액션이 canceled 일 때 OnMove 함수 실행하도록 연결

    }

    private void OnMove(InputAction.CallbackContext context)
    {
        // Exception : 예외 상황( 무엇을 해야 할지 지정이 안되어있는 예외 일때 )
        //throw new NotImplementedException();    // NotImplementedException 을 실행해라. => 코드 구현을 알려주기 위해 강제로 죽이는 코드

        //Debug.Log("이동 입력");
        dir = context.ReadValue<Vector2>();    // 어느 방향으로 움직여야 하는지를 입력받음

        //dir.y > 0   // W를 눌렀다.
        //dir.y == 0  // W,S 중 아무것도 안눌렀다.
        //dir.y < 0   // S를 눌렀다.
        animator.SetInteger("Input", (int)dir.x); // 애니메이션 적용 좌우 이동시
    }
    private void Update()
	{
		// 플레이어가 사망 애니메이션 재생 중일 때 이동, 공격이 불가능하게 설정
		if ( isDie == true ) return;

		// 방향 키를 눌러 이동 방향 설정
		float x = Input.GetAxisRaw("Horizontal");
		float y = Input.GetAxisRaw("Vertical");

		movement2D.MoveTo(new Vector3(x, y, 0));

		// 공격 키를 Down/Up으로 공격 시작/종료
		if ( Input.GetKeyDown(keyCodeAttack) )
		{
			weapon.StartFiring();
		}
		else if ( Input.GetKeyUp(keyCodeAttack) )
		{
			weapon.StopFiring();
		}

		// 폭탄 키를 눌러 폭탄 생성
		if ( Input.GetKeyDown(keyCodeBoom) )
		{
			weapon.StartBoom();
            //weapon.StartCoroutine(boomCoroutine());
            //StartCoroutine(boomCoroutine());
        }
	}

	private void LateUpdate()
	{
		// 플레이어 캐릭터가 화면 범위 바깥으로 나가지 못하도록 함
		transform.position = new Vector3(Mathf.Clamp(transform.position.x, stageData.LimitMin.x, stageData.LimitMax.x),
										 Mathf.Clamp(transform.position.y, stageData.LimitMin.y, stageData.LimitMax.y));
	}

	public void OnDie()
	{
		// 공격 중지
		weapon.StopFiring();
		// 이동 방향 초기화
		movement2D.MoveTo(Vector3.zero);
		// 사망 애니메이션 재생 -> 어차피 씬 변환 바로되니까 필요없음
		//animator.SetTrigger("onDie");
		// 적들과 충돌하지 않도록 충돌 박스 삭제
		Destroy(GetComponent<CircleCollider2D>());
		// 사망 시 키 플레이어 조작 등을 하지 못하게 하는 변수
		isDie = true;
		// 플레이어 사망시 다시시작 결과창 출력
		
	    SceneManager.LoadScene(10);
		
    }

	public void OnDie_Hard() 
	{
        // 공격 중지
        weapon.StopFiring();
        // 이동 방향 초기화
        movement2D.MoveTo(Vector3.zero);
        // 사망 애니메이션 재생
        //animator.SetTrigger("onDie");
        // 적들과 충돌하지 않도록 충돌 박스 삭제
        Destroy(GetComponent<CircleCollider2D>());
        // 사망 시 키 플레이어 조작 등을 하지 못하게 하는 변수
        isDie = true;
        // 플레이어 사망시 다시시작 결과창 출력

        SceneManager.LoadScene(11);
    }

	public void OnDieEvent()
	{
		// 디바이스에 획득한 점수 score 저장
		PlayerPrefs.SetInt("Score", score);
        // 플레이어 사망 시 nextSceneName 씬으로 이동
        //SceneManager.LoadScene(nextSceneName);
        
    }
}


/*
 * File : PlayerController.cs
 * Desc
 *	: 플레이어 캐릭터에 부착해서 사용
 *	
 * Functions
 *	: OnDie() - 플레이어 사망 시 호출하는 함수
 *	: OnDieEvent() - 사망 애니메이션 재생 직후 호출하는 함수
 */