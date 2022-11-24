using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    [SerializeField]
    Sprite[] allSpr;
    [SerializeField]
    GameObject pBullet;
    SpriteRenderer spr;
    GameObject[] stars = new GameObject[5];
    public int type { get; set; }
    public int star { get; set; }
    float timer;
    int[] index = new int[2];

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
        if (!DefenceManager.Inst.isGame)
        {
            return;
        }
        timer += Time.deltaTime;
        if(timer > 3 - star * 0.5)
        {
            Shoot();
            timer = 0;
        }
        
    }

    public void SetIndex(int y, int x)
    {
        index[0] = y;
        index[1] = x;
    }

    public void Init(int _type,int _star)
    {
        this.gameObject.SetActive(true);
        type = _type;
        star = _star;
        spr.sprite = allSpr[type];
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
        type = Random.Range(0, 5);
        spr.sprite = allSpr[type];
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
        if (type == 2)
        {
            int damage = DefenceManager.Inst.level[type] * 10;
            DefenceManager.Inst.Gold += damage;
            ObjectPooler.SpawnFromPool<DamageText>("DmgTxt", this.transform.position).Init(this.transform, damage, 1);
        }
        if(type==4)
        {
            DefenceManager.Inst.Shoot(this.transform, type, DefenceManager.Inst.mapMgr.CheckGir(index));
        }
        else
        {
            DefenceManager.Inst.Shoot(this.transform, type);
        }
    }
}
