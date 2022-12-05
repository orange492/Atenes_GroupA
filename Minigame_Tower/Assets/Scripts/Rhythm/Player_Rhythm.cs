using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Rhythm : MonoBehaviour
{
    [SerializeField]
    Transform[] pos;
    public Transform player { get; set; }
    public int loca { get; set; }

    void Awake()
    {
        player = GameObject.FindWithTag("Player").transform;
        player.position = new Vector2(player.position.x, pos[1].position.y);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ClickKey(int value)
    {
        loca = value;
        player.position = new Vector2(player.position.x, pos[value].position.y);
    }
}
