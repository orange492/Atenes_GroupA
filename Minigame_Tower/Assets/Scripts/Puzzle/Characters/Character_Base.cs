using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.Playables;

public class Character_Base : MonoBehaviour
{

    SpriteRenderer spriteRenderer;
    int animalType;
    Animator anim;
    UnityEngine.Vector3 targetDir;
    Transform characterImage;
    float moveSpeed = 20.0f;



    ParticleSystem ps;
    public int AnimalType
    {
        get => animalType;
        set
        {
            animalType = value;
        }
    }

    enum Animal
    {
        Bear,
        Cat,
        Deer,
        Dog,
        Duck
        //Mouse,
        //Panda,
        // Pig,
        //Rabbit
    }




    private void Awake()
    {
        characterImage = transform.GetChild(0);
        spriteRenderer = characterImage.GetComponent<SpriteRenderer>();



        anim = characterImage.GetComponent<Animator>();

    }

    private void Start()
    {


    }
    private void Update()
    {
      
       transform.localPosition = UnityEngine.Vector3.MoveTowards(transform.localPosition, UnityEngine.Vector3.zero, Time.deltaTime * moveSpeed);


    }




    public void AnimationActive(string direction)
    {

        anim.SetTrigger(direction);
    }

    public void Init(int type, Sprite spr)
    {
        animalType = type;
        spriteRenderer.sprite = spr;
    }

 
}