using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SlingShot : MonoBehaviour
{
    LineRenderer rightLine;
    LineRenderer leftLine;
    Transform net;
    Transform netPosRight;
    Transform netPosLeft;
    Egg egg;


    PlayerInputActions inputActions;



    Vector2 onClickPosition;
    Vector2 offClickPostion;
    Vector3 netDir;
    Vector3 zeroPos;
    Vector3 zeroPosWorld;
    public float force = 10.0f;

    EdgeCollider2D edgeCollider2;
    Transform ground;

    Vector3 offSet;

    public bool isEggOnSlingShot = true;
    public bool isClicked;
    private void Awake()
    {
        egg = transform.GetComponent<Egg>();
        inputActions = new PlayerInputActions();
        leftLine = transform.GetChild(3).GetComponent<LineRenderer>();
        rightLine = transform.GetChild(2).GetComponent<LineRenderer>();
        net = transform.GetChild(4).transform;
        zeroPos = transform.GetChild(5).transform.localPosition;
        zeroPosWorld = transform.GetChild(5).transform.position;
        egg = FindObjectOfType<Egg>();

        netPosRight = net.GetChild(0).transform;
        netPosLeft = net.GetChild(1).transform;


    }

    private void Start()
    {
    }



    private void Update()
    {
        if (EggGameManager.Inst.mode == EggGameManager.Mode.Play || EggGameManager.Inst.mode == EggGameManager.Mode.Die)
        {
            if (isClicked && isEggOnSlingShot)
            {
                onClickPosition = Camera.main.ScreenToWorldPoint(inputActions.Input.Pos.ReadValue<Vector2>());
                if (onClickPosition.y < -9.5f)
                {
                    onClickPosition.y = -9.5f;
                }
                net.position = onClickPosition;
            }

            if (isEggOnSlingShot)
            {
                egg.Rigid.position = net.position + Vector3.up * 0.1f;
                egg.Rigid.velocity = Vector2.zero;
            }
            else
            {
                if ((netDir - net.localPosition).sqrMagnitude > 0.025f)
                {
                    if (((netDir - net.localPosition).normalized * Time.deltaTime * 60.0f).magnitude < ((netDir - net.localPosition) * Time.deltaTime * 60.0f).magnitude)
                    {
                        net.localPosition += (netDir - net.localPosition).normalized * Time.deltaTime * 60.0f;
                    }
                    else
                    {
                        net.localPosition += (netDir - net.localPosition) * Time.deltaTime * 60.0f;
                    }
                }
                else
                {
                    SetNetDir();
                }
            }
            leftLine.SetPosition(0, netPosRight.position - transform.position);
            rightLine.SetPosition(1, netPosLeft.position - transform.position);
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

    private void OnClick(InputAction.CallbackContext obj)
    {
        if (EggGameManager.Inst.mode != EggGameManager.Mode.Play)
        {
            return;
        }
        isClicked = true;
    }
    private void OffClick(InputAction.CallbackContext obj)
    {
        if (EggGameManager.Inst.mode != EggGameManager.Mode.Play)
        {
            return;
        }
        if (isEggOnSlingShot)
        {
            offClickPostion = Camera.main.ScreenToWorldPoint(inputActions.Input.Pos.ReadValue<Vector2>());
            isClicked = false;
            isEggOnSlingShot = false;
            egg.EggMove((zeroPosWorld - (Vector3)offClickPostion) * force);
            float plusMinus = 1.0f;
            if (zeroPos.x - offClickPostion.x > 0)
            {
                plusMinus *= -1.0f;
            }
            egg.EggRotate((zeroPosWorld - (Vector3)offClickPostion).magnitude * plusMinus);
            SetNetDir();
        }

    }

    void SetNetDir()
    {
        netDir = zeroPos - net.localPosition * 0.8f;
    }







}
