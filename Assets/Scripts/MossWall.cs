using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class MossWall : MonoBehaviour
{
    public Rigidbody2D other;
    public Define.PlayerState player_state;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            other.constraints = RigidbodyConstraints2D.FreezeAll;

        }
    }


    
    void Update()
    {
        if (Input.GetButtonDown("Jump") || Input.GetKeyDown(KeyCode.Space))
        {
            other.constraints = RigidbodyConstraints2D.FreezeRotation ;
        }
        
    }
}
