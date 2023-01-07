using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class CameraFollow : MonoBehaviour
{
    float moveSpeed = 3.0f;
    Egg egg;
    Transform target;
    Vector3 offset = Vector3.zero;
    SlingShot slingShot;
    Vector3 pos;

    float mouseFollowRange = 15.0f;


    Camera cameraMain;

    Trash trash;



    PlayerInputActions inputActions;

    float scroll;

    private void Awake()
    {
       
        cameraMain=GetComponent<Camera>();
        inputActions = new PlayerInputActions();
        
        pos = transform.position;
        inputActions.Input.Scroll.performed += x => scroll = x.ReadValue<float>();
    }
    private void Start()
    {
        egg = FindObjectOfType<Egg>();
        trash = FindObjectOfType<Trash>();
        target = egg.transform;
        offset = transform.position - target.position;
    }

    private void OnEnable()
    {
        inputActions.Enable();
    }

    private void OnDisable()
    {
        inputActions.Disable();
    }
    // Update is called once per frame
    void Update()
    {
      
        
        if (EggGameManager.Inst.mode == EggGameManager.Mode.ReadyToPlay||EggGameManager.Inst.mode==EggGameManager.Mode.Editting)
        {
            FollowMouse();
        }
        if (EggGameManager.Inst.mode == EggGameManager.Mode.Play)
        {
            FollowEgg();
        }
       trash.transform.position = (Vector2)Camera.main.transform.position + new Vector2(16.0f, -8.2f) * Camera.main.orthographicSize * 0.1f;
       trash.transform.localScale = new Vector3(Camera.main.orthographicSize * 0.1f, Camera.main.orthographicSize * 0.1f, 0);

    }

    public void SetSlingShot()
    {
        slingShot = FindObjectOfType<SlingShot>();
    }
    void FollowEgg()
    {
        if (!egg.IsDead)
        {
            if (slingShot == null || !slingShot.isEggOnSlingShot)
            {
                //transform.position = Vector3.Lerp(transform.position, target.position + offset, moveSpeed * Time.deltaTime);
                transform.position = target.position + offset;
            }
            else
            {
                transform.position = pos;
            }
        }
    }

    void FollowMouse()
    {
        Vector3 mousePos = inputActions.Input.Pos.ReadValue<Vector2>();
        float cameraSize = cameraMain.orthographicSize;
        if (mousePos.y < mouseFollowRange)
        {

            transform.position += Vector3.down * Time.deltaTime * moveSpeed * cameraSize;
        }
        if (mousePos.y > Screen.height - mouseFollowRange)
        {
            transform.position += Vector3.up * Time.deltaTime * moveSpeed * cameraSize;
        }
        if (mousePos.x > Screen.width - mouseFollowRange)
        {
            transform.position += Vector3.right * Time.deltaTime * moveSpeed * cameraSize;
        }
        if (mousePos.x < mouseFollowRange)
        {
            transform.position += Vector3.left * Time.deltaTime * moveSpeed * cameraSize;
        }

        cameraMain.orthographicSize -= scroll * Time.deltaTime;
        cameraMain.orthographicSize = Mathf.Clamp(cameraMain.orthographicSize, 1.0f, 20.0f);
    }

    public void SlinShotSet()
    {
        slingShot = FindObjectOfType<SlingShot>();
    }
}
