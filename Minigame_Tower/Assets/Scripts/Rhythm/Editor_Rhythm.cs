using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class Editor_Rhythm : MonoBehaviour
{
    [SerializeField]
    GameObject oEditor;
    [SerializeField]
    Transform tMap;
    [SerializeField]
    GameObject pBlock;
    [SerializeField]
    TextMeshProUGUI bpmTxt;
    [SerializeField]
    TextMeshProUGUI startTimeTxt;

    Button[] musicButton;
    //GameObject oLine;
    bool isPlay;
    float scrollingSpeed;

    private void Awake()
    {
        scrollingSpeed = Manager_Rhythm.SPEED;
        isPlay = false;
        musicButton = new Button[2];
        musicButton[0] = transform.Find("PlayBtn").GetComponent<Button>();
        musicButton[0].onClick.AddListener(PlayButton);
        musicButton[1] = transform.Find("ResetBtn").GetComponent<Button>();
        musicButton[1].onClick.AddListener(ResetButton);
    }

    private void OnEnable()
    {
        bpmTxt.text = Sound_Rhythum.Inst.bpm.ToString();
        startTimeTxt.text = Sound_Rhythum.Inst.startTime.ToString();
        oEditor.SetActive(true);
        CreateMap();
    }

    private void OnDisable()
    {
        oEditor.SetActive(false);
    }

    private void Update()
    {
        if(isPlay)
        {
            tMap.transform.Translate(-scrollingSpeed * Time.deltaTime * transform.right);
        }
    }

    void PlayButton()
    {
        if(Sound_Rhythum.Inst.PlayOrPauseButton())
        {
            isPlay = true;
            musicButton[0].gameObject.GetComponentInChildren<TextMeshProUGUI>().text = "l l";
        }
        else
        {
            isPlay = false;
            musicButton[0].gameObject.GetComponentInChildren<TextMeshProUGUI>().text = "►";
        }
    }

    void ResetButton()
    {
        isPlay = false;
        Sound_Rhythum.Inst.Clear();
        musicButton[0].gameObject.GetComponentInChildren<TextMeshProUGUI>().text = "►";
        CreateMap();
    }

    public void CreateMap()
    {
        DeleteLine();
        float totalDistance = scrollingSpeed * 60f;
        float objectWidth = totalDistance / Sound_Rhythum.Inst.bpm;
        pBlock.transform.GetChild(0).transform.localScale = new Vector3(objectWidth, pBlock.transform.localScale.y, pBlock.transform.localScale.z);
        pBlock.transform.GetChild(1).transform.localScale = new Vector3(objectWidth, pBlock.transform.localScale.y, pBlock.transform.localScale.z);
        for (int i = 0; i < Sound_Rhythum.Inst.bpm * 5; i++)
        {
            
            GameObject obj = Instantiate(pBlock, tMap);
            obj.transform.Translate(i * objectWidth, 0,0);
        }
        tMap.transform.Translate(-Sound_Rhythum.Inst.startTime, 0,0);
    }

    void DeleteLine()
    {
        tMap.transform.position = new Vector3(0,0,0);
        Transform[] childList = tMap.gameObject.GetComponentsInChildren<Transform>();

        if (childList != null)
        {
            for (int i = 1; i < childList.Length; i++)
            {
                if (childList[i] != transform)
                    Destroy(childList[i].gameObject);
            }
        }
    }

    public void BpmChange(bool add)
    {
        if(add)
        {
            Sound_Rhythum.Inst.bpm++;
        }
        else
        {
            Sound_Rhythum.Inst.bpm--;
        }
        bpmTxt.text = Sound_Rhythum.Inst.bpm.ToString();
    }
    public void StartTimeChange(bool add)
    {
        if (add)
        {
            Sound_Rhythum.Inst.startTime+=0.1f;
        }
        else
        {
            Sound_Rhythum.Inst.startTime-=0.1f;
        }
        startTimeTxt.text = Sound_Rhythum.Inst.startTime.ToString();
    }

    public void InputFieldChange(bool bpm)
    {
        if(bpm)
        {
            int a = Convert.ToInt32(bpmTxt.text);
            Sound_Rhythum.Inst.bpm = a;
        }
        else
        {
            Sound_Rhythum.Inst.startTime = Convert.ToInt32(startTimeTxt.text);
        }
        
    }
}
