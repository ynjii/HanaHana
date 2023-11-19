using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

/// <summary>
/// 이끼벽. 
/// 1. 닿으면 플레이어 ridigbody 전부 freeze
/// 2. 스페이스, 점프 누르면 freeze 풀어(except rotation)
/// 잘 작동하나? 안 됐던 것 같은데 다시 확인
/// </summary>
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
