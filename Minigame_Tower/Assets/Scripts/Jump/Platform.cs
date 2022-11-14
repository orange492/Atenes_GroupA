using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    public Vector2 move_dir = Vector2.right;
    public float distance = 2.0f;
    public float move_time = 4.0f;

    public bool arrived = false; // 플랫폼이 이동을 완료 했을때 Flag가 True로 되는 변수

    public void Activate(float time)
    {
        StartCoroutine(Moveoverthereinafewseconds((Vector2)transform.position + move_dir * distance, move_time));
    }

public void Rotation(float time)
    {
        StartCoroutine(OneTurn(time));
    }

    IEnumerator OneTurn(float TargetTime)
    {
        float target_angle = 360.0f;
        if (transform.position.x < 0)
        {
            target_angle = target_angle;
        }
        else
        {
            target_angle = target_angle * -1.0f;
        }

        float ElapsedTime = 0.0f;
        Vector3 ori_angle = transform.eulerAngles;
        while (ElapsedTime < TargetTime)
        {
            transform.eulerAngles = Vector3.Lerp(ori_angle, Vector3.forward * target_angle, ElapsedTime / TargetTime);
            ElapsedTime += Time.deltaTime;
            yield return new WaitForSeconds(Time.deltaTime);
        }
    }

        IEnumerator Moveoverthereinafewseconds(Vector2 TargetPosition, float TargetTime)
    {
        float ElapsedTime = 0.0f;
        Vector2 FirstPosition = transform.position;
        while (ElapsedTime < TargetTime)
        {
            Vector2 new_pos = Vector2.Lerp(FirstPosition, TargetPosition, ElapsedTime / TargetTime);
            transform.position = new_pos;
            ElapsedTime += Time.deltaTime;
            yield return new WaitForSeconds(Time.deltaTime);
        }

        arrived = true;
    }
 }
