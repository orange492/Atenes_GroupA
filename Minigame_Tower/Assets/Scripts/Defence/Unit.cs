using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    [SerializeField]
    GameObject pBullet;
    SpriteRenderer spr;
    GameObject[] stars = new GameObject[5];
    public int type { get; set; }
    public int star { get; set; }
    float timer;

    // Start is called before the first frame update
    void Awake()
    {
        timer = 0;
        spr = this.GetComponent<SpriteRenderer>();
        Transform starTr = this.transform.GetChild(0);
        for (int i = 0; i < 5; i++)
        {
            stars[i] = starTr.GetChild(i).gameObject;
            if (stars[i].activeSelf)
                stars[i].SetActive(false);
        }
        this.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        timer += Time.deltaTime;
        if(timer > 3 - star * 0.5)
        {
            Shoot();
            timer = 0;
        }
        
    }

    public void Init(int _type, Sprite spr,int _star)
    {
        this.gameObject.SetActive(true);
        type = _type;
        star = _star;
        this.GetComponent<SpriteRenderer>().sprite = spr;
        PrintUnit();
    }

    void PrintUnit()
    {
        for (int i = 0; i < stars.Length; i++)
        {
            if (!stars[i].activeSelf && star > i)
            {
                stars[i].SetActive(true);
            }
            else if (stars[i].activeSelf && star <= i)
            {
                stars[i].SetActive(false);
            }
        }
    }

    public void ResetPos()
    {
        transform.localPosition = new Vector3(0, 0, 0);
        this.gameObject.SetActive(false);
    }

    public void Merge()
    {
        star++;
        PrintUnit();
    }

    public void SetOrder(int value)
    {
        this.GetComponent<SpriteRenderer>().sortingOrder += value;
        for (int i = 0; i < stars.Length; i++)
        {
            stars[i].GetComponent<SpriteRenderer>().sortingOrder += value;
        }
    }

    void Shoot()
    {
        DefenceManager.Instance.Shoot(this.transform, type);
    }

}
