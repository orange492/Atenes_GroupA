using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleGameManager : SingletonPuzzle<PuzzleGameManager>
{

    BlockController blockController;
    RemainTime remainTime;
    RemainScore remainScore;
    GameOverPanel gameOverPanel;

    public BlockController BlockController=>blockController;
    public RemainTime RemainTime => remainTime;

    public RemainScore RemainScore => remainScore;

    public GameOverPanel GameOverPanel => gameOverPanel;




    protected override void Initialize()
    {
        base.Initialize();
        blockController = FindObjectOfType<BlockController>();
        remainScore = FindObjectOfType<RemainScore>();
        remainTime = FindObjectOfType<RemainTime>();
        gameOverPanel = FindObjectOfType<GameOverPanel>();
        Debug.Log(gameOverPanel);
    }

}
