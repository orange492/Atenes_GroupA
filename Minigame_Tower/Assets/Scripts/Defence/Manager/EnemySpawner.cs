using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    Transform[] tr;
    [SerializeField]
    GameObject enemyPref;
    public Dictionary<int ,Enemy>[] enemy { get; set; } = new Dictionary<int, Enemy>[3];
    int index = 0;
    public int number = 0;
    public int count = 0;
    float timer = 0;
    float waitTime = 0;
    int hp;
    int gold;



    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < enemy.Length; i++)
        {
            enemy[i] = new Dictionary<int, Enemy>();
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (number > 0)
        {
            timer += Time.deltaTime;
            if (timer > waitTime)
            {
                CrtEnemy();
                timer = 0;
                number--;
            }
        }
    }

    public void StartWave(int num)
    {
        timer = 0;
        if (num == 1)
        {
            waitTime = 1.5f;
            number = 5;
            hp = 100;
            gold = 100;
        }
        else
        {
            number += 5;
            hp += 100;
            gold += 100;
        }
        count = number;
        for (int i = 0; i < enemy.Length; i++)
        {
            enemy[i].Clear();
        }
        index = 0;
    }

    void CrtEnemy()
    {
        Enemy newEnemy = ObjectPooler.SpawnFromPool<Enemy>("Enemy", this.transform.position).Init(index, tr, hp, gold);
        enemy[0].Add(index, newEnemy);
        index++;
    }
    public void AddDic(int index, Enemy scr)
    {
        enemy[index].Add(scr.index, scr);
    }
    public void DelDic(int index, int num)
    {
        enemy[index].Remove(num);
    }
    public int FindRail(int index)
    {
        int rail = -1;
        for (int i = 0; i < enemy.Length; i++)
        {
            if (enemy[i].ContainsKey(index))
                rail = i;
        }
        return rail;
    }
}
