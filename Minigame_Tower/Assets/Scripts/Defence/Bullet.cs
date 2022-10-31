using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Dictionary<int, Color> color = new Dictionary<int, Color>()
    {
     {0, Color.red},
     {1, Color.blue},
     {2, Color.yellow},
     {3, Color.green},
     {4, Color.gray}
};
    int type;
    int damage;
    Transform target;

    // Start is called before the first frame update
    void Start()
    {

    }
    void OnDisable()
    {
        ObjectPooler.ReturnToPool(gameObject);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(DefenceManager.Instance.Target().Hp - damage <= 0)
        {
            DefenceManager.Instance.frontEnemy++;
        }
        else if((target.position - transform.position).sqrMagnitude < 0.1f)
        {
            target.GetComponent<Enemy>().Hp -= damage;
            ObjectPooler.SpawnFromPool<DamageText>("DmgTxt", this.transform.position).Init(damage);
            this.gameObject.SetActive(false);
            return;
        }
        transform.Translate(5f * (target.position - transform.position) * Time.deltaTime);
    }

    public void Init(int _type, int _damage)
    {
        type = _type;
        damage = _damage;
        this.GetComponent<SpriteRenderer>().color = color[type];
        target = DefenceManager.Instance.Target().transform;
    }
}
