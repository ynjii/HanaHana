using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 움직이는 obstacle들의 부모 스크립트입니다.
/// </summary>
public class ObstacleController : MonoBehaviour
{
    private bool isTriggered = false;

    public enum Direction
        {
            Up,
            Down,
            Left,
            Right
        }

    protected Direction currentDirection;
    protected float distance;
    protected float speed;
    public Rigidbody2D rigid;

    /// <summary>
    /// isTriggered 처리가 된 collider와 부딪혔을때 
    /// 부딪혔다는 사실을 isTriggred로 알리고, tag도 부딪히면 죽게 Enemy로 바꿔줍니다.
    /// 만약 isTriggred 처리된 collider를 사용하고 싶다면 주의해서 사용해주셔야해요.
    /// </summary>
    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.gameObject.CompareTag("Player"))
        {
            isTriggered = true;   
            this.gameObject.tag = "Enemy";
        }
    }

    /// <summary>
    /// 화면 나가면 죽음
    /// </summary>
    void OnBecameInvisible()
    {
        this.gameObject.SetActive(false);
    }

    protected void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    private void Update() 
    {
        if(isTriggered)
        {
            ObstacleMove();
        }
    }

    /// <summary>
    /// 이제 주어진 방향, 속도, 거리만큼 obstacle이 움직입니다. 
    /// </summary>
    private void ObstacleMove()
    {
        Vector3 velocity = Vector3.zero;
        switch (currentDirection)
        {
            case Direction.Up:
                velocity = Vector3.up * speed;
                break;
            case Direction.Down:
                velocity = Vector3.down * speed;
                break;
            case Direction.Left:
                velocity = Vector3.left * speed;
                break;
            case Direction.Right:
                velocity = Vector3.right * speed;
                break;
        }

        rigid.velocity = velocity; 

        //
         if (Vector3.Distance(transform.position, transform.position + velocity * Time.deltaTime) >= distance)
        {
            rigid.velocity = Vector3.zero;
        }
    }
}
