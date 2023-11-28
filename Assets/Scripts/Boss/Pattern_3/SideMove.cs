using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SideMove : MonoBehaviour
{
    public float direction = 1f;
    public float duration;
    public float target;

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.transform.tag == "Alert")
        {
            direction = direction * -1;
        }
    }

    private void Start()
    {
        for(int i=0; i<6; i++)
        {
            transform.DOMoveX(target * direction, duration, false);
            direction = direction * -1f;
        }
    }
}
