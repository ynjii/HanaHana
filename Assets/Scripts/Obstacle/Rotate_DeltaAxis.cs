using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class Rotate_DeltaAxis : MonoBehaviour
{
    [SerializeField] private Tags compareTag;
    [SerializeField] private RotateDelta rotateDelta;
    void Start()
    {
        
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(compareTag.ToString()))
        {
            transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, (int)rotateDelta));
        }
    }
}
