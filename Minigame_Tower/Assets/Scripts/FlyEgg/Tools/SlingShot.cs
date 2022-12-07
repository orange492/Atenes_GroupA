using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SlingShot : MonoBehaviour
{
    LineRenderer rightLine;
    LineRenderer leftLine;
    Transform net;
    Egg egg;

    PlayerInputActions inputActions;

    Vector2 onClickPosition;
    Vector2 offClickPostion;
    Vector3 netDir;
    Vector3 zeroPos;
    public float force = 1.0f;


    public bool isEggOnSlingShot = true;
    public bool isClicked;
    private void Awake()
    {
        egg = transform.GetComponent<Egg>();
        inputActions = new PlayerInputActions();
        rightLine = transform.GetChild(2).GetComponent<LineRenderer>();
        leftLine = transform.GetChild(3).GetComponent<LineRenderer>();
        net = transform.GetChild(4).transform;
        zeroPos = transform.GetChild(5).transform.position;
        egg = FindObjectOfType<Egg>();

    }



    private void Update()
    {
        if (isClicked && isEggOnSlingShot)
        {
            onClickPosition = Camera.main.ScreenToWorldPoint(inputActions.Input.Pos.ReadValue<Vector2>());
            if (onClickPosition.y < -4.0f)
            {
                onClickPosition.y = -4.0f;
            }
            net.position = onClickPosition;
        }

        if (isEggOnSlingShot)
        {
            egg.transform.position = net.position + Vector3.up * 0.5f;
            egg.Rigid.velocity = Vector2.zero;
        }
        else
        {
            if ((netDir - net.position).sqrMagnitude > 0.025f)
            {
                 net.position += (netDir - net.position).normalized * Time.deltaTime * 30.0f;
            }
            else
            {
                SetNetDir();
            }
        }

        leftLine.SetPosition(0, net.position - transform.position);
        rightLine.SetPosition(1, net.position - transform.position);
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

    private void OnClick(InputAction.CallbackContext obj)
    {
        isClicked = true;



    }
    private void OffClick(InputAction.CallbackContext obj)
    {
        if (isEggOnSlingShot)
        {
            offClickPostion = Camera.main.ScreenToWorldPoint(inputActions.Input.Pos.ReadValue<Vector2>());
            isClicked = false;
            isEggOnSlingShot = false;
            egg.EggMove((zeroPos - (Vector3)offClickPostion)*force);
            SetNetDir();
        }
    }

    void SetNetDir()
    {
        netDir = zeroPos - net.position*0.8f;
    }
}
