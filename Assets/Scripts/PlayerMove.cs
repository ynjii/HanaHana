using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] 
    private Sprite itemPlayer; //아이템 얻은 후
    [SerializeField]
    private float max_speed;
    [SerializeField]
    private float jump_power;
    Rigidbody2D rigid;
    SpriteRenderer sprite_renderer;
    Animator anim;
    
    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        sprite_renderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        max_speed = 3;
        jump_power = 8;
    }

    // Update is called once per frame
    void Update()//단발적 입력: 업데이트함수
    {
        //점프
        if (Input.GetButtonDown("Jump")&&!anim.GetBool("isJump"))
        { 
            rigid.AddForce(Vector2.up * jump_power, ForceMode2D.Impulse);
            anim.SetBool("isJump", true);
        }
        //브레이크
        if (Input.GetButtonUp("Horizontal"))
        {
            //normalized: 벡터크기를 1로 만든 상태. 방향구할 때 씀
            //방향에 속력을 0으로 
            rigid.velocity = new Vector2(rigid.velocity.normalized.x * 0.0f, rigid.velocity.y);
        }

        //방향전환
        if (Input.GetButtonDown("Horizontal"))
            sprite_renderer.flipX= Input.GetAxisRaw("Horizontal")==-1;

        //애니메이션
        if (rigid.velocity.normalized.x == 0)
        {
            anim.SetBool("isWalk", false);
        }
        else
        {
            anim.SetBool("isWalk", true);
        }
    }
    private void FixedUpdate()//물리 update
    {
        //키 컨트롤로 움직이기
        float h = Input.GetAxisRaw("Horizontal");
        rigid.AddForce(Vector2.right*h,ForceMode2D.Impulse);
        if(rigid.velocity.x> max_speed)//오른쪽
        {
            rigid.velocity = new Vector2(max_speed, rigid.velocity.y);
        }
        else if (rigid.velocity.x < max_speed*(-1))//왼쪽
        {
            rigid.velocity = new Vector2(max_speed*(-1), rigid.velocity.y);
        }


    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Platform")
        {
            anim.SetBool("isJump", false);
        }

        if(collision.gameObject.tag == "Enemy")
        { 
            onDamaged(collision.transform.position);
        }
        if (collision.gameObject.tag == "Item")
        {
            //아이템 얻은 후 애니메이션이 따로 있어야할듯
            anim.SetBool("isItemIdle", true);
            //changeSprite(itemPlayer);
            collision.gameObject.SetActive(false);
        }
    }
    
    void changeSprite(Sprite sprite)
    {
        if (sprite != null)
        {
            sprite_renderer.sprite = sprite;
        }
    }

    void onDamaged(Vector2 targetPos)
    {
        //레이어 바꾸기
        gameObject.layer = 7;

        //투명하게 바꾸기
        sprite_renderer.color = new Color(1, 1, 1, 0.4f);

        //리액션
        int dirc = transform.position.x - targetPos.x > 0 ? 1 : -1;
        rigid.AddForce(new Vector2(dirc, 1) * 7, ForceMode2D.Impulse);

        Invoke("Die", 0.1f);
    }
    private void OnBecameInvisible()
    {
        this.gameObject.SetActive(false);
        this.gameObject.layer = 9;//PlayerDied
    }
    void die()
    {
        //레이어 바꾸기
        gameObject.layer = 7;

    }

}
