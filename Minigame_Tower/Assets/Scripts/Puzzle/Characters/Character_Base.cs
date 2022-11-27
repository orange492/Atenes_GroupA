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

    public int Temp => temp;
  



    bool isOnGargoyle = false;

    public bool IsOnGargoyle
    {
        get => isOnGargoyle;
        set
        {
            OnGargoyle();
            isOnGargoyle = value;
        }
    }


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
                block.GargolyeBlockCheck();
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
        Block block = GetComponentInParent<Block>();
        block.GargolyeBlockCheck();

    }
    protected virtual void Update()
    {
        if ((transform.localPosition - UnityEngine.Vector3.zero).magnitude > 0.025f)
        {

            //transform.localPosition = UnityEngine.Vector3.MoveTowards(transform.localPosition, UnityEngine.Vector3.zero, Time.deltaTime * moveSpeed);
            transform.localPosition += ChangeSpeed() * Time.deltaTime * moveSpeed;
        }
        else
        {
            AnimalType = temp;

        }

    }

    public void IsPang()
    {
        isPang = true;

    }

    public UnityEngine.Vector3 ChangeSpeed()
    {
        UnityEngine.Vector3 movedir= UnityEngine.Vector3.zero- transform.localPosition;
        return movedir;
    }



    public void AnimationActive(string direction)
    {
        if (isPang == false)
            anim.SetTrigger(direction);
    }

    public void Init(CharaterType character, int type, Sprite spr)
    {
        animalType = type;
        spriteRenderer.sprite = spr;
        temp = type;
        charaterType = character;

    }

    public void SetAnimalType(int type)
    {
        AnimalType = type;
    }

    void OnGargoyle()
    {
        spriteRenderer.color = Color.black;
    }


}