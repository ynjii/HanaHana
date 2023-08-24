using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RealItem : MonoBehaviour
{
    [SerializeField] private GameObject _player;
    [SerializeField] private string _realItemName;

    private void Start()
    {
        _player = GameObject.FindWithTag("Player");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject != _player)
        {
            return;
        }
        PlayerPrefs.SetString("RealItem", _realItemName);
        _player.GetComponent<Player>().ChangeSprites();
    }
}
