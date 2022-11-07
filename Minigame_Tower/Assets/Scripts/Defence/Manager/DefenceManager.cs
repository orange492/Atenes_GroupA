using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DefenceManager : Singleton<DefenceManager>
{
    [SerializeField]
    GameObject oLife;
    [SerializeField]
    TextMeshProUGUI[] tLevel;
    [SerializeField]
    TextMeshProUGUI tWave;
    [SerializeField]
    TextMeshProUGUI tPrice;
    [SerializeField]
    TextMeshProUGUI tGold;

    MapManager mapMgr;
    public EnemySpawner enemySpnr { get; set; }
    public int frontEnemy { get; set; }
    int life;
    int[] level = new int[5];

    int wave;
    public int Wave
    {
        get
        {
            return wave;
        }
        set
        {
            wave = value;
            string str = "Wave\n " + wave.ToString();
            tWave.text = str.Replace("\\n", "\n");
            enemySpnr.StartWave(wave);
            frontEnemy = 0;
        }
    }
    int price;
    public int Price
    {
        get
        {
            return price;
        }
        set
        {
            price = value;
            string str = "생성\nG " + price.ToString();
            tPrice.text = str.Replace("\\n", "\n");
        }
    }
    int gold;
    public int Gold
    {
        get
        {
            return gold;
        }
        set
        {
            gold = value;
            string str = "Gold\n " + gold.ToString();
            tGold.text = str.Replace("\\n", "\n");
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        mapMgr = FindObjectOfType<MapManager>();
        enemySpnr = FindObjectOfType<EnemySpawner>();
        mapMgr.InitMap();
        Gold = 600;
        for (int i = 0; i < level.Length; i++)
        {
            SetLevel(i);
        }
        Wave = 1;
        Price = 10;
        frontEnemy = 0;
        life = 3;
        SetLife(0);
    }

    // Update is called once per frame
    void Update()
    {

    }


    public void CrtUnit()
    {
        if (gold < price)
        {
            return;
        }
        Gold -= price;
        mapMgr.CrtUnit();
        Price += 10;
    }

    public void SetLevel(int index)
    {
        int price = 100;
        int temp = 0;
        for (int i = 0; i < level[index]; i++)
        {
            price += temp;
            temp += 100;
        }
        if (gold < price)
        {
            return;
        }
        Gold -= price;
        level[index]++;
        string str = "LV " + level[index] + "\nG " + (price + temp);
        tLevel[index].text = str.Replace("\\n", "\n");
    }

    public void SetUnitCnt(int value)
    {
        mapMgr.unitCnt += value;
    }

    public void SetLife(int value)
    {
        if (life + value >= 0 && life + value < 4)
        {
            life += value;
        }
        for (int i = 0; i < oLife.transform.childCount; i++)
        {
            if (!oLife.transform.GetChild(i).gameObject.activeSelf && life > i)
            {
                oLife.transform.GetChild(i).gameObject.SetActive(true);
            }
            else if (oLife.transform.GetChild(i).gameObject.activeSelf && life <= i)
            {
                oLife.transform.GetChild(i).gameObject.SetActive(false);
            }
        }
    }

    public void Shoot(Transform tr, int type)
    {
        if (enemySpnr.enemy.Count <= frontEnemy)
        {
            return;
        }
        int damage = level[type] * 10;
        enemySpnr.enemy[frontEnemy].Hp -= damage;
        ObjectPooler.SpawnFromPool<Bullet>("Bullet", tr.position).Init(type, damage);
        if (enemySpnr.enemy[frontEnemy].Hp <= 0)
        {
            frontEnemy++;
        }
    }

    public Enemy Target()
    {
        return enemySpnr.enemy[frontEnemy];
    }
}
