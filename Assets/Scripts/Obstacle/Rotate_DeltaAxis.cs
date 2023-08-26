using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class Rotate_DeltaAxis : MonoBehaviour
{
    [SerializeField] private Tags compareTag;
    [SerializeField] private float rotateDelta;
    [SerializeField] private Vector3 point;
    [SerializeField] private Collider2D collisionCollider;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag(compareTag.ToString())) 
            return;

        collisionCollider.enabled = true;
        transform.RotateAround(transform.position + point, Vector3.forward, rotateDelta);
    }
}
