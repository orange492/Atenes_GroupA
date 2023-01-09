using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    private void Start()
    {
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
    private void ModeChange(EggGameManager.Mode obj)
    {
        if (obj == EggGameManager.Mode.Intro)
        {
            transform.gameObject.SetActive(false);
        }
        else
        {
            transform.gameObject.SetActive(true);
        }
    }
}
