using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Detection : MonoBehaviour
{
    EdgeCollider2D edgeCollider2;
    LineRenderer lineRenderer;
    int segments=1000;
    float radius;
    bool isOnDetective = false;
    float spreadSpeed = 20.0f;
    float detectiveArea = 20.0f;

    public bool IsOnDetective
    {
        get => isOnDetective;
        set => isOnDetective = value;
    }
    private void Awake()
    {
        edgeCollider2 = GetComponent<EdgeCollider2D>();
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount=(segments + 1);
        lineRenderer.useWorldSpace = false;
        lineRenderer.startWidth=0.02f;
        lineRenderer.endWidth = 0.02f;
    }

    private void Update()
    {
        
        if (isOnDetective)
        {
            if (edgeCollider2.edgeRadius < detectiveArea)
            {
                edgeCollider2.edgeRadius += spreadSpeed * Time.deltaTime;
            }
            else
            {
                edgeCollider2.edgeRadius = 0.0f;
                isOnDetective = false;
            }
        }
        ColliderVisualize();
    }

    void ColliderVisualize()
    {
        radius = edgeCollider2.edgeRadius;
        float x;
        float y;
        float z = 0.0f;

        float angle = 20.0f;

        for (int i = 0; i < segments+1; i++)
        {
            x = Mathf.Cos(Mathf.Deg2Rad * angle) * radius;
            y = Mathf.Sin(Mathf.Deg2Rad * angle) * radius;

            lineRenderer.SetPosition(i, new Vector3(x, y+1, z));
            angle += 360f / segments;
        }
      
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Trap"))
        {
            ITrap trap =collision.GetComponent<ITrap>();
            trap.Visualize();
        }
    }

}
