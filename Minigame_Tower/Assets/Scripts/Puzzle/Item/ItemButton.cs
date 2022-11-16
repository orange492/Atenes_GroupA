using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D.Sprites;
using UnityEngine;
using UnityEngine.UI;

public class ItemButton : MonoBehaviour
{
    bool isClicked = false;
   public bool IsClicked => isClicked;
    Button bombButton;
    Button resetButton;
    MeshRenderer meshRenderer;
    [SerializeField]
    Material[] material;
    int materialIndex=0;
    Image image;

    BlockController blockController;

    private void Awake()
    {
        bombButton = transform.GetChild(0).GetComponent<Button>();
        bombButton.onClick.AddListener(OnClick_Bomb);
        resetButton = transform.GetChild(1).GetComponent < Button > ();
        resetButton.onClick.AddListener(OnClick_Reset);
        image = transform.GetChild(0).GetComponent<Image>();
      
        image.material = material[materialIndex];
    }

 

    void Start()
    {
        blockController = FindObjectOfType<BlockController>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnClick_Bomb()
    {
        isClicked = !isClicked;
        materialIndex++;
        materialIndex %= 2;
        image.material = material[materialIndex];
    }

    private void OnClick_Reset()
    {
        blockController.ResetAllBlock();
        blockController.EmptyBlockCheck();
        blockController.CharaterDownPlay();
        blockController.ResetList();
        
    }

}
