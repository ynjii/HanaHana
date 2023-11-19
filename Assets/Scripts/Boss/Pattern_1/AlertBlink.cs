using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 가시 깜빡이는 효과의 클래스
/// </summary>
public class AlertBlink : MonoBehaviour
{
    float time;
    [SerializeField] private int r=1;
    [SerializeField] private int g=1;
    [SerializeField] private int b=1;

    void Update()
    {
        if (time < 0.3f)
        {
            GetComponent<SpriteRenderer>().color = new Color(r, g, b, 1 - time);
        }
        else
        {
            GetComponent<SpriteRenderer>().color = new Color(r, g, b, time) ;
            if(time > 0.5f)
            {
                time = 0;
            }
        }

        time += Time.deltaTime;
        
    }
}
