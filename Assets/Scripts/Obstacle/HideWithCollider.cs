using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideWithCollider : MonoBehaviour
{
    [SerializeField] private bool isCol;
    [SerializeField] private MonoBehaviour _component;

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (!isCol) return;
        
    }
}
