using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lightning : MonoBehaviour
{
    [SerializeField] private GameObject lightning;
    [SerializeField] private bool isCol;

    private void OnCollisionEnter2D(Collision2D other)
    {
        
        if(!isCol) return;
        if ((other.gameObject.CompareTag("Player"))){
            lightning.SetActive(true);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isCol) return;
        if ((other.gameObject.CompareTag("Player"))){
            lightning.SetActive(true);
        }
    }
}
