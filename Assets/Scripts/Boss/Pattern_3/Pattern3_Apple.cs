using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;

public class Pattern3_Apple : MonoBehaviour
{
    private Rigidbody2D rb;
    public Vector3 newDir;
    public int bounceCount = 3;
    public int _speed = 15;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = newDir * _speed;
        Destroy(gameObject, 3f);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.transform.CompareTag("Player"))
        {
            other.gameObject.GetComponent<Player>().Die(new Vector2(transform.position.x, transform.position.y));
            Destroy(gameObject, 0.1f);
        }
        if (other.transform.CompareTag("Enemy"))
        {
            newDir = Vector3.Reflect(newDir, other.contacts[0].normal);
            rb.velocity = newDir * _speed * 2f;
        }
        if (other.transform.CompareTag("Platform"))
        {
            Debug.Log(other.transform.name);
            bounceCount--;
            if (bounceCount >= 0)
            {
                newDir = Vector3.Reflect(newDir, other.contacts[0].normal);
                rb.velocity = newDir * _speed * 1.25f;
            }
            else
            {
                Destroy(gameObject, 0.05f);
            }
        }
    }
}
