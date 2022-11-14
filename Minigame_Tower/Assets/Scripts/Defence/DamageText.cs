using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamageText : MonoBehaviour
{
    const float DELETE_TIME = 0.5f;
    Dictionary<int, Color> color = new Dictionary<int, Color>()
    {
     {0, Color.white},
     {1, Color.yellow},
     {2, Color.green},
};
    [SerializeField]
    TextMeshPro txt;
    Transform tr;
    float y;
    float Timer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        y += 0.5f * Time.deltaTime;
        transform.position = new Vector2(tr.position.x, tr.position.y + y);
        Timer += Time.deltaTime;
        if (Timer > DELETE_TIME)
        {
            this.gameObject.SetActive(false);
        }
    }

    void OnEnable()
    {
        Timer = 0;
        y = 0;
    }

    void OnDisable()
    {
        ObjectPooler.ReturnToPool(gameObject);
    }

    public void Init(Transform _tr, int dmg, int type = 0)
    {
        tr = _tr;
        txt.text = dmg.ToString();
        txt.color = color[type];
    }
}
