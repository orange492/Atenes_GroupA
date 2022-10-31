using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    Transform[] tr;
    [SerializeField]
    GameObject enemyPref;
    public List<Enemy> enemy { get; set; } = new List<Enemy>();

    int number = 0;
    float timer = 0;
    float waitTime = 0;
    int hp;
    int gold;



    // Start is called before the first frame update
    void Start()
    {

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
        if (DefenceManager.Instance.frontEnemy >= enemy.Count && number == 0)
        {
            DefenceManager.Instance.Wave++;
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
        enemy.Clear();
    }

    void CrtEnemy()
    {
        Enemy newEnemy = ObjectPooler.SpawnFromPool<Enemy>("Enemy", this.transform.position).Init(tr, hp, gold); //수정예정!
        enemy.Add(newEnemy);
    }
}
