using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SideMove : MonoBehaviour
{
    public float speed;
    public float direction = 1;

    private void Update()
    {
        transform.parent.position += Vector3.right * direction * Time.deltaTime * speed;
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Alert"))
        {
            direction = direction * -1;
        }
    }
}
