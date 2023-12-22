using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 가시 깜빡이는 효과의 클래스
/// </summary>
public class AlertBlink : MonoBehaviour
{
    float time;
    [SerializeField] private float r = 1f; // 0부터 1 사이의 값으로 수정
    [SerializeField] private float g = 0.87f; // 0부터 1 사이의 값으로 수정
    [SerializeField] private float b = 0.0039f; // 0부터 1 사이의 값으로 수정

    void Update()
    {
        if (time < 0.3f)
        {
            GetComponent<SpriteRenderer>().color = new Color(r, g, b, 1 - time);
        }
        else
        {
            GetComponent<SpriteRenderer>().color = new Color(r, g, b, time);
            if (time > 0.5f)
            {
                time = 0;
            }
        }

        time += Time.deltaTime;
    }
}
