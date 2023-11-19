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
        if (collision.gameObject.CompareTag("Player")) //여기서 player 가 내려오는 속도때문에 덜 올라가는게 있는데 여기에 player.velocity = 0f 추가해주면 될듯
        {
            Vector2 forceDirection = new Vector2(0, 1);
            playerRigidbody.AddForce(forceDirection * jumpPower * 1000); //player 위로 올려보내기.
        }
    }
}
