using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformManager : MonoBehaviour
{
    public GameObject prefab_PlatformGroup;

    Vector2 pos_WillCreatePlatformGroup;
    public float heightBetweenPlatform = 3.0f;
    int index = 0; // 추가된 내용
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
        GameObject added_platformGroup = Instantiate(prefab_PlatformGroup);
        added_platformGroup.transform.position = pos_WillCreatePlatformGroup;
        // 추가된 내용!
        added_platformGroup.name = "new_platform" + index;
        added_platformGroup.GetComponent<PlatformGroup>().SetName("new_platform" + index);
        index = index + 1;
        pos_WillCreatePlatformGroup = new Vector2(pos_WillCreatePlatformGroup.x, pos_WillCreatePlatformGroup.y + heightBetweenPlatform);
    }
}