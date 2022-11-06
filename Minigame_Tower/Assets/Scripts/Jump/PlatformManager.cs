using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformManager : MonoBehaviour
{
    public GameObject prefab_PlatformGroup;

    Vector2 pos_WillCreatePlatformGroup;
    public float heightBetweenPlatform = 3.0f;
    int index = 0;

    bool stop = false; // 새로 추가된 부분
    private void Awake()
    {
        pos_WillCreatePlatformGroup = prefab_PlatformGroup.transform.position;
    }
    private void Start()
    {
        AddNewPlatform();
    }
    public void AddNewPlatform()
    {
        if (stop == false) // 새로 추가된 부분
        {
            GameObject added_platformGroup = Instantiate(prefab_PlatformGroup);
            added_platformGroup.transform.position = pos_WillCreatePlatformGroup;
            added_platformGroup.name = "new_platform" + index;
            added_platformGroup.GetComponent<PlatformGroup>().SetName("new_platform" + index);
            index = index + 1;

            pos_WillCreatePlatformGroup = new Vector2(pos_WillCreatePlatformGroup.x, pos_WillCreatePlatformGroup.y + heightBetweenPlatform);
        }
    }

    public void Stop() // 새로 추가된 부분
    {
        stop = true;
    }
}