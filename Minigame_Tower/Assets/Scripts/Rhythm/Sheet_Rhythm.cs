using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SaveData
{
    public int bpm;
    public float startTime;

    public List<int> NoteList1;
    public List<int> NoteList2;
    //public Dictionary<int, int> NoteDic1;
    //public Dictionary<int, int> NoteDic2;
}
