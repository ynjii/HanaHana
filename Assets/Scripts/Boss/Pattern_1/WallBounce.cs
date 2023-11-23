using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 랜덤으로 튀어나가는 함수.
/// 사과가 벽에 튕길 때 어디로 가는가?
/// 이 함수 이대로 괜찮은가. 
/// </summary>
public class WallBounce : MonoBehaviour
{
    [SerializeField][Range(500f, 2000f)] float speed = 1000f;
    public Rigidbody2D rb;
    float randomX, randomY;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        randomX = Random.Range(-1f, 1f);
        randomY = Random.Range(-1f, 1f);

        Vector2 dir = new Vector2(randomX, randomY).normalized;

        rb.AddForce(dir * speed);
    }
}
