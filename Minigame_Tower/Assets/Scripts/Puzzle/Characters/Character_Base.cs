using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.Playables;

public class Character_Base : MonoBehaviour
{

    SpriteRenderer spriteRenderer;

    protected Animator anim;
    UnityEngine.Vector3 targetDir;
    protected Transform characterImage;
    protected float moveSpeed = 10.0f;

    protected BlockController blockController;
    protected int animalType;
    protected int temp;

    bool isPang = false;


    public int AnimalType
    {
        get => animalType;
        set
        {
            if (animalType == -1 && animalType != value)
            {
                animalType = value;
                Block block = GetComponentInParent<Block>();
                block.BlockCheck();
            }
            animalType = value;

        }
    }


    public enum CharaterType
    {
        Animal,
        Bomb,
        Gargoyle,
        Imp
    }

    protected CharaterType charaterType;
    public CharaterType CharaterTypeProperty
    {
        get => charaterType;
        set => charaterType = value;
    }











    protected virtual void Awake()
    {
        characterImage = transform.GetChild(0);
        spriteRenderer = characterImage.GetComponent<SpriteRenderer>();
        anim = characterImage.GetComponent<Animator>();
        temp = animalType;
        charaterType = CharaterType.Animal;

    }

    protected virtual void Start()
    {
        blockController = FindObjectOfType<BlockController>();


    }
    protected virtual void Update()
    {
        if ((transform.localPosition - UnityEngine.Vector3.zero).magnitude > 0.025f)
        {
            transform.localPosition = UnityEngine.Vector3.MoveTowards(transform.localPosition, UnityEngine.Vector3.zero, Time.deltaTime * moveSpeed);
        }
        else
        {
            AnimalType = temp;

        }
    }

    public void IsPang(){
        isPang = true;

    }




    public void AnimationActive(string direction)
    {
        if(isPang==false)
        anim.SetTrigger(direction);
    }

    public void Init(int type, Sprite spr)
    {
        animalType = type;
        spriteRenderer.sprite = spr;
        temp = type;
        charaterType = CharaterType.Animal;
    }

    public void SetAnimalType(int type)
    {
        AnimalType = type;
    }


}