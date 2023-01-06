using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
using static UnityEngine.Rendering.DebugUI;

public class Line : MonoBehaviour
{
    PlayerInputActions inputActions;
    bool isDrawingObject = false;
    LineRenderer line;
    int lineIndex = 0;
    //EdgeCollider2D edgeCollider2D;
    PolygonCollider2D polygonCollider2;
    List<Vector2> linePositions;
    Rigidbody2D rigid;
    Trash trash;
    DrawButton drawButton;
    Vector3 offSet;

    float length;
    Mesh mesh;

    float price=0.0f;

    Shop shop;

    public int LineIndex => lineIndex;

    bool movingMode = false;

    public bool IsDrawingObject
    {
        get => isDrawingObject;
        set
        {
            isDrawingObject = value;
            if (!isDrawingObject)
            {
                rigid.gravityScale = 1.0f;
            }
        }
    }

    private void Awake()
    {
        inputActions = new PlayerInputActions();
        line = GetComponent<LineRenderer>();
        // edgeCollider2D = GetComponent<EdgeCollider2D>();
        polygonCollider2 = GetComponent<PolygonCollider2D>();
        rigid = GetComponent<Rigidbody2D>();
        linePositions = new List<Vector2>();
        rigid.gravityScale = 0;
        trash = FindObjectOfType<Trash>();
        drawButton = FindObjectOfType<DrawButton>();
    }

    private void Start()
    {
        shop = FindObjectOfType<Shop>();
    }

    private void Update()
    {
        if (isDrawingObject)
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(inputActions.Input.Pos.ReadValue<Vector2>());
            mousePos = new Vector3(mousePos.x, mousePos.y, 0);
            line.SetPosition(lineIndex, mousePos);
        }
    }

    private void OnEnable()
    {
        inputActions.Input.Enable();
        inputActions.Input.Click.performed += OnClick;
        inputActions.Input.Click.canceled += OffClick;

    }


    private void OnDisable()
    {
        inputActions.Input.Click.canceled -= OffClick;
        inputActions.Input.Click.performed -= OnClick;
        inputActions.Input.Disable();

    }
    private void OffClick(InputAction.CallbackContext obj)
    {
        if (isDrawingObject)
        {
            line.positionCount++;
            DrawLine();
            //edgeCollider2D.points = linePositions.ToArray();
            //polygonCollider2.points = linePositions.ToArray();
            lineIndex++;
        }
    }

    private void OnClick(InputAction.CallbackContext obj)
    {

    }

    void DrawLine()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(inputActions.Input.Pos.ReadValue<Vector2>());

        if (isDrawingObject)
        {

            mousePos = new Vector3(mousePos.x, mousePos.y, 0);
            linePositions.Add(mousePos);
            line.SetPosition(lineIndex, mousePos);
        }
    }

    public void EndDraw()
    {
        if (line.positionCount < 2)
        {
            Destroy(this.gameObject);
        }
        line.positionCount--;
        line.positionCount--;
        //Debug.Log(line.positionCount);
        linePositions.RemoveAt(linePositions.Count - 1);

        if (line.positionCount < 3)
        {
            Destroy(this.gameObject);
        }
        else
        {
            polygonCollider2.points = linePositions.ToArray();
            polygonCollider2.isTrigger = false;
            length = GetLineLength();
            mesh = polygonCollider2.CreateMesh(true, true);



            MeshFilter mf = GetComponent<MeshFilter>();
            mf.mesh = mesh;
            price = ((float)(int)(length * 10.0f))*0.01f;
            Debug.Log(price);
            if (shop.MoneyRemain < price)
            {
                shop.MoneyLack();
                Destroy(this.gameObject);
            }
            else
            {
            shop.Purchase(price);
            }
        }

        
   
    }

    public void RemoveLastDraw()
    {
        lineIndex--;
        line.positionCount--;
        linePositions.RemoveAt(linePositions.Count - 1);
        //edgeCollider2D.points = linePositions.ToArray();
        //polygonCollider2.points = linePositions.ToArray();
    }



  

  



    private void OnMouseDown()
    {
        if (drawButton.IsDrawMode)
        {
            return;
        }
        if (EggGameManager.Inst.mode == EggGameManager.Mode.Play)
        {
            return;
        }
        polygonCollider2.isTrigger = true;
        rigid.velocity = Vector2.zero;
        rigid.freezeRotation = true;
        Vector3 mousePosition = new Vector3(Input.mousePosition.x,
Input.mousePosition.y,-0.1f);
        Vector3 objPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        offSet = transform.position - (Vector3)objPosition;
    }


    private void OnMouseUp()
    {
        if (drawButton.IsDrawMode)
        {
            return;
        }
        if (EggGameManager.Inst.mode == EggGameManager.Mode.Play)
        {
            return;
        }
        movingMode = true;
        polygonCollider2.isTrigger = false;
        rigid.velocity = Vector2.zero;
        rigid.freezeRotation = false;

        StartCoroutine(ModeChange());
        
    }
    void OnMouseDrag()
    {
        if (drawButton.IsDrawMode)
        {
            return;
        }
        if (EggGameManager.Inst.mode == EggGameManager.Mode.Play)
        {
            return;
        }
        Vector3 mousePosition = new Vector3(Input.mousePosition.x,
Input.mousePosition.y,-0.1f);
        Vector3 objPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        transform.position = objPosition + offSet;
        rigid.velocity = Vector2.zero;
        rigid.freezeRotation = true;
    }





    private void OnTriggerStay2D(Collider2D collision)
    {
        if (movingMode)
        {
            if (collision.transform.CompareTag("Trash"))
            {
                movingMode = false;
                shop.Purchase(-price);
                shop.MoneyRefund();
                Destroy(this.gameObject);
                trash.MaterialChange();
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (movingMode)
        {
            if (collision.transform.CompareTag("Trash"))
            {
                movingMode = false;
                shop.Purchase(-price);
                shop.MoneyRefund();
                Destroy(this.gameObject);
                trash.MaterialChange();
            }
        }
    }

    float GetLineLength()
    {
        float length=0.0f;
        
        for (int i = 0; i < line.positionCount-1; i++)
        {
           length += (line.GetPosition(i) - line.GetPosition(i+1)).magnitude;
        }
        length += (line.GetPosition(0) - line.GetPosition(line.positionCount - 1)).magnitude;

       
        return length;
    }

   IEnumerator ModeChange()
    {
        yield return new WaitForSeconds(0.1f);
        movingMode = false;
    }
  

   

}
