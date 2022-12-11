using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.TextCore.Text;
using UnityEngine;
using UnityEngine.UI;

public class DrawButton : MonoBehaviour
{
    public GameObject linePrefab;
    Button drawButton;
    Button drawEndButton;
    Line line;
    CanvasGroup drawEndButtonCanvasGroup;
    bool isDrawMode = false;

    public bool IsDrawMode => isDrawMode;


    private void Awake()
    {
        drawButton = transform.GetChild(0).GetComponent<Button>();
        drawButton.onClick.AddListener(OnDrawMode);
        drawEndButton = transform.GetChild(1).GetComponent<Button>();
        drawEndButton.onClick.AddListener(OffDrawMode);
        drawEndButtonCanvasGroup = transform.GetChild(1).GetComponent<CanvasGroup>();
        DrawEndButtonShutDown();
    }

    private void OffDrawMode()
    {
        DrawEndButtonShutDown();
        line.IsDrawingObject = false;
        isDrawMode = false;
        line.EndDraw();
    }

    private void OnDrawMode()
    {
        if (line == null || !line.IsDrawingObject)
        {
            line = Instantiate(linePrefab).GetComponent<Line>();
            line.IsDrawingObject = true;
            isDrawMode = true;
            DrawEndButtonActive();
        }
    }

    void DrawEndButtonShutDown()
    {
        drawEndButtonCanvasGroup.alpha = 0;
        drawEndButtonCanvasGroup.interactable = false;
        drawEndButtonCanvasGroup.blocksRaycasts = false;
    }

    void DrawEndButtonActive()
    {
        drawEndButtonCanvasGroup.alpha = 1;
        drawEndButtonCanvasGroup.interactable = true;
        drawEndButtonCanvasGroup.blocksRaycasts = true;
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
