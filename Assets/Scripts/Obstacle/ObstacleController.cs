using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 움직이는 obstacle들의 부모 스크립트입니다.
/// </summary>
public class ObstacleController : MonoBehaviour
{

    public enum Direction
        {
            Up,
            Down,
            Left,
            Right
        }

    //protected 처리된 애들은 받아올 애들
    protected Direction currentDirection; //어디로 움직일지
    protected float distance; 
    protected float speed; 
    protected string tagName = "Enemy";
    
    private bool isMoving = false; //isTrigger 처리된 collider랑 부딪히면 true;
    private Vector3 initialPosition; 
    private float movedDistance = 0f; 
    
    public Rigidbody2D rigid;

    /// <summary>
    /// isTriggered 처리가 된 collider와 부딪혔을때 
    /// 부딪혔다는 사실을 isTriggred로 알리고, tag도 부딪히면 죽게 Enemy로 바꿔줍니다.
    /// 만약 isTriggred 처리된 collider를 사용하고 싶다면 주의해서 사용해주셔야해요.
    /// </summary>
    protected void OnTriggerEnter2D(Collider2D collision) {
        if(collision.gameObject.CompareTag("Player"))
        {
            isMoving = true;   
            this.gameObject.tag = tagName;
        }
    }

    /// <summary>
    /// 화면 나가면 죽음
    /// 현재 setActive(false);가 통하지 않는 버그가 있습니다 ㅠ
    /// void onBecomInvisible까지는 잘됨.
    /// 일단 보고 tilemapRnderer 사용하는 것도 하나의 방법같아요.
    /// </summary>
    void OnBecameInvisible()
    {
        gameObject.SetActive(false);
    }

    protected void Awake()
    {
        initialPosition = transform.position;
    }

    private void Update() 
    {
        if(isMoving && movedDistance < distance)
        {
            ObstacleMove();
            movedDistance = Vector3.Distance(initialPosition, transform.position);
        }
    }

    /// <summary>
    /// 이제 주어진 방향, 속도, 거리만큼 obstacle이 움직입니다. 
    /// </summary>
    private void ObstacleMove()
    {
        Vector3 movement = Vector3.zero;
        switch (currentDirection)
        {
            case Direction.Up:
                movement = Vector3.up * speed * Time.deltaTime;
                break;
            case Direction.Down:
                movement = Vector3.down * speed * Time.deltaTime;
                break;
            case Direction.Left:
                movement = Vector3.left * speed * Time.deltaTime;
                break;
            case Direction.Right:
                movement = Vector3.right * speed * Time.deltaTime;
                break;
        }

        transform.position += movement;
    }
}
