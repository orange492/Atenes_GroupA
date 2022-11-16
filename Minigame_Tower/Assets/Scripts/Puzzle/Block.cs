using UnityEngine;

using System;
using System.Collections;
using System.Collections.Generic;
using Random = UnityEngine.Random;
using Unity.VisualScripting;
using System.Transactions;

public class Block : MonoBehaviour
{
    public GameObject characterPrefab;
    public GameObject bombPrefab;
    int indexX;
    int indexY;
    
    public Sprite[] sprites;
    Animator pangAnim;



    BlockController blockController;



    public int IndexX
    {
        get => indexX;
        set
        {
            indexX = value;
        }
    }
    public int IndexY
    {
        get => indexY;
        set
        {
            indexY = value;
        }
    }
    public enum CharacterType
    {
        Animal,
        Bomb,
        Gargoyle,
        Imp
    }

    public CharacterType charaterType;
    private void OnEnable()
    {

    }

    private void Awake()
    {
        MakeCharacter();
        pangAnim = transform.GetChild(0).GetComponent<Animator>();

    }

    private void Start()
    {

        blockController = FindObjectOfType<BlockController>();




    }
    private void Update()
    {

        if (transform.childCount == 1 && indexY < blockController.invisibleBlockYSize)
        {

            MakeCharacter();

        }
    


    }


    public void MakeCharacter()
    {
        GameObject obj = Instantiate(characterPrefab, transform.position, transform.rotation, transform);

        Character_Base character_Base = obj.GetComponent<Character_Base>();

        if (Random.Range(0.0f, 1.0f) < 0.1)
        {
            character_Base.Init(Character_Base.CharaterType.Gargoyle, -3, sprites[sprites.Length-1]);
            charaterType = CharacterType.Gargoyle;
        }
        else
        {
            
            int animalType = Random.Range(0, sprites.Length-1);
            character_Base.Init(Character_Base.CharaterType.Animal,animalType, sprites[animalType]);
            charaterType = CharacterType.Animal;
        }
    }


    public void PangAnimationActive()
    {
        pangAnim.SetTrigger("Pang");
    }

    public void DestroyCharacter()
    {
        if (transform.childCount != 1)
        {
            Destroy(transform.GetChild(1).gameObject);
        }
       
    }

    public void DestroyImmediateCharacter()
    {
        if (transform.childCount != 1)
        {
            DestroyImmediate(transform.GetChild(1).gameObject);
        }

    }

    public void BlockCheck()
    {
        if (indexY < blockController.invisibleBlockYSize)
        {
            return;
        }
        blockController.ThreeMatchAction(indexX, indexY);
    }

    public void GargolyeBlockCheck()
    {
        if (indexY < blockController.invisibleBlockYSize)
        {
            return;
        }
        blockController.GargoyleCheck(indexX, indexY);
    }

    public IEnumerator BombCreate(float time)
    {
        yield return new WaitForSeconds(time);
        Instantiate(bombPrefab, transform);
        SetCharacterType();

    }

    public void SetCharacterType()
    {
        if (transform.childCount == 2)
        {
            charaterType = (CharacterType)transform.GetChild(1).GetComponent<Character_Base>().CharaterTypeProperty;
        }
    }

    public void SetCharacterType(CharacterType type)
    {
        charaterType = type;
    }

    public void DestroySelf()
    {
        Destroy(this.gameObject);
    }


}
