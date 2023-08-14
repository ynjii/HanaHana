using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tranpoline : MonoBehaviour
{

    [SerializeField]
    private GameObject player;

    [SerializeField]
    private float jumpPower = 500f;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Rigidbody2D playerRigidbody = player.GetComponent<Rigidbody2D>();
        if (collision.gameObject.CompareTag("Player"))
        {
            Vector2 forceDirection = new Vector2(0, 1);
            playerRigidbody.AddForce(forceDirection * jumpPower * 1000);
        }
    }
}
