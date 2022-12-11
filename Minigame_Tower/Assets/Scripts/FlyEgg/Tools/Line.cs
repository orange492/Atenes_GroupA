using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

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
            polygonCollider2.points = linePositions.ToArray();
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
        line.positionCount--;
        line.positionCount--;
        linePositions.RemoveAt(linePositions.Count - 1);
        //    edgeCollider2D.points = linePositions.ToArray();
        polygonCollider2.points = linePositions.ToArray();
        polygonCollider2.isTrigger = false;
    }

  



    private void OnMouseDown()
    {
        if (drawButton.IsDrawMode)
        {
            return;
        }
        polygonCollider2.isTrigger = true;
        rigid.velocity = Vector2.zero;
        rigid.freezeRotation = true;
        Vector2 mousePosition = new Vector2(Input.mousePosition.x,
Input.mousePosition.y);
        Vector2 objPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        offSet = transform.position - (Vector3)objPosition;
    }


    private void OnMouseUp()
    {
        if (drawButton.IsDrawMode)
        {
            return;
        }
        
        polygonCollider2.isTrigger = false;
        rigid.velocity = Vector2.zero;
        rigid.freezeRotation = false;
    }
    void OnMouseDrag()
    {
        if (drawButton.IsDrawMode)
        {
            return;
        }
        Vector2 mousePosition = new Vector2(Input.mousePosition.x,
Input.mousePosition.y);
        Vector2 objPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        transform.position = objPosition + (Vector2)offSet;
        rigid.velocity = Vector2.zero;
        rigid.freezeRotation = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Trash"))
        {
            Destroy(this.gameObject);
            trash.MaterialChange();
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Trash"))
        {
            Destroy(this.gameObject);
            trash.MaterialChange();
        }
    }

}
