using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.SceneManagement;

public class EggGameManager : SingletonPuzzle<EggGameManager>
{


    public enum Mode
    {
        ReadyToPlay,
        Editting,
        Die,
        Play,
        Clear,
        Intro
    }

    public List<GameObject> lineList = new();

    float money=1000.0f;
    public float Money
    {
        get => money;
        set 
        {
            money = value;
        }

    }

    /// <summary>
    /// 아이템 데이터를 관리하는 메니저
    /// </summary>
    ItemDataManager itemData;

    private Mode _mode;
    public Mode mode
    {
        get => _mode;
        set
        {
            _mode = value;
            onModeChange?.Invoke(mode);
        }
    }

    public Action<Mode> onModeChange;
    Shop shop;

    /// <summary>
    /// 아이템 데이터 메니저(읽기전용) 프로퍼티
    /// </summary>
    public ItemDataManager ItemData => itemData;

    public bool isSlingShot;
    public bool isDetection;
    public bool isParachute;

    public enum item
    {
        None,
        Propeller,
        Rocket
    }

    public item[] SlotItems;

    // 함수 ---------------------------------------------------------------------------------------



    private void Start()
    {
    
            SlotItems = new item[8];
            mode = Mode.Intro;
 
     
    }
    /// <summary>
    /// 게임 메니저가 새로 만들어지거나 씬이 로드 되었을 때 실행될 초기화 함수
    /// </summary>
    protected override void Initialize()
    {
        base.Initialize();

        itemData = GetComponent<ItemDataManager>();
        shop = FindObjectOfType<Shop>();

    }
}