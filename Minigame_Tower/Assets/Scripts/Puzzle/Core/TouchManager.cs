using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TouchManager : MonoBehaviour
{
    InputActions inputActions;
    Vector2 onClickPosition;
    Vector2 offClickPosition;
    Vector2 dragDir;
   protected GameObject touchedObject;
   protected GameObject targetObject;
   protected BlockController blockController;
  protected  int touchedIndexX;
   protected int touchedIndexY;
    protected int targetIndexX;
    protected int targetIndexY;
    bool isMoving = false;
    bool isClickLock = false;
    ItemButton itemButton;

    public bool IsClickLock
    {
        get => isClickLock;
        set => isClickLock = value;
    }

    private void Awake()
    {
        inputActions = new InputActions();
    }
    private void Start()
    {
        blockController = FindObjectOfType<BlockController>();
        itemButton = FindObjectOfType<ItemButton>();
    }

    private void OnEnable()
    {
        inputActions.Touch.Enable();
        inputActions.Touch.Touch.performed += OnClick;
        inputActions.Touch.Touch.canceled += OffClick;
        inputActions.Touch.Test.performed += Test4;
    }

    private void Test4(InputAction.CallbackContext obj)
    {
   
    }

    private void OnDisable()
    {
        inputActions.Touch.Touch.performed -= OnClick;
        inputActions.Touch.Touch.canceled -= OffClick;
        inputActions.Touch.Disable();
    }

    private void OffClick(InputAction.CallbackContext obj)
    {
        if (blockController.IsGameOver)
        {
            return;
        }
        if (blockController.Mode == BlockController.GameMode.Checking)
        {
            return;
        }
        if (touchedObject == null) //터치한 오브젝트가 있는지 확인
        {
            return;
        }
        if (!touchedObject.CompareTag("Block")) //터치한 오브젝트가 블록인지 확인
        {
            return;
        }
        if (touchedObject.transform.childCount == 1) //터치한 오브젝트의 자식이 있는지 확인
        {
            return;
        }

        offClickPosition = Mouse.current.position.ReadValue();
        Vector2 touchPos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        RaycastHit2D hitInformation = Physics2D.Raycast(touchPos, Camera.main.transform.forward);
        if (hitInformation.collider != null)
        {
            targetObject = hitInformation.transform.gameObject;
        }

        if (targetObject == touchedObject && itemButton.IsClicked && itemButton.BombRemain > 0 && touchedObject.transform.GetChild(1).GetComponent < Character_Bomb >()== null)
        {
            itemButton.BombRemain--;
            itemButton.BombRemainToText();
            Block targetBlock= targetObject.GetComponent<Block>();
            targetBlock.DestroyCharacter();
            targetBlock.StartCoroutine(targetBlock.BombCreate(0.0f));
            ResetObject();
            return;
        }
        
        if (touchedObject.transform.GetChild(1).GetComponent<Character_Bomb>() != null && targetObject == touchedObject)
        {
            blockController.BombExplosion(touchedIndexX, touchedIndexY);
            ResetObject();
            return;
        }

        if(touchedObject.transform.GetChild(1).GetComponent<Character_Bomb>() != null)
        {
            return;
        }
        
        dragDir = (offClickPosition - onClickPosition);

        if (dragDir.magnitude > Vector2.right.magnitude * 50) //드래그 모션인지 확인
        {
            float singedAngle = Vector2.SignedAngle(Vector2.right, dragDir); //상하좌우 판별을 위한 두벡터의 사이각 구하기



            if (singedAngle >= -45 && singedAngle < 45)
            {
               // Debug.Log("우");
                if (touchedIndexX < blockController.blockXSize - 1 && !isMoving)
                {
                    targetIndexX += 1;
                    MoveCharacter("Left", "Right");
                }
                else
                {
                  //  Debug.Log("오른쪽 이동불가");
                }
            }
            else if (singedAngle >= 45 && singedAngle < 135)
            {
               // Debug.Log("상");
                if (touchedIndexY >blockController.invisibleBlockYSize && !isMoving)
                {
                    targetIndexY -= 1;
                    MoveCharacter("Down", "Up");
                }
                else
                {
                   // Debug.Log($"위쪽 이동불가");
                }

            }
            else if (singedAngle >= 135 || singedAngle < -135)
            {
               // Debug.Log("좌");
                if (touchedIndexX > 0 && !isMoving)
                {
                    targetIndexX -= 1;
                    MoveCharacter("Right", "Left");
                }
                else
                {
                    //Debug.Log("왼쪽 이동불가");
                }
            }
            else
            {
                // Debug.Log("하");
                if (touchedIndexY < blockController.blockYSize - 1 && !isMoving)
                {
                    targetIndexY += 1;
                    MoveCharacter("Up", "Down");
                }
                else { }

                    //Debug.Log("아래쪽 이동불가");
            }



        }
       
    }



    private void OnClick(InputAction.CallbackContext obj)
    {
        if (blockController.IsGameOver)
        {
            return;
        }
        onClickPosition = Mouse.current.position.ReadValue();
       

        if (blockController.Mode==BlockController.GameMode.Checking)
        {
            return;
        }

        Vector2 touchPos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        RaycastHit2D hitInformation = Physics2D.Raycast(touchPos, Camera.main.transform.forward);
        if (hitInformation.collider != null)
        {

            touchedObject = hitInformation.transform.gameObject;
            Debug.Log($"{touchedObject}");

            touchedIndexX = touchedObject.transform.GetComponent<Block>().IndexX; //블록의 인덱스 찾기
            touchedIndexY = touchedObject.transform.GetComponent<Block>().IndexY;
            targetIndexX = touchedIndexX;
            targetIndexY = touchedIndexY;
        }

    }

    void MoveCharacter(string targetAnim, string touchedAnim)
    {
        targetObject = blockController.blocks[targetIndexY][targetIndexX];


        if (touchedObject.GetComponentInChildren<Character_Base>() != null && targetObject.GetComponentInChildren<Character_Base>() != null)
        {

            if (touchedObject.GetComponentInChildren<Character_Base>().IsOnGargoyle || targetObject.GetComponentInChildren<Character_Base>().IsOnGargoyle)
            {
                return;
            }
        }

        if (targetObject.transform.childCount == 1 || targetObject.transform.GetChild(1).GetComponent<Character_Bomb>() != null)
        {
            return;
        }

        Character_Base targetCharacter = targetObject.transform.GetChild(1).GetComponent<Character_Base>();
        Character_Base touchedCharacter = touchedObject.transform.GetChild(1).GetComponent<Character_Base>();

        targetCharacter.AnimationActive(targetAnim);
        touchedCharacter.AnimationActive(touchedAnim);

    }

    public virtual void ChildChange()
    {

        if (touchedObject == null || targetObject == null || touchedObject == targetObject)
        {
            return;
        }
        if (touchedObject.transform.childCount == 1 ||
            targetObject.transform.childCount == 1)
        {
            return;
        }

        touchedObject.transform.GetChild(1).transform.parent = transform;
        targetObject.transform.GetChild(1).transform.parent = touchedObject.transform;
        transform.GetChild(0).transform.parent = targetObject.transform;
        targetObject.transform.GetChild(1).localPosition = Vector3.zero;
        touchedObject.transform.GetChild(1).localPosition = Vector3.zero;
        if (blockController.ThreeMatchCheck(touchedIndexX, touchedIndexY) ||
            blockController.ThreeMatchCheck(targetIndexX, targetIndexY))
        {

            blockController.ThreeMatchAction(touchedIndexX, touchedIndexY);
            blockController.ThreeMatchAction(targetIndexX, targetIndexY);
         
        }
        else
        {
            touchedObject.transform.GetChild(1).transform.parent = transform;
            targetObject.transform.GetChild(1).transform.parent = touchedObject.transform;
            transform.GetChild(0).transform.parent = targetObject.transform;
            blockController.Mode = BlockController.GameMode.Normal;
            Debug.Log("잘못된 블럭을 옮겼을때 노말");
        }


        return;

    }

    public void ResetObject() //이전 클릭에서 저장된 오브젝트 초기화
    {
        touchedObject = null;
        targetObject = null;
    }
}