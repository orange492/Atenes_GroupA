using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(CanvasGroup))]

public class BlackjackManager : Singletons<BlackjackManager>
{   
    //델리게이트 실행을 위해서 불러오는 셋팅
    PlayerScript player;
     
    public PlayerScript Player => player;
    public ResultPannel rp;

    // Game Buttons
    public Button dealBtn; // 딜, 카드를 나누는 버튼(셔플 개념) -> 패돌림 -> 리셋!
    public Button hitBtn; // 히트, 카드 더 뽑아오는 버튼 -> 한장더
    public Button standBtn; // 스탠드, 카드를 뽑지 않고 차례 마치기 ->턴쉰다
    public Button betBtn; // 배팅, 칩을 더 건다는 버튼 ->칩걸기 ->> 항상 고정적으로 배팅될수 있게 변경 필요

  



    private int standClicks = 0; // 처음 스킵하는 버튼 횟수는 0으로 초기화

    //  플레이어와 딜러 스크립트에 접근하는 것
    public PlayerScript playerScript; // 플레이어 스크립트 적용시키기
    public PlayerScript2 dealerScript; // 딜러 스크립트 적용 시키기

    // 업데이트 할때마다 보이는 텍스트 만들기
    public TextMeshProUGUI scoreText; // 플레이어 점수 스코어 , 21을 생각하고
    public TextMeshProUGUI dealerScoreText; // 딜러 점수 스코어 ,21을 생각하고
    public TextMeshProUGUI betsText; // 배팅 거는 숫자 표시, 얼마 걸지
    public TextMeshProUGUI cashText; // 얼마 남아 있는지... HP 개념
    public TextMeshProUGUI mainText; // 승패 판정할때 뜨는 텍스트
    public TextMeshProUGUI standBtnText; // 콜 얼마걸지 배팅하는데 나오는 텍스트


    //적 스테이터스 개념... -> 추후 보이도록 변경필요함!
    public TextMeshProUGUI scoreText2; // 플레이어 점수 스코어 , 21을 생각하고
    public TextMeshProUGUI betsText2; // 배팅 거는 숫자 표시, 얼마 걸지
    public TextMeshProUGUI cashText2; // 얼마 남아 있는지... HP 개념

    public Action onDead; // 죽을때 실행되는 델리게이트
    public Action onGameStart; // 게임 처음실행될때 실행 되는 델리게이트
    public Action onResulintg;

  


    //  딜러는 2번째 카드 부터 숨긴다 
    public GameObject hideCard;
    //얼마나 걸건지, 기본 배팅단위
    public int pot = 20;

    void Start()
    {
        
        //버튼 생성값으로 가져오기(읽기) , get 과 같다.
        dealBtn.onClick.AddListener(() => DealClicked()); // 스크립트에 온클릭 리스너 추가 ,셔플개념
        hitBtn.onClick.AddListener(() => HitClicked()); // 스크립트에 온클릭 리스너 추가 ,카드 가져오기개념
        standBtn.onClick.AddListener(() => StandClicked()); //  스크립트에 온클릭 리스너 추가 ,한턴 쉬기 개념
        betBtn.onClick.AddListener(() => BetClicked()); //  스크립트에 온클릭 리스너 추가 ,배팅 개념
    }
    private void Awake()
    {
        
    }

    private void DealClicked() // 초기화 함수 -> 최종 승패 여기서 결정한다.
    {

        // 한라운드를 리샛하고 텍스트를 숨기고 새 덱으로 셔플한다.
        playerScript.ResetHand(); // 플레이어 의 핸드를 리셋
        dealerScript.ResetHand(); // 딜러의 핸드를 리셋
        dealerScoreText.gameObject.SetActive(false);  // 딜러의 점수 스코어를 안보이게 한다.
        mainText.gameObject.SetActive(false); // 게임의 메인 택스트를 안보이게 한다.
        dealerScoreText.gameObject.SetActive(false); // 딜러 점수 안 보이도록 한다.
        GameObject.Find("Deck").GetComponent<DeckScript>().Shuffle(); // 덱이라는 오브젝트 찾은 다음 덱스크립트를 셔플한다.
        playerScript.StartHand(); // 플레이의 핸드를 새로 가져옴
        dealerScript.StartHand(); // 딜러의 핸드를 새로 가져옴
                                  // 매프레임 마다 스크린 표시해주기
        scoreText.text = "손패점수: " + playerScript.handValue.ToString(); // 스코어텍스트 오브젝트의 text의 "Hand"내용에다가 플레이어 스크립트의 hand값 표시해주기
        dealerScoreText.text = "상대점수: " + dealerScript.handValue.ToString(); //스코어텍스트 오브젝트의 text의 "Hand"내용에다가 딜러 스크립트의 hand값 표시해주기
                                                                             // 딜러카드를 뒤로놓고 숨긴다
        hideCard.GetComponent<Renderer>().enabled = true; //숨긴 카드 표시
                                                          // 버튼들 보이게 조정하기
        dealBtn.gameObject.SetActive(false); // 패돌리는 버튼 안보이게 하기
        hitBtn.gameObject.SetActive(true); // 카드 한장 가져오는 버튼 보이기

        standBtn.gameObject.SetActive(true); //한 턴 쉬는 버튼 보이기
        standBtnText.text = "턴넘겨"; // 기본 베팅 단위 출력!
                                   //  기본 배팅금액 등등 설정
        pot = 20;
        betsText.text = "얼마걸까: $" + pot.ToString();

        //playerScript.AdjustMoney(-20);
        cashText.text = "$" + playerScript.GetMoney().ToString();
        cashText2.text = "$" + dealerScript.GetMoney().ToString();

        Resulting();

    }

    private void Resulting()
    {
        if (playerScript.money <= 0)   // 최종적으로 이길때 출력됨
        {
            Debug.Log("완전히 패배했습니다.");
            GameObject.Find("ResultPannel").GetComponent<ResultPannel>().Open();


        }
        else if (dealerScript.money <= 0)  // 최종적으로 졋을때 출력됨
        {
            Debug.Log("최종 승리했습니다");
            GameObject.Find("ResultPannel").GetComponent<ResultPannel>().Open();

        }
    }




    private void HitClicked() // 카드한장 가져오는 버튼 클릭시 발동되는 함수
    {

        if (playerScript.cardIndex <= 10)
        {
            playerScript.GetCard();
            scoreText.text = "손패점수: " + playerScript.handValue.ToString();

            if (playerScript.handValue > 20) RoundOver();
        }
    }

    private void StandClicked() // 턴넘기는 버튼 클릭시 발동되는 함수
    {
        standClicks++;
        if (standClicks > 1) RoundOver();
        HitDealer();
        standBtnText.text = "턴넘겨";
    }

    private void HitDealer() // 딜러가 카드 받아오는 함수 실행
    {
        while (dealerScript.handValue < 16 && dealerScript.cardIndex < 10)
        {
            dealerScript.GetCard();
            dealerScoreText.text = "손패점수: " + dealerScript.handValue.ToString();
            if (dealerScript.handValue > 20) RoundOver();
        }
    }

    /// <summary>
    /// 승패 판정하는 조건
    /// </summary>
    void RoundOver() // pot 걸은것도 다 승리하면 가져갈 수 있도록 변경 필요!!
    {
        // 논리형으로 승패 나눌 수 있도록 변수 생성
        bool playerBust = playerScript.handValue > 21; // 플레이어가 진다
        bool dealerBust = dealerScript.handValue > 21; //딜러가 진다
        bool playerLose = playerScript.money <= 0; // 플레이어 머니가 0보다 작아질때 확실한 패배! -> 추후 딜러 결과출력 UI 적용할때 다시 만들기
        bool player21 = playerScript.handValue == 21; // 플레이어가 이긴다
        bool dealer21 = dealerScript.handValue == 21; // 딜러가 이긴다
        // 턴 2번 이상 돌렷을때 그리고 플레이어랑 딜러 둘다 21이 아닐때 -> 판정시작
        if (standClicks < 2 && !playerBust && !dealerBust && !player21 && !dealer21)
            return;
        bool roundOver = true;
        // 다 21이 넘었을때
        if (playerBust && dealerBust)
        {
            mainText.text = "무승부 입니다(버스트)";
            playerScript.AdjustMoney(pot / 2);
            dealerScript.AdjustMoney(pot / 2); // 딜러도 동일하게 받기
        }
        // 플레이어만 21넘고 딜러는 안넘었을때 또는 딜러가 버스트 상태이며 딜러의 손패점수가 플레이어보다 높을때
        else if (playerBust || (!dealerBust && dealerScript.handValue > playerScript.handValue))
        {
            mainText.text = "너가 졌어";
            playerScript.AdjustMoney2(100); // 내가 돈 100원 빼기
            dealerScript.AdjustMoney(100); // 딜러가 돈 100가져가기
            if (pot > 0)
            {
                playerScript.AdjustMoney(-pot); // 패배시 걸었던 pot 감소
            }
        }
        // 딜러만만 21넘고 딜러는 안넘었을때 또는 플레이어가 버스트 상태이며 딜러의 손패점수가 플레이어보다 높을때
        else if (dealerBust || playerScript.handValue > dealerScript.handValue)
        {
            mainText.text = "나의 승리";
            playerScript.AdjustMoney(100); // 내가 돈 100가져오기
            dealerScript.AdjustMoney2(100); // 딜러 돈 100빼기
            if (pot > 0) 
            {
                playerScript.AdjustMoney(pot); // 승리시 걸었던 pot 증가
            }
        }
        //둘다 값이 동일할때
        else if (playerScript.handValue == dealerScript.handValue)
        {
            mainText.text = "무승부 입니다!(값동일)";
            playerScript.AdjustMoney(pot / 2);
            dealerScript.AdjustMoney(pot / 2); // 딜러도 동일하게 받기
        }
        else
        {
            roundOver = false;
          
        }
        //다음턴 시작되면 리셋 되는 요소들
        if (roundOver)
        {
            hitBtn.gameObject.SetActive(false);
            standBtn.gameObject.SetActive(false);
            dealBtn.gameObject.SetActive(true);
            mainText.gameObject.SetActive(true);
            dealerScoreText.gameObject.SetActive(true);
            hideCard.GetComponent<Renderer>().enabled = false;
            cashText.text = "$" + playerScript.GetMoney().ToString();
            cashText2.text = "$" + dealerScript.GetMoney().ToString(); // 딜러금액도 판정해주기
            standClicks = 0;
            pot = 0; // 배팅금액 0원으로 초기화
        }
    }

    // 돈 베팅 버튼 누르면 진행되는 함수 -> 여기서 배팅 건거 자기만 다 먹을 수있도록 일단 만들기... 아직 컴퓨터 논리 로직은 완성안되었으니깐!!
    void BetClicked()
    {
        TextMeshProUGUI newBet = betBtn.GetComponentInChildren(typeof(TextMeshProUGUI)) as TextMeshProUGUI; // Text로 취급한다...
        int intBet = int.Parse(newBet.text.ToString().Remove(0, 1));
        playerScript.AdjustMoney(-intBet);
        cashText.text = "$" + playerScript.GetMoney().ToString();
        pot += (intBet * 2);
        betsText.text = "얼마걸까: $" + pot.ToString();
    }

  
}
