using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteSpawner : MonoBehaviour
{
    [SerializeField]
    GameObject pNote;
    [SerializeField]
    Transform[] tSpawnPoint;
    int index = 0;

    public Dictionary<int, Note>[] noteDic { get; set; } = new Dictionary<int, Note>[2];

    // Start is called before the first frame update
    void Awake()
    {
        for (int i = 0; i < noteDic.Length; i++)
        {
            noteDic[i] = new Dictionary<int, Note>();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void CreateNote(int value)
    {
        Note note = Instantiate(pNote, tSpawnPoint[value]).GetComponent<Note>();
        note.Init(this, index, value);
        noteDic[value].Add(index, note);
        index++;
    }
    public void RemoveNote(int loca, int index)
    {
        noteDic[loca].Remove(index);
    }
}
