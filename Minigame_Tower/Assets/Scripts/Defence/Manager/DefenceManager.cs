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
    public int[] level { get; set; } = new int[5];

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

    public void Shoot(Transform tr, int type, int gir=0)
    {
        int repeat = 1;
        int[,] index = new int[3,2];
        if (type==0)
        {
            repeat = 3;
        }
        Dictionary<int, Enemy>[] tempEnemy = new Dictionary<int, Enemy>[3];
        for (int i = 0; i < tempEnemy.Length; i++)
        {
            tempEnemy[i] = new Dictionary<int, Enemy>(enemySpnr.enemy[i]);
        }

        for (int j = 0; j < repeat; j++)
        {
            index[j, 0] = 2;
            if (tempEnemy[2].Count == 0)
            {
                if (tempEnemy[1].Count == 0)
                {
                    if (tempEnemy[0].Count == 0)
                    {
                        index[j, 0] = -1;
                        continue;
                    }
                    index[j, 0]--;
                }
                index[j, 0]--;
            }
            float? minVal = null;
            foreach (KeyValuePair<int, Enemy> items in tempEnemy[index[j, 0]])
            {
                Enemy thisEnemy = items.Value;
                if (!minVal.HasValue)
                {
                    minVal = thisEnemy.RemainTr();
                    index[j,1] = thisEnemy.index;
                    continue;
                }
                float thisNum = thisEnemy.RemainTr();
                if (thisNum < minVal)
                {
                    minVal = thisNum;
                    index[j,1] = thisEnemy.index;
                }
            }
            tempEnemy[index[j, 0]].Remove(index[j,1]);
        }

        for (int j = 0; j < repeat; j++)
        {
            if (index[j, 0] == -1)
            {
                continue;
            }
            int damage = level[type] * 10;
            damage += damage / 4 * gir;
            enemySpnr.enemy[index[j, 0]][index[j, 1]].Hp -= damage;
            ObjectPooler.SpawnFromPool<Bullet>("Bullet", tr.position).Init(enemySpnr.enemy[index[j, 0]][index[j, 1]].transform, type, damage);
            if (enemySpnr.enemy[index[j, 0]][index[j, 1]].Hp <= 0)
            {
                enemySpnr.DelDic(index[j, 0], index[j, 1]);
            }
        }
    }
}
