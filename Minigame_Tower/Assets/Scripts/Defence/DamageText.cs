using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamageText : MonoBehaviour
{
    const float DELETE_TIME = 0.5f;

    [SerializeField]
    TextMeshPro txt;
    float Timer = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Translate(0.5f *Time.deltaTime * transform.up);
        Timer += Time.deltaTime;
        if (Timer > DELETE_TIME)
        {
            this.gameObject.SetActive(false);
        }
    }

    void OnEnable()
    {
        Timer = 0;
    }

    void OnDisable()
    {
        ObjectPooler.ReturnToPool(gameObject);
    }

    public void Init(int dmg)
    {
        txt.text = dmg.ToString();
    }
}
