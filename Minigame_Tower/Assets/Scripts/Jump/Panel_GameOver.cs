using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Panel_GameOver : MonoBehaviour
{
    public UnityEngine.UI.Text Text_GameResult; // 게임의 결과를 표시해줄 Text UI

    private void Awake()
    {
        transform.gameObject.SetActive(false); // 게임이 시작되면 GameOver 팝업 창을 보이지 않게
    }

    public void Show()
    {
        transform.gameObject.SetActive(true); // GameOver 팝업 창을 화면에 표시

        int score = FindObjectOfType<ScoreText>().GetScore(); // ScoreText로 부터 현재 기록된 점수를 불러옴
        int highScore = FindObjectOfType<ScoreText>().Get_HighScore();  // ScoreText로 부터 최고점수를 불러옴

        Text_GameResult.text =
            "GameOver\n\n" +
            "HighScore : " + highScore.ToString() + "\n" +
            "Score : " + score.ToString();
    }

    public void OnClick_Retry() // '재도전' 버튼을 클릭하면 호출되는 함수
    {
        SceneManager.LoadScene("JumpScene"); // SceneManager의 LoadScene 함수를 사용하여 현재 신 'GameScene'을 다시 불러옴
                                             // 같은 신을 다시 불러오면 게임이 재시작 된다.
    }
}