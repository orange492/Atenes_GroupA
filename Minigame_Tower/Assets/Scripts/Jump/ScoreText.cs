using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // UI를 사용하기 위해서는 UnityEngine.UI를 사용한다고 Using 선언
using static System.Net.Mime.MediaTypeNames;

public class ScoreText : MonoBehaviour
{
    int score = 0; //  점수를 저장하기 위해서 score 변수를 선언
    UnityEngine.UI.Text text_score; // score를 표시해줄 UI text

    private void Awake()
    {
        text_score = GetComponent<UnityEngine.UI.Text>(); // Text에다가 바로 스크립트를 생성했기 때문에 GetComponent를 하면 Ui Text를 가져올 수 있음
    }
    public void AddPoint()
    {
        score += 1; // 이런식으로 score를 +
        UpdateTextUi(); // Ui Text에 숫자를 출력
    }
    public void UpdateTextUi()
    {
        text_score.text = score.ToString(); // text_score라는 UiText의 text 에다가 score.ToString()을 저장
    }
}