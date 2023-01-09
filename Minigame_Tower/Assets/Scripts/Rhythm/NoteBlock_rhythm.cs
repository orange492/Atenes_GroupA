using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteBlock_rhythm : MonoBehaviour
{
    [SerializeField]
    GameObject pNote;

    public int[] index { get; set; }

    private void Awake()
    {
        index = new int[2];
    }
    public void Init(int _loca, int _index)
    {
        index[0] = _loca;
        index[1] = _index;
    }

    public bool Click()
    {
        if(transform.childCount == 0)
        {
            Instantiate(pNote, transform);
            return true;
        }
        else if (transform.GetChild(0).gameObject.activeSelf)
        {
            transform.GetChild(0).gameObject.SetActive(false);
            return false;
        }
        else
        {
            transform.GetChild(0).gameObject.SetActive(true);
            return true;
        }
    }
    public void Cancle()
    {

    }

    public void MakeNote()
    {

    }

    public void MakeLongNote(int length)
    {

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Line") && transform.childCount == 1)
        {
            if(transform.GetChild(0).gameObject.activeSelf)
            {
                //Manager_Rhythm.Inst.CreateNote(index[0]);
                Debug.Log(Sound_Rhythum.Inst.MusicTime());
            }
        }
    }

}
