using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block_Rhythm : MonoBehaviour
{
    Editor_Rhythm editor;
    int index;
    [SerializeField]
    GameObject childObjectPrefab; // 생성할 자식 오브젝트의 프리팹
    int numChildren = 4; // 생성할 자식 오브젝트의 개수

    public void Init(Editor_Rhythm _editor, int _index)
    {
        editor = _editor;
        index = _index;
        for (int i = 0; i < transform.childCount; i++)
        {
            CreateNoteBox(transform.GetChild(i).gameObject, i);
        }

    }

    void CreateNoteBox(GameObject box, int up)
    {
        float width = box.transform.GetComponent<BoxCollider2D>().size.x; // 부모 오브젝트의 가로 길이
        float childWidth = width / numChildren; // 자식 오브젝트의 가로 길이

        for (int i = 0; i < numChildren; i++)
        {
            // 새로운 자식 오브젝트 생성
            GameObject childObject = Instantiate(childObjectPrefab, box.transform);

            // 자식 오브젝트의 콜라이더 가로 길이 설정
            //BoxCollider2D collider = childObject.GetComponent<BoxCollider2D>();
            //collider.size = new Vector2(childWidth, collider.size.y);

            // 자식 오브젝트의 위치 조정
            Vector3 position = childObject.transform.position;
            position.x += (i * childWidth * box.transform.localScale.x) - box.transform.localScale.x / 2;
            childObject.transform.position = position;

            NoteBlock_rhythm noteBlock = childObject.GetComponent<NoteBlock_rhythm>();
            noteBlock.Init(up, index * 4 + i);
            editor.noteBlocks[up].Add(noteBlock);
            if(editor.NoteDic[up].ContainsKey(index * 4 + i))
            {
                noteBlock.Click();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Line"))
        {
            Sound_Rhythum.Inst.PlayNoteSound();
        }
    }
}
