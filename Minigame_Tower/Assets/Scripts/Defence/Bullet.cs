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
        if(!target.gameObject.activeSelf)
        {
            this.gameObject.SetActive(false);
        }
        else if((target.position - transform.position).sqrMagnitude < 0.1f)
        {
            target.GetComponent<Enemy>().Damage(-damage);
            ObjectPooler.SpawnFromPool<DamageText>("DmgTxt", target.transform.position).Init(target.transform, damage);
            this.gameObject.SetActive(false);
            if(type==3)
            {
                target.GetComponent<Enemy>().SetPoison(damage/5);
            }
            else if(type == 1)
            {
                target.GetComponent<Enemy>().SetSlow(1);
            }
            return;
        }
        transform.Translate(5f * (target.position - transform.position) * Time.deltaTime);
    }
     
    public void Init(Transform _tr, int _type, int _damage)
    {
        type = _type;
        damage = _damage;
        this.GetComponent<SpriteRenderer>().color = color[type];
        target = _tr;
    }
}
