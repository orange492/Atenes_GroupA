using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformGroup : MonoBehaviour
{
    Platform[] platforms;
    bool missionComplete = false;
    private void Awake()
    {
        platforms = GetComponentsInChildren<Platform>();
    }

    private void Start()
    {
        Activate();
    }

    public void Activate()
    {
        float randomPlatformMovingTime = GetRandTime();
        bool rotate = false;
        float rotate_percent = UnityEngine.Random.Range(0, 1.0f);
        if (rotate_percent <= 0.3f)
        {
            rotate = true;
        }

        foreach (Platform platform in platforms)
        {
            platform.Activate(randomPlatformMovingTime);
            if (rotate)
            {
                platform.Rotation(randomPlatformMovingTime);
            }
        }
    }

    public float GetRandTime()
    {
        float minTime = 0.7f;
        float maxTime = 1.3f;
        return UnityEngine.Random.Range(minTime, maxTime);
    }

    public void Update()
    {
        if (missionComplete == false && CheckPlatformsDeactivate())
        {
            missionComplete = true;
            FindObjectOfType<PlatformManager>().AddNewPlatform();
        }
    }

    public bool CheckPlatformsDeactivate()
    {
        bool deactivate = true;
        foreach (Platform platform in platforms)
        {
            if (platform.arrived == false)
            {
                deactivate = false;
                break;
            }
        }
        return deactivate;
    }
    public void SetName(string name) // 추가된 내용
    {
        foreach (Platform platform in platforms)
        {
            platform.gameObject.name = name;
        }
    }
}