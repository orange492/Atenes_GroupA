using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note : MonoBehaviour
{
    float MissDistance = -8;
    bool miss = false;
    NoteSpawner ns;
    int id;
    int loca;
    float scrollingSpeed = 10f;

    public Note Init(NoteSpawner noteSpawner, int index, int location)
    {
        ns = noteSpawner;
        id = index;
        loca = location;
        return this;
    }
    void Start()
    {
        
    }

    void OnDisable()
    {
        ObjectPooler.ReturnToPool(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
       transform.Translate(-scrollingSpeed * Time.deltaTime * transform.right);
        if(transform.position.x < MissDistance)
        {
            if(miss)
            {
                Remove();
                return;
            }
            miss = true;
            MissDistance -= 2;
            Manager_Rhythm.Inst.MissNote(loca);
        }
    }

    public void Remove()
    {
        ns.RemoveNote(loca, id);
        gameObject.SetActive(false);
    }
}
