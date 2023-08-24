using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
public class SnowWhite_Rope : MonoBehaviour
{
    [SerializeField] private List<SpriteRenderer> _ropeSprites;
    [SerializeField] private Sprite _burningSprite;

    private void Start()
    {
        _burningSprite.
        StartCoroutine(BurnRope());
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            StartCoroutine(BurnRope());
        }
    }

    IEnumerator BurnRope()
    {
        for (int i = 0; i < _ropeSprites.Count - 1; i++)
        {
            yield return new WaitForSeconds(0.3f);
            _ropeSprites[i].gameObject.SetActive(false);
            _ropeSprites[i+1].sprite = _burningSprite;
        }
    }
}*/
