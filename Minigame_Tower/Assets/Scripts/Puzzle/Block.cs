using UnityEngine;

using System;
using System.Collections;
using System.Collections.Generic;
using Random = UnityEngine.Random;
using Unity.VisualScripting;

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
    int animalType;
    public int AnimalType
    {
        get => animalType;
        set
        {
            animalType = value;
        }
    }

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
        int animalType = Random.Range(0, sprites.Length);

        character_Base.Init(animalType, sprites[animalType]);
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

    public void BlockCheck()
    {
        blockController.ThreeMatchAction(indexX, indexY);
    }

    public IEnumerator BombCreate()
    {
        yield return new WaitForSeconds(0.2f);
        Instantiate(bombPrefab, transform);

    }



}
