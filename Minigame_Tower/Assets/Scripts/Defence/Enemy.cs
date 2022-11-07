using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    TextMeshPro tHp;
    [SerializeField]
    Sprite[] spr;
    [SerializeField]
    Material[] mat;
    SpriteRenderer spriteRenderer;
    Transform[] tr;
    public int index { get; set; }
    int dir = 0;
    int hp;
    int showHp;
    int gold;

    float slow = 0;
    float poisonTime = 0;
    int[] poison = new int[2] { 0,0};

    public int Hp
    {
        get
        {
            return hp;
        }
        set
        {
            if (value > 0)
            {
                hp = value;
            }
            else
            {
                hp = 0;
                
            }
        }
    }


    // Start is called before the first frame update
    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    void OnDisable()
    {
        ObjectPooler.ReturnToPool(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
       
        if (dir == 0 && Mathf.Abs(tr[dir].position.y - this.transform.position.y) < 0.01f)
        {
            DefenceManager.Instance.enemySpnr.DelDic(dir, index);
            dir++;
            DefenceManager.Instance.enemySpnr.AddDic(dir, this);
        }
        else if (dir == 1 && Mathf.Abs(tr[dir].position.x - this.transform.position.x) < 0.01f)
        {
            
            DefenceManager.Instance.enemySpnr.DelDic(dir, index);
            dir++;
            DefenceManager.Instance.enemySpnr.AddDic(dir, this);
        }
        else if (dir == 2 && Mathf.Abs(tr[dir].position.y - this.transform.position.y) < 0.01f)
        {
            DefenceManager.Instance.enemySpnr.DelDic(dir, index);
            DestroyEnemy(false);
        }

        if(poisonTime>0)
        {
            poisonTime -= Time.deltaTime;
        }
        else if(poison[0]>0)
        {
            poisonTime = 0.5f;
            Damage(-poison[1]);
            poison[0]--;
            int temp = DefenceManager.Instance.enemySpnr.FindRail(index);
            if(temp != -1)
            {
                DefenceManager.Instance.enemySpnr.enemy[temp][index].Hp -= poison[1];
            }
            ObjectPooler.SpawnFromPool<DamageText>("DmgTxt", this.transform.position).Init(this.transform, poison[1], 2);
            if (poison[0] == 0)
            {
                spriteRenderer.sprite = spr[0];
            }
        }

        if(slow>0)
        {
            slow -= Time.deltaTime;
            this.transform.Translate(Time.deltaTime * (tr[dir].position - this.transform.position).normalized/2);
        }
        else if(slow == -10)
        {
            this.transform.Translate(Time.deltaTime * (tr[dir].position - this.transform.position).normalized);
        }
        else
        {
            slow = -10;
            spriteRenderer.material = mat[0];
        }
    }

    public Enemy Init(int _index, Transform[] _tr, int _hp, int _gold)
    {
        index = _index;
        dir = 0;
        tr = _tr;
        hp = _hp;
        showHp = _hp;
        gold = _gold;
        Damage(0);
        return this;
    }

    public void Damage(int num)
    {
        showHp += num;
        if(showHp <= 0)
        {
            DestroyEnemy(true);
        }
        tHp.text = showHp.ToString();
    }

    void DestroyEnemy(bool dead)
    {
        if (dead)
        {
            DefenceManager.Instance.Gold += gold;
        }
        else
        {
            DefenceManager.Instance.SetLife(-1);
        }
        DefenceManager.Instance.enemySpnr.count--;
        if (DefenceManager.Instance.enemySpnr.count == 0)
        {
            DefenceManager.Instance.Wave++;
        }
        this.gameObject.SetActive(false);
    }
    public float RemainTr()
    {
        if(dir==1)
        {
            return Mathf.Abs(tr[dir].position.x - this.transform.position.x);
        }
        else
        {
            return Mathf.Abs(tr[dir].position.y - this.transform.position.y);
        }
    }
    public void SetPoison(int damage)
    {
        poison[0] = 5;
        poison[1] = damage;
        spriteRenderer.sprite = spr[1];
    }
    public void SetSlow(float _slow)
    {
        slow = _slow;
        spriteRenderer.material = mat[1];
    }
}
