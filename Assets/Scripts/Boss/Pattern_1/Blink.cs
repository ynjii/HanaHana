using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blink : MonoBehaviour
{
    float time;
    [SerializeField] private int r=1;
    [SerializeField] private int g=1;
    [SerializeField] private int b=1;

    // Update is called once per frame
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
