using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float max_speed;
    [SerializeField]
    private float jump_power;
    Rigidbody2D rigid;
    SpriteRenderer sprite_renderer;
    Animator anim;
    public Define.PlayerState player_state;
    public bool isJumpButton=false;
    public bool isLeftButton = false;
    public bool isRightButton = false;
    public bool isButtonPressed = false;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        sprite_renderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        max_speed = 5;
        jump_power = 15;
        player_state=new PlayerState();
    }

    // Update is called once per frame
    void Update()//단발적 입력: 업데이트함수
    {
        //점프
        if ((Input.GetButtonDown("Jump")&&!anim.GetBool("isJump"))&&!(rigid.velocity.y < -0.5f))
        {
            if (player_state != PlayerState.Damaged) 
            {
               player_state = PlayerState.Jump;
            } 
            rigid.velocity = new Vector2(rigid.velocity.x, jump_power);
            anim.SetBool("isJump", true);
        }
      

        //브레이크
        if (Input.GetButtonUp("Horizontal"))
        {
            //normalized: 벡터크기를 1로 만든 상태. 방향구할 때 씀
            //방향에 속력을 0으로 
            rigid.velocity = new Vector2(rigid.velocity.normalized.x * 0.0000001f, rigid.velocity.y);
        }

        //방향전환
        if (Input.GetButton("Horizontal"))
            sprite_renderer.flipX = Input.GetAxisRaw("Horizontal") == -1;

        //애니메이션
        if (rigid.velocity.normalized.x == 0)
        {
            if (player_state != PlayerState.Damaged)
            {
                 player_state = PlayerState.Idle;
            }
            anim.SetBool("isWalk", false);
        }
        else
        {
            if (player_state != PlayerState.Damaged)
            {
                player_state = PlayerState.Walk;
            }
            anim.SetBool("isWalk", true);
        }

        // 화면 위에 손가락이 없는지 확인
        if (Input.touchCount == 0)
        {
            isButtonPressed = false;
            isJumpButton=false;
            isLeftButton=false;
            isRightButton=false;
   
        }
        // 화면 위에 손가락이 있는지 확인
        if (Input.touchCount > 0)
        {
            isButtonPressed = true;
        }
    }
    private void FixedUpdate()//물리 update
    {
        //키 컨트롤로 움직이기
        float h = Input.GetAxisRaw("Horizontal");
        rigid.velocity = new Vector2(max_speed*h, rigid.velocity.y);

        //버튼 이동
        if (isButtonPressed)
        {
            // 버튼을 계속 누르고 있을 때 호출할 메소드를 여기에 작성.
            if (isJumpButton)
            {
                //점프
                if (!anim.GetBool("isJump") && !(rigid.velocity.y < -0.5f))
                {
                    if (player_state != PlayerState.Damaged)
                    {
                        player_state = PlayerState.Jump;
                    }
                    rigid.velocity = new Vector2(rigid.velocity.x, jump_power);
                    anim.SetBool("isJump", true);
                }
            }
            if (isLeftButton)
            {
                rigid.velocity = new Vector2(max_speed * -1, rigid.velocity.y);
            }
            if (isRightButton)
            {
                rigid.velocity = new Vector2(max_speed * 1, rigid.velocity.y);
            }
        }

    }
 
    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if(collision.gameObject.CompareTag("Enemy"))
        { 
            onDamaged(collision.transform.position);           
            //게임 매니저의 게임오버 처리 실행
            GameManager.instance.OnPlayerDead();            
        }
    }

    private void OnTriggerEnter2D(Collider2D collision){
        if(collision.gameObject.CompareTag("Flag"))
        {
            //리스폰 위치를 해당 Flag 위치로 재설정
            Vector3 flagPosition=collision.gameObject.transform.position;
            PlayerRespawn playerRespawn = GetComponent<PlayerRespawn>();
            playerRespawn.SetRespawnPoint(flagPosition);
        }
        
        else if(collision.gameObject.CompareTag("Enemy"))
        { 
            onDamaged(collision.transform.position);           
            //게임 매니저의 게임오버 처리 실행
            GameManager.instance.OnPlayerDead();            
        }

        else if (collision.gameObject.CompareTag("Item"))
        {
            //진짜 아이템 먹었을 때 animation 바꿈
            anim.SetBool("isItemGet", true);
            collision.gameObject.SetActive(false);
        }
    }

    public void onDamaged(Vector2 targetPos)
    {
        //맞은 상태
        player_state = PlayerState.Damaged;
        //레이어변경
        this.gameObject.layer = 7;
        
        //투명하게 바꾸기
        sprite_renderer.color = new Color(1, 1, 1, 0.4f);

        //리액션
        int dirc = transform.position.x - targetPos.x > 0 ? 1 : -1;
        rigid.AddForce(new Vector2(dirc, 1) * 7, ForceMode2D.Impulse);

    }

    //화면밖으로 나감: 죽음
    private void OnBecameInvisible()
    {
        player_state = PlayerState.Damaged;
        GameManager.instance.OnPlayerDead();
        this.gameObject.SetActive(false);
    }


    //버튼을 눌렀는지 뗐는지
    public void jumpButtonTrue()
    {
        isJumpButton = true;
    }
    public void jumpButtonFalse()
    {
        isJumpButton = false;
    }
    public void leftButtonTrue()
    {
        
        isLeftButton = true;
        sprite_renderer.flipX = true;
    }
    public void leftButtonFalse()
    {
        isLeftButton = false;       
    }
    public void rightButtonTrue()
    {
        sprite_renderer.flipX = false;
        isRightButton = true;
    }
    public void rightButtonFalse()
    {
        isRightButton = false;
    }

}

