using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    TextMeshPro tHp;

    Transform[] tr;
    public int num { get; set; }
    int dir = 0;
    int hp;
    int gold;


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
                DestroyEnemy(true);
            }
            Damage();
        }
    }


    // Start is called before the first frame update
    void Awake()
    {

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
            dir++;
        }
        else if (dir == 1 && Mathf.Abs(tr[dir].position.x - this.transform.position.x) < 0.01f)
        {
            dir++;
        }
        else if (dir == 2 && Mathf.Abs(tr[dir].position.y - this.transform.position.y) < 0.01f)
        {
            DestroyEnemy(false);
        }
        this.transform.Translate(Time.deltaTime * (tr[dir].position - this.transform.position).normalized);
    }

    public Enemy Init(Transform[] _tr, int _hp, int _gold)
    {
        dir = 0;
        tr = _tr;
        hp = _hp;
        gold = _gold;
        Damage();
        return this;
    }

    public void Damage()
    {
        tHp.text = hp.ToString();
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
        hp = 0;
        this.gameObject.SetActive(false);
    }

}
