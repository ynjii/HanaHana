using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{
    private Player player_script;
    private Rigidbody2D player_rigid;
    private PlatformEffector2D platform_effector;
    private void Awake()
    {
        player_script = GameObject.FindWithTag("Player").GetComponent<Player>();
        player_rigid=GameObject.FindWithTag("Player").GetComponent<Rigidbody2D>();
        if (GetComponent<PlatformEffector2D>() != null)
        {
            platform_effector=GetComponent<PlatformEffector2D>();
        }
    }
    // Update is called once per frame
    void Update()
    {
        if(GetComponent<PlatformEffector2D>() != null && player_script.player_state==Define.PlayerState.Damaged) 
        {
            platform_effector.surfaceArc = 0;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 충돌한 오브젝트의 콜라이더를 가져옵니다.
        Collider2D otherCollider = collision.collider;

        // 머리에 닿았는가?
        if (!(otherCollider is CapsuleCollider2D) && otherCollider is CircleCollider2D && collision.gameObject.CompareTag("Player")&&!(player_rigid.velocity.y>=-0.000007&&player_rigid.velocity.y<=0.000002))
        {
            //점프 막기
            player_script.ignore_jump= true;
        }
        //다리에 닿았으면 ignore_jump =false
        if (otherCollider is CapsuleCollider2D && collision.gameObject.CompareTag("Player"))
        {
            //점프 풀어주기
            player_script.ignore_jump = false;
        }
    }
}
