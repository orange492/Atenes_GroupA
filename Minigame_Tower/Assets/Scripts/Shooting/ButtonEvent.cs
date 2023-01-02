using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonEvent : MonoBehaviour
{
	public void SceneLoader(string sceneName)
	{
		SceneManager.LoadScene(sceneName);
	}
}


/*
 * File : ButtonEvent.cs
 * Desc
 *	: Button UI 오브젝트에 부착해서 사용
 *	: 버튼을 눌렀을 때 호출되는 함수들을 작성
 *	
 * Functions
 *	: SceneLoader() - 씬 전환
 *	
 */