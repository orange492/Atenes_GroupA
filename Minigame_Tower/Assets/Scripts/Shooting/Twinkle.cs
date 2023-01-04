using System.Collections;
using UnityEngine;

public class Twinkle : MonoBehaviour
{
	private	float			fadeTime = 0.1f;
	private	SpriteRenderer	spriteRenderer;

	private void Awake()
	{
		spriteRenderer	= GetComponent<SpriteRenderer>();
		
		StartCoroutine("TwinkleLoop");
	}

	private IEnumerator TwinkleLoop()
	{
		while ( true )
		{
			// Alpha 값을 1에서 0으로 : Fade Out
			yield return StartCoroutine(FadeEffect(1, 0));
			// Alpha 값을 0에서 1로 : Fade In
			yield return StartCoroutine(FadeEffect(0, 1));
		}
	}

	private IEnumerator FadeEffect(float start, float end)
	{
		float currentTime	= 0.0f;
		float percent		= 0.0f;
		
		while ( percent < 1 )
		{
			// fadeTime 시간동안 while() 반복문 실행
			currentTime += Time.deltaTime;
			percent = currentTime / fadeTime;
			
			// 유니티의 클래스에 설정되어 있는 spriteRenderer.color, transform.position은 프로퍼티로
			// spriteRenderer.color.a = 1.0f과 같이 설정이 불가능하다
			// spriteRenderer.color = new Color(spriterRenderer.color.r, .., .., 1.0f); 과 같이 설정해야 한다
			Color color = spriteRenderer.color;
			color.a = Mathf.Lerp(start, end, percent);
			spriteRenderer.color = color;

			yield return null;
		}
	}
}


/*
 * File : Twinkle.cs
 * Desc
 *	: 오브젝트의 깜박임 처리 (Fade In, Fade Out Loop)
 *	
 * Functions
 *	: TwinkleLoop() - Fade In/Out을 무한루프
 *	: FadeEffect() - Fade In or Out을 재생
 */