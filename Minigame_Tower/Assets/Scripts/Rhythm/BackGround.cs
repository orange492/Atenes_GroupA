using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGround : MonoBehaviour
{
    Transform[] bgSlots;
    float Background_Width;
    public float scrollingSpeed = 2.5f;

    void Awake()
    {
        Background_Width = transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite.bounds.size.x * transform.GetChild(0).gameObject.transform.localScale.x;
        bgSlots = new Transform[transform.childCount];
        for (int i = 0; i < transform.childCount; i++) 
        {
            bgSlots[i] = transform.GetChild(i);
            bgSlots[i].transform.position = new Vector2(0 + i * Background_Width, 0);
        }
    }

    private void Update()
    {
        for (int i = 0; i < bgSlots.Length; i++)
        {
            bgSlots[i].Translate(-scrollingSpeed * Time.deltaTime * transform.right);
            if (bgSlots[i].position.x < -Background_Width)
            {           
                MoveRightEnd(i);
            }
        }
    }

    protected virtual void MoveRightEnd(int index)
    {
        bgSlots[index].Translate(Background_Width * bgSlots.Length * transform.right);
    }
}
