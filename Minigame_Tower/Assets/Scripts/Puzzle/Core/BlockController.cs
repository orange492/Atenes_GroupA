using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class BlockController : MonoBehaviour
{
    public GameObject blockPrefab;
    public Transform blockPostionDefault;
    public GameObject[][] blocks;
    Block[][] blocksComponents;
    TouchManager touchManager;


    public int blockXSize = 6;
    public int blockYSize = 9 * 2;
    public int invisibleBlockYSize = 9;
    int destroyCount = 0;
    bool isStart = true;
    int pangAnimationCount=0;

    

    public int PangAnimationCount
    {
        get => pangAnimationCount;
        set => pangAnimationCount = value;
    }
   public int DestroyCount {
     get=>   destroyCount;
        set
        {
            destroyCount = value;
        }
    }
    List<int>[] emptyBlocks;

  public enum GameMode
    {
        CHECKMODE,
        STOPCHECKMODE
    }
   public GameMode mode = GameMode.CHECKMODE;
    public GameMode Mode
    {
        get => mode;
        set
        {
            mode = value;
        }
    } 
 


    private void Awake()
    {
        blocks = new GameObject[blockYSize][];
        for (int i = 0; i < blockYSize; i++)
        {
            blocks[i] = new GameObject[blockXSize];
        }

        blocksComponents = new Block[blockYSize][];
        for (int i = 0; i < blockYSize; i++)
        {
            blocksComponents[i] = new Block[blockXSize];
        }

        emptyBlocks = new List<int>[6];
        for (int i = 0; i < emptyBlocks.Length; i++)
        {
            emptyBlocks[i] = new List<int>();
        }

    }
    private void Start()
    {
        BlockCreate(blockXSize, blockYSize);
        MoveBlock(blockYSize, invisibleBlockYSize);

        touchManager = FindObjectOfType<TouchManager>();

        for (int i = 0; i < blockYSize; i++)
        {
            for (int j = 0; j < blockXSize; j++)
            {
                blocksComponents[i][j] = blocks[i][j].GetComponent<Block>();
            }

        }

    
        AllBlockAction();

    }

    private void Update()
    {
        //if (isStart)
        //{
        //    AllBlockAction();
        //    isStart = false;
        //}
      

    }

    bool BlockFullcheck()
    {
        for (int i = 0; i < blockXSize; i++)
        {
            for (int j = 0; j < blockYSize; j++)
            {
                if (blocks[j][i].transform.childCount == 1)
                {
                    return false;
                }

            }
        }
        return true;
    }


    void MoveBlock(int X, int Y)
    {
        for (int i = 0; i < blockXSize; i++)
        {
            for (int j = 0; j < invisibleBlockYSize; j++)
            {
                blocks[j][i].transform.position += Vector3.up * 10;
            }
        }

    }


    void BlockCreate(int X, int Y)
    {
        for (int j = 0; j < Y; j++)
        {
            for (int i = 0; i < X; i++)
            {

                Vector2 position = blockPostionDefault.position + new Vector3(i * 1.25f, -j * 1.25f);
                blocks[j][i] = Instantiate(blockPrefab, position, transform.rotation, transform);
                blocks[j][i].name = $"block({i},{j})";
                Block block = blocks[j][i].GetComponent<Block>();
                block.IndexX = i;
                block.IndexY = j;

                // Instantiate(characterPrefab, position, transform.rotation, transform);
            }
        }
    }

    bool AllBlockCheck()
    {
        for (int i = 0; i < blockXSize; i++)
        {
            for (int j = 0; j < blockYSize; j++)
            {
                if (ThreeMatchCheck(i, j))
                    return true;

            }
        }
      //  Mode = GameMode.CHECKMODE;
        return false;
    }

    public void AllBlockAction()
    {
       
        for (int i = 0; i < blockXSize; i++)
        {
            for (int j = invisibleBlockYSize; j < blockYSize; j++)
            {
                ThreeMatchAction(i, j);

            }
        }
    }

    public bool ThreeMatchCheck(int X, int Y)
    {

        int animalType = -2;
        Character_Base character_Base = blocks[Y][X].transform.GetComponentInChildren<Character_Base>();
        if (character_Base != null)
        {
            animalType = character_Base.AnimalType;
        }

        XThreeMatchCheck(X, Y, animalType);




        YThreeMatchCheck(X, Y, animalType);


        return (XThreeMatchCheck(X, Y, animalType) ||




        YThreeMatchCheck(X, Y, animalType)



           );


    }

    public void ThreeMatchAction(int X, int Y)
    {

        //if (Mode == GameMode.STOPCHECKMODE)
        //{
        //    return;
        //}
        //Mode = GameMode.STOPCHECKMODE;
        
        Character_Base character_Base = blocks[Y][X].transform.GetComponentInChildren<Character_Base>();
        if (character_Base == null)
        {
            return;
        }
        int animalType;
        animalType = character_Base.AnimalType;
        if (animalType == -1)
        {
            return;
        }

        XThreeMatchAction(X, Y, animalType);
        YThreeMatchAction(X, Y, animalType);

        return;

    }

    bool XThreeMatchCheck(int X, int Y, int animalType)
    {
        Character_Base[] characters;
        characters = new Character_Base[2];
        bool isThreeMatch = false;
        if (X + 2 < blockXSize)
        {
            characters[0] = blocks[Y][X + 1].GetComponentInChildren<Character_Base>();
            characters[1] = blocks[Y][X + 2].GetComponentInChildren<Character_Base>();
            if (characters[0] != null && characters[1] != null)
            {
                if ((animalType == characters[0].AnimalType) && animalType == characters[1].AnimalType)
                {
                    isThreeMatch = true;


                }
                for (int i = 0; i < 2; i++)
                {
                    characters[i] = null;
                }
            }
        }
        if (X + 1 < blockXSize && X - 1 >= 0)
        {
            characters[0] = blocks[Y][X - 1].GetComponentInChildren<Character_Base>();
            characters[1] = blocks[Y][X + 1].GetComponentInChildren<Character_Base>();
            if (characters[0] != null && characters[1] != null)
            {
                if ((animalType == characters[0].AnimalType) && animalType == characters[1].AnimalType)
                {

                    if ((animalType == characters[0].AnimalType) && animalType == characters[1].AnimalType)
                    { isThreeMatch = true; }
                    for (int i = 0; i < 2; i++)
                    {

                        characters[i] = null;
                    }


                }
            }
        }
        if (X - 2 >= 0)
        {
            characters[0] = blocks[Y][X - 1].GetComponentInChildren<Character_Base>();
            characters[1] = blocks[Y][X - 2].GetComponentInChildren<Character_Base>();
            if (characters[0] != null && characters[1] != null)
            {
                if ((animalType == characters[0].AnimalType) && animalType == characters[1].AnimalType)
                {
                    isThreeMatch = true;
                }
                for (int i = 0; i < 2; i++)
                {
                    characters[i] = null;
                }
            }
        }

        return isThreeMatch;
    }

    void XThreeMatchAction(int X, int Y, int animalType)
    {
        Character_Base[] characters;
        characters = new Character_Base[4];
        int[] animalTypes;
        animalTypes = new int[4];
        for (int i = 0; i < 4; i++)
        {
            animalTypes[i] = -1;
        }


        if (X + 2 < blockXSize)
        {
            characters[3] = blocks[Y][X + 2].GetComponentInChildren<Character_Base>();
            if (characters[3] != null)
                animalTypes[3] = (int)characters[3].AnimalType;
        }
        if (X + 1 < blockXSize)
        {
            characters[2] = blocks[Y][X + 1].GetComponentInChildren<Character_Base>();
            if (characters[2] != null)
                animalTypes[2] = characters[2].AnimalType;
        }
        if (X - 2 >= 0)
        {
            characters[0] = blocks[Y][X - 2].GetComponentInChildren<Character_Base>();
            if (characters[0] != null)
                animalTypes[0] = characters[0].AnimalType;
        }
        if (X - 1 >= 0)
        {
            characters[1] = blocks[Y][X - 1].GetComponentInChildren<Character_Base>();
            if (characters[1] != null)
                animalTypes[1] = characters[1].AnimalType;
        }

        if (animalType == -1)
        {
            return;
        }
        
        if (animalTypes[2] == animalType ) // □□|■■|□
        {
            if (animalTypes[3] == animalType ) // □□|■■■|
            {
                if (animalTypes[1] == animalType ) // □|■■■■|
                {
                    if (animalTypes[0] == animalType) //  |■■■■■|
                    {
                        for (int i = -2; i <= 2; i++)
                        {
                            CharacterDestroyAndAnimation(X + i, Y);
                        }
                        blocksComponents[Y][X].StartCoroutine(blocksComponents[Y][X].BombCreate()) ;
                        //   StartCoroutine(CharacterDownX(X-2, Y, 5));
                    }
                    else //  |□■■■■|
                    {
                        for (int i = -1; i <= 2; i++)
                        {
                            CharacterDestroyAndAnimation(X + i, Y);
                        }
                        //    StartCoroutine(CharacterDownX(X - 1, Y, 4));
                    }
                }
                else // |□□■■■|
                {
                    for (int i = 0; i <= 2; i++)
                    {
                        CharacterDestroyAndAnimation(X + i, Y);
                    }
                    //     StartCoroutine(CharacterDownX(X, Y, 3));
                }
            }
            else // □□|■■□|
            {
                if (animalTypes[1] == animalType) // □|■■■□|
                {
                    if (animalTypes[0] == animalType) // |■■■■□|
                    {
                        for (int i = -2; i <= 1; i++)
                        {
                            CharacterDestroyAndAnimation(X + i, Y);
                        }
                        //   StartCoroutine(CharacterDownX(X - 2, Y, 4));
                    }
                    else // |□■■■□|
                    {
                        for (int i = -1; i <= 1; i++)
                        {
                            CharacterDestroyAndAnimation(X + i, Y);
                        }
                        //StartCoroutine(CharacterDownX(X - 1, Y, 3));
                    }
                }
                else // |□□■■□| 
                {

                }
            }
        }
        else // □□|■□□|
        {
            if (animalTypes[1] == animalType) // □|■■□□|
            {
                if (animalTypes[0] == animalType) // |■■■□□|
                {
                    for (int i = -2; i <= 0; i++)
                    {
                        CharacterDestroyAndAnimation(X + i, Y);
                    }
                    //StartCoroutine(CharacterDownX(X - 2, Y, 3));
                }
                else  // |□■■□□|
                {

                }
            }
            else // |□□■□□|
            {

            }
        }





        return;
    }


    bool YThreeMatchCheck(int X, int Y, int animalType)
    {
        Character_Base[] characters;
        characters = new Character_Base[2];
        bool isThreeMatch = false;
        if (Y + 2 < blockYSize)
        {
            characters[0] = blocks[Y + 1][X].GetComponentInChildren<Character_Base>();
            characters[1] = blocks[Y + 2][X].GetComponentInChildren<Character_Base>();
            if (characters[0] != null && characters[1] != null)
            {
                if ((animalType == characters[0].AnimalType) && animalType == characters[1].AnimalType)
                {
                    isThreeMatch = true;
                }
                for (int i = 0; i < 2; i++)
                {
                    characters[i] = null;
                }
            }
        }

        if (Y + 1 < blockYSize && Y - 1 >= invisibleBlockYSize)
        {
            characters[0] = blocks[Y - 1][X].GetComponentInChildren<Character_Base>();
            characters[1] = blocks[Y + 1][X].GetComponentInChildren<Character_Base>();
            if (characters[0] != null && characters[1] != null)
            {
                if ((animalType == characters[0].AnimalType) && animalType == characters[1].AnimalType)
                {
                    isThreeMatch = true;

                }
                for (int i = 0; i < 2; i++)
                {
                    characters[i] = null;
                }

            }
        }
        if (Y - 2 >= invisibleBlockYSize)
        {
            characters[0] = blocks[Y - 1][X].GetComponentInChildren<Character_Base>();
            characters[1] = blocks[Y - 2][X].GetComponentInChildren<Character_Base>();
            if (characters[0] != null && characters[1] != null)
            {
                if ((animalType == characters[0].AnimalType) && animalType == characters[1].AnimalType)
                {
                    isThreeMatch = true;



                }
                for (int i = 0; i < 2; i++)
                {
                    characters[i] = null;
                }
            }


        }
        return isThreeMatch;
    }
    public void YThreeMatchAction(int X, int Y, int animalType)
    {
        Character_Base[] characters;
        characters = new Character_Base[4];
        int[] animalTypes;
        animalTypes = new int[4];
        for (int i = 0; i < 4; i++)
        {
            animalTypes[i] = -1;
        }


        if (Y + 2 < blockYSize)
        {
            characters[3] = blocks[Y + 2][X].GetComponentInChildren<Character_Base>();
            if (characters[3] != null)
                animalTypes[3] = characters[3].AnimalType;
        }
        if (Y + 1 < blockYSize)
        {
            characters[2] = blocks[Y + 1][X].GetComponentInChildren<Character_Base>();
            if (characters[2] != null)
                animalTypes[2] = characters[2].AnimalType;
        }
        if (Y - 2 >= invisibleBlockYSize)
        {
            characters[0] = blocks[Y - 2][X].GetComponentInChildren<Character_Base>();
            if (characters[0] != null)
                animalTypes[0] = characters[0].AnimalType;
        }
        if (Y - 1 >= invisibleBlockYSize)
        {
            characters[1] = blocks[Y - 1][X].GetComponentInChildren<Character_Base>();
            if (characters[1] != null)
                animalTypes[1] = characters[1].AnimalType;
        }
        if (animalType == -1)
        {
            return;
        }
        if (animalTypes[2] == animalType) // □□|■■|□
        {
            if (animalTypes[3] == animalType) // □□|■■■|
            {
                if (animalTypes[1] == animalType) // □|■■■■|
                {
                    if (animalTypes[0] == animalType) //  |■■■■■|
                    {
                        for (int i = -2; i <= 2; i++)
                        {
                            CharacterDestroyAndAnimation(X, Y + i);
                        }
                        Debug.Log("222");
                        blocksComponents[Y][X].BombCreate() ;
                        

                        //downSizeIndex = 5;
                        //downX = X;
                        //downY = Y + 2;
                        //StartCoroutine(CharacterDownY(X, Y+2, 5));


                    }
                    else //  |□■■■■|
                    {
                        for (int i = -1; i <= 2; i++)
                        {
                            CharacterDestroyAndAnimation(X, Y + i);


                        }
                        //downSizeIndex = 4;

                        //downX = X;
                        //downY = Y + 2; 
                        //StartCoroutine(CharacterDownY(X, Y+2, 4));
                    }
                }
                else // |□□■■■|
                {
                    for (int i = 0; i <= 2; i++)
                    {
                        CharacterDestroyAndAnimation(X, Y + i);
                    }
                    //downSizeIndex = 3;
                    //downX = X;
                    //downY = Y+2;
                    //StartCoroutine(CharacterDownY(X, Y+2, 3));
                }
            }
            else // □□|■■□|
            {
                if (animalTypes[1] == animalType) // □|■■■□|
                {
                    if (animalTypes[0] == animalType) // |■■■■□|
                    {
                        for (int i = -2; i <= 1; i++)
                        {
                            CharacterDestroyAndAnimation(X, Y + i);
                        }
                        //downSizeIndex = 4;
                        //downX = X;
                        //downY = Y + 1;
                        //StartCoroutine(CharacterDownY(X, Y+1, 4));
                    }
                    else // |□■■■□|
                    {
                        for (int i = -1; i <= 1; i++)
                        {
                            CharacterDestroyAndAnimation(X, Y + i);
                        }
                        //downSizeIndex = 3;
                        //downX = X;
                        //downY = Y + 1; 
                        //StartCoroutine(CharacterDownY(X, Y+1, 3));
                    }
                }
                else // |□□■■□| 
                {

                }
            }
        }
        else // □□|■□□|
        {
            if (animalTypes[1] == animalType) // □|■■□□|
            {
                if (animalTypes[0] == animalType) // |■■■□□|
                {
                    for (int i = -2; i <= 0; i++)
                    {
                        CharacterDestroyAndAnimation(X, Y + i);
                    }
                    //downSizeIndex = 3;
                    //downX = X;
                    //downY = Y;
                    //StartCoroutine(CharacterDownY(X, Y, 3));
                }
                else  // |□■■□□|
                {

                }
            }
            else // |□□■□□|
            {

            }
        }


        return;
    }

    public  void BombExplosion(int X, int Y)
    {
        for (int i = X-1; i <=X+1 ; i++)
        {
            for (int j = Y-1; j <= Y+1; j++)
            {
                CharacterDestroyAndAnimation(i, j);
            }
        }
    }
    
    public void CharacterDown(int X, int lowY, int downSize)
    {
        for (int i = lowY; i >= downSize; i--)
        {
            if (blocks[i - downSize][X].transform.childCount == 2 && blocks[i][X].transform.childCount == 1)
            {
                blocks[i - downSize][X].transform.GetChild(1).transform.parent = blocks[i][X].transform;
                blocks[i][X].transform.GetChild(1).localPosition = Vector3.up * downSize * 1.25f;
                blocks[i][X].transform.GetChild(1).GetComponent<Character_Base>().SetAnimalType(-1);
            }
        }
        //downSizeIndex = 0;
        //downX = 0;
        //downY = 0;
    }

    public void CharaterDownPlay()
    {
        for (int i = 0; i < blockXSize; i++)
        {
            if (emptyBlocks[i].Count != 0)
            {
                int count = 0;
                int block;
                for (int j = 0; j < emptyBlocks[i].Count; j++)
                {
                    if (j == emptyBlocks[i].Count - 1)
                    {
                        
                    }
                    else if (emptyBlocks[i][j] - emptyBlocks[i][j + 1] == -1)
                    {
                        count++;
                        continue;
                    }
                    count++;
                    block = emptyBlocks[i][j];
                    CharacterDown(i, block, count);
                    count = 0;
                }
            }
        }
    }


    void CharacterDestroyAndAnimation(int X, int Y)
    {
        if (X < 0 || Y < 0)
        {
            return;
        }
        if (X >= blockXSize || Y >= blockYSize)
        {
            return;
        }
        blocksComponents[Y][X].PangAnimationActive();
  


    }

    public void EmptyBlockCheck()
    {
        for (int i = 0; i < blockXSize; i++)
        {
            for (int j = invisibleBlockYSize; j < blockYSize; j++)
            {
                if (blocks[j][i].transform.childCount == 1)
                {
                    emptyBlocks[i].Add(j);
                }
            }

        }
    }

    public void ResetList()
    {
        for (int i = 0; i < blockXSize; i++)
        {
            emptyBlocks[i].Clear();
        }

    }




}