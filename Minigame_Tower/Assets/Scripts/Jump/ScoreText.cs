using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // UI를 사용하기 위해 UnityEngine.UI를 사용한다고 Using 선언

public class ScoreText : MonoBehaviour
{
    int score = 0; //  점수를 저장하기 위해서 score 변수를 선언
    UnityEngine.UI.Text text_score; // score를 표시해줄 UI text

    private void Awake()
    {
        text_score = GetComponent<UnityEngine.UI.Text>(); // Text에다가 바로 스크립트를 생성했기 때문에 GetComponent를 하면 Ui Text를 가져올 수 있다
    }
    public void AddPoint() // AddPoint함수가 호출이 되면 score += 1;
    {
        score += 1; // score를 + 해주고
        UpdateTextUi(); // Ui Text에 숫자 출력
    }
    public void UpdateTextUi()
    {
        text_score.text = score.ToString(); // text_score라는 UiText의 text 에다가 score.ToString()을 저장                                            // score.ToString(): Int형인 score를 string의 형태, 즉 '문자열'로 변환을 시켜서
                                            // text_score.text 값에 넣어주는 것
    }

    public int GetScore()
    {
        return score;
    }

    //******************** 새로 추가된 부분 *********************//
    string highScoreKey = "HighScore";
    public int Get_HighScore()
    {
        int highScore = PlayerPrefs.GetInt(highScoreKey);
        return highScore;
    }
    public void Set_HightScore(int cur_score)
    {
        if (cur_score > Get_HighScore())
        {
            PlayerPrefs.SetInt(highScoreKey, cur_score);
        }
    }
    //***********************************************************//
}