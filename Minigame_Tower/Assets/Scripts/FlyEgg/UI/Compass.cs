using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Compass : MonoBehaviour
{
    Nest nest;

    Transform pin;
    private void Start()
    {
        nest = FindObjectOfType<Nest>();
        pin = transform.GetChild(1).transform;
        if (EggGameManager.Inst != null)
        {
            EggGameManager.Inst.onModeChange += ModeChange;
        }
        if (EggGameManager.Inst.mode == EggGameManager.Mode.Intro)
        {
            transform.gameObject.SetActive(false);
        }
    }

    private void OnDestroy()
    {
        if (EggGameManager.Inst != null)
        {
            EggGameManager.Inst.onModeChange -= ModeChange;
        }
    }
    private void Update()
    {
        Vector2 dir = nest.transform.position - (Camera.main.transform.GetChild(0).transform.position );
        pin.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg);
    }
    private void ModeChange(EggGameManager.Mode obj)
    {
        transform.gameObject.SetActive(obj != EggGameManager.Mode.Intro);
    }
}
