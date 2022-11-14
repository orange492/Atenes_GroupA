using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{

    BlockController blockController;
    RemainTime remainTime;
    RemainScore remainScore;
    private void Awake()
    {
        blockController = FindObjectOfType<BlockController>();
        remainScore = FindObjectOfType<RemainScore>();
        remainTime = FindObjectOfType<RemainTime>();
    }

}
