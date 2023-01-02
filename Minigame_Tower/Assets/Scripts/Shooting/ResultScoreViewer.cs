using UnityEngine;
using TMPro;

public class ResultScoreViewer : MonoBehaviour
{
	private	TextMeshProUGUI	textResultScore;

	private void Awake()
	{
		textResultScore = GetComponent<TextMeshProUGUI>();
		// Stage에서 저장한 점수를 불러와서 score 변수에 저장
		int score = PlayerPrefs.GetInt("Score");
		// textResultScore UI에 점수 갱신
		textResultScore.text = "Result Score "+score;
	}
}


/*
 * File : ResultScoreViewer.cs
 * Desc
 *	: 스테이지에서 획득한 점수 정보를 Text UI에 업데이트
 *	
 */