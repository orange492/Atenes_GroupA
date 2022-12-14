using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.TextCore.Text;
using UnityEngine;
using UnityEngine.UI;


public class DrawButton : MonoBehaviour
{
    public GameObject linePrefab;
    Button drawButton;
    Button drawEndButton;
    Button drawExitButton;
    Line line;
    CanvasGroup drawEndButtonCanvasGroup;
    bool isDrawMode = false;
    Shop shop;



    public bool IsDrawMode
    {
        get => isDrawMode;
        set
        {
            isDrawMode = value;
        }
    }



    private void Awake()
    {
        drawButton = transform.GetChild(0).GetComponent<Button>();
        drawButton.onClick.AddListener(OnDrawMode);
        drawEndButton = transform.GetChild(1).GetComponent<Button>();
        drawEndButton.onClick.AddListener(OffDrawMode);
        drawEndButtonCanvasGroup = transform.GetChild(1).GetComponent<CanvasGroup>();
        drawExitButton = transform.GetChild(2).GetComponent<Button>();
        drawExitButton.onClick.AddListener(ExitDrawMode);

        DrawEndButtonShutDown();
    }

    void Start()
    {
        shop = FindObjectOfType<Shop>();
    }

    private void ExitDrawMode()
    {
        if (isDrawMode)
        {
            OffDrawMode();
        }
        shop.ShopOpenButtonActivate();
        shop.IsOnDrawMode = false;
        transform.gameObject.SetActive(false);

    }

    private void OffDrawMode()
    {
        DrawEndButtonShutDown();
        line.IsDrawingObject = false;
        IsDrawMode = false;
        line.EndDraw();
    }

    private void OnDrawMode()
    {
        if (line == null || !line.IsDrawingObject)
        {
            line = Instantiate(linePrefab).GetComponent<Line>();
            line.IsDrawingObject = true;
            IsDrawMode = true;
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



}
