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

    public void Init(NoteSpawner noteSpawner, int index, int location)
    {
        ns = noteSpawner;
        id = index;
        loca = location;
    }
    void Start()
    {
        
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
            Manager_Rhythm.Inst.SetJudgeTxt(loca, 0);
            miss = true;
            MissDistance -= 2;
        }
    }

    public void Remove()
    {
        ns.RemoveNote(loca, id);
        gameObject.SetActive(false);
    }
}
