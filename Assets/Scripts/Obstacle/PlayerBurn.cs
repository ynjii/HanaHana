using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBurn : MonoBehaviour
{
    [SerializeField] private bool isCol = false;

    private SpriteRenderer _playerSprite;
    private void Start()
    {
        _playerSprite = GameObject.FindWithTag("Player").GetComponent<SpriteRenderer>();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (!isCol) return;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isCol) return;
    }
}
