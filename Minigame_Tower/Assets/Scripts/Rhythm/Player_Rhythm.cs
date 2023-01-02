using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Rhythm : MonoBehaviour
{
    [SerializeField]
    Transform[] pos;
    public Transform player { get; set; }
    public int loca { get; set; }
    GameObject hitEffect;

    void Awake()
    {
        player = GameObject.FindWithTag("Player").transform;
        hitEffect = player.root.GetChild(2).gameObject;
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
        GameObject spawnedHit = Instantiate(hitEffect);
        spawnedHit.transform.LookAt(Camera.main.transform);
        spawnedHit.transform.position = player.position + spawnedHit.transform.position + new Vector3(0.3f,0,0);
        spawnedHit.gameObject.SetActive(true);
        Sound_Rhythum.Inst.PlayNoteSound();
    }
}
