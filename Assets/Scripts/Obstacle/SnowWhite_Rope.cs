using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowWhite_Rope : MonoBehaviour
{
    [SerializeField] private List<SpriteRenderer> _ropeSprites;
    [SerializeField] private Sprite _burningSprite;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(BurnRope());
        }
    }

    IEnumerator BurnRope()
    {
        yield return new WaitForSeconds(0.7f);
        for (int i = 0; i < _ropeSprites.Count - 1; i++)
        {
            yield return new WaitForSeconds(0.12f);
            _ropeSprites[i].gameObject.SetActive(false);
            _ropeSprites[i+1].sprite = _burningSprite;
            _ropeSprites[i + 1].transform.rotation = Quaternion.Euler(new Vector3(0, 0, -90));
            _ropeSprites[i + 1].transform.localScale = new Vector3(2, 2, 2);
        }
    }
}
