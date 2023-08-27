using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static Define;

public class Player : MonoBehaviour
{
    public AudioSource[] audioSources=null;


    [SerializeField] private bool invincibility = false;
    public bool Invincibility
    {
        get { return invincibility; }
        set { invincibility = value; }
    }

    private const float JUMP_CRITERIA=0.005f;
    
    [SerializeField]
    private float jump_power;
    private float max_speed;
    Rigidbody2D rigid;
    SpriteRenderer sprite_renderer;
    Animator anim;
    public Define.PlayerState player_state;
    private bool isJumpButton = false;
    private bool isLeftButton = false;
    private bool isRightButton = false;
    private bool isButtonPressed = false;
    private float horizontal = 0;

    private bool isBorder = false;
    private bool isIce = false;
    public GameObject SaveLoad;
    private bool isClimbing = false;
    private float inputVertical;


    public float ladder_speed;
    public LayerMask whatIsLadder;
    public float distance;
    private bool jump = true;
    public bool movable = true;

    private bool ignoreJump = false;

    //getter setter
    public float Horizontal
    {
        get
        {
            return horizontal;
        }
    }

    public bool IsLeftButton
    {
        get
        {
            return isLeftButton;
        }

    }

    public bool IsRightButton
    {
        get
        {
            return isRightButton;
        }
    }

    public bool IsJumpButton
    {
        get { return isJumpButton; }
    }
    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        sprite_renderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        max_speed = 5;
        player_state = new PlayerState();
        movable = true;

    }

        // Update is called once per frame
    // Update is called once per frame
    void Update()//단발적 입력: 업데이트함수
    {
        // 화면 위에 손가락이 없는지 확인
        if (Input.touchCount == 0)
        {
            isButtonPressed = false;
            isJumpButton = false;
            isLeftButton = false;
            isRightButton = false;
        }
        //////////////////////////////////////////
        if (!anim.GetBool("isFly") && SceneManager.GetActiveScene().name == Define.Scene.SnowBoss4.ToString())
        {
            //애니메이션: 계속 날아가는거로.
            anim.SetBool("isFly", true);
        }

        if (!movable) return;

        //Idle이면 중력스케일 복구
        if (player_state == PlayerState.Idle)
        {
            rigid.gravityScale = 2;
        }

        //무한점프
        if (SceneManager.GetActiveScene().name == Define.Scene.SnowBoss4.ToString())
        {
            if (Input.GetButton("Jump") && player_state != PlayerState.Damaged)
            {
                player_state = PlayerState.Fly;
                rigid.velocity = new Vector2(rigid.velocity.x, jump_power);
            }
        }

        //브레이크
        if (!isIce && Input.GetButtonUp("Horizontal"))
        {
            //normalized: 벡터크기를 1로 만든 상태. 방향구할 때 씀
            //방향에 속력을 0으로 
            rigid.velocity = new Vector2(rigid.velocity.normalized.x * 0.0000001f, rigid.velocity.y);
        }

        if (Input.GetButton("Horizontal") && player_state != PlayerState.Damaged)
        {
            //방향전환
            sprite_renderer.flipX = Input.GetAxisRaw("Horizontal") == -1;
        }

        
        // 화면 위에 손가락이 있는지 확인
        if (Input.touchCount > 0)
        {
            isButtonPressed = true;
        }

        //버튼 이동
        if (isButtonPressed)
        {

            if ((!ignoreJump) && isJumpButton )//y방향성이 없을 때 눌러야 함.
            {
                if (SceneManager.GetActiveScene().name == Define.Scene.SnowBoss4.ToString())
                {
                    if (audioSources != null)
                    {
                        audioSources[0].Play();
                    }
                    rigid.velocity = new Vector2(rigid.velocity.x, jump_power);
                }
                else if((rigid.velocity.normalized.y > -JUMP_CRITERIA && rigid.velocity.normalized.y < JUMP_CRITERIA )&& (player_state != PlayerState.Jump) && jump )
                {
                    if(audioSources!= null)
                    {
                        audioSources[0].Play();
                    }
                    rigid.velocity = new Vector2(rigid.velocity.x, jump_power);
                }
            }

            if (isLeftButton)
            {
                transform.position += Vector3.left * max_speed * Time.deltaTime;
            }
            if (isRightButton)
            {
                transform.position += Vector3.right * max_speed * Time.deltaTime;
            }
        }

        ////////////////////
        // 화면의 가로 크기의 절반 값을 구함
        float halfScreenWidth = Screen.width * 0.5f;

        // 터치가 하나 발생했는지 확인
        if (Input.touchCount == 1)
        {
            // 첫 번째 터치 정보 가져오기
            Touch touch = Input.GetTouch(0);

            //좌우키만 클릭중이면
            if (touch.position.x < halfScreenWidth)
            {
                isJumpButton = false;
            }
            else//점프만 클릭중이면
            {
                isLeftButton = false;
                isRightButton = false;
            }
        }


    }
    private void FixedUpdate()//물리 update
    {

        if (!movable) return;

        //레이캐스트(for 버튼점프 꾹 누를 시 천장붙음 방지)
        Debug.DrawRay(rigid.position, Vector3.up, new Color(0, 1, 0));
        RaycastHit2D headRayHit = Physics2D.Raycast(rigid.position, Vector2.up, 1f, LayerMask.GetMask("Platform"));
        //맞았다는 뜻
        if (headRayHit.collider != null)
        {
            ignoreJump = true;
        }
        else
        {
            ignoreJump = false;
        }


        //타고올라가기 방지
        Debug.DrawRay(rigid.position + new Vector2(0, -0.45f), Vector3.right, new Color(1, 0, 0));
        Debug.DrawRay(rigid.position + new Vector2(0, -0.45f), Vector3.left, new Color(1, 0, 0));
        RaycastHit2D platformRightRayHit = Physics2D.Raycast(rigid.position + new Vector2(0, -0.45f), Vector2.right, 0.5f, LayerMask.GetMask("Platform"));
        RaycastHit2D platformLeftRayHit = Physics2D.Raycast(rigid.position + new Vector2(0, -0.45f), Vector2.left, 0.5f, LayerMask.GetMask("Platform"));
        //맞았다는 뜻
        if (platformRightRayHit.collider != null || platformLeftRayHit.collider != null)
        {
            //평지가 아니면
            if (!(rigid.velocity.normalized.y > -JUMP_CRITERIA && rigid.velocity.normalized.y < JUMP_CRITERIA))
            {
                this.GetComponent<CapsuleCollider2D>().enabled = false;
                this.GetComponent<BoxCollider2D>().enabled = true;
            }
        }
        else
        {
            this.GetComponent<CapsuleCollider2D>().enabled = true;
            this.GetComponent<BoxCollider2D>().enabled = false;
        }


        //키 컨트롤로 움직이기
        horizontal = Input.GetAxisRaw("Horizontal");
        if (isIce)
        {
            if (Mathf.Abs(rigid.velocity.x) <= max_speed)
            {
                rigid.AddForce(Vector2.right * horizontal, ForceMode2D.Impulse);
            }
        }
        else
        {
            rigid.velocity = new Vector2(max_speed * horizontal, rigid.velocity.y);
        }

        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, Vector2.up, distance, whatIsLadder);

        //점프설정
        if (rigid.velocity.normalized.y <= -JUMP_CRITERIA || rigid.velocity.normalized.y >= JUMP_CRITERIA)
        {
            if (SceneManager.GetActiveScene().name != Define.Scene.SnowBoss4.ToString())
            {
                anim.SetBool("isJump", true);
                anim.SetBool("isWalk", false);
                if (player_state != PlayerState.Damaged)
                {
                    player_state = PlayerState.Jump;
                }
            }
        }
        else//y에 아무런 방향성이 없을 때
        {
            if (SceneManager.GetActiveScene().name != Define.Scene.SnowBoss4.ToString())
            {
                if (rigid.velocity.normalized.x != 0||isLeftButton||isRightButton)//x에 방향성이 있으면 걷기
                {
                    anim.SetBool("isJump", false);
                    anim.SetBool("isWalk", true);
                    if (player_state != PlayerState.Damaged)
                    {
                        player_state = PlayerState.Walk;
                    }
                }
                else//x에 방향성이 없으면 idle (위 if문이 true가 아니면 else)
                {

                    anim.SetBool("isJump", false);
                    anim.SetBool("isWalk", false);
                    if (player_state != PlayerState.Damaged)
                    {
                        player_state = PlayerState.Idle;
                    }
                }
            }
        }
        //점프키누르면
        if (rigid.velocity.normalized.y > -JUMP_CRITERIA && rigid.velocity.normalized.y < JUMP_CRITERIA)//y방향성이 없을 때 눌러야 함.
        {
            if ((!ignoreJump)&&(player_state!=PlayerState.Jump)&&(Input.GetButton("Jump")) && jump && SceneManager.GetActiveScene().name != Define.Scene.SnowBoss4.ToString())
            {
                if (audioSources != null)
                {
                    audioSources[0].Play();
                }
                rigid.velocity = new Vector2(rigid.velocity.x, jump_power);
            }
        }




        if (hitInfo.collider != null)
        {
            jump = false;
            if ((Input.GetButton("Jump") || Input.GetKey(KeyCode.Space)))
            {
                isClimbing = true;
                rigid.velocity = new Vector2(rigid.velocity.x, ladder_speed);
            }
            else
            {
                if (isLeftButton || isRightButton)
                {
                    isClimbing = false;
                    jump = true;
                }
            }
        }
        else { jump = true; }
        
        if (isClimbing == true && hitInfo.collider != null)
        {
            inputVertical = Convert.ToSingle(Input.GetButton("Jump") || Input.GetKey(KeyCode.Space));
            rigid.velocity = new Vector2(rigid.velocity.x, inputVertical * ladder_speed);
            rigid.gravityScale = 0;
        }
        else
        {
            if (rigid.velocity.normalized.y <= -JUMP_CRITERIA)//낙하하면 훅 떨어지게
            {
            
                    rigid.gravityScale = 3;
            }      
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            rigid.gravityScale = 2;//땅에 착지하면 중력스케일 원상복구
        }
        if (collision.gameObject.CompareTag("Enemy"))
        {
            if (collision.gameObject.name == "t_FakeItem")
            {
                PlayerPrefs.SetString("TransparentWall", "False");
            }
            Die(collision.transform.position);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Die(collision.transform.position);
        }
        else if (collision.gameObject.CompareTag("BurnEnemy"))
        {
            StartCoroutine(BurnAndDie());
            Die(collision.transform.position);
        }
        else if (collision.gameObject.CompareTag("Boss"))
        {
            Die(collision.transform.position);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("Flag"))
        {
            //리스폰 위치를 해당 Flag 위치로 재설정
            Vector3 flagPosition = collision.gameObject.transform.position;
            SaveLoad.GetComponent<SaveLoad>().SaveRespawn("respawn", flagPosition);
        }
        else if (collision.gameObject.CompareTag("Item"))
        {
            //진짜 아이템 먹었을 때 animation 바꿈
            anim.SetBool("isItemGet", true);
            collision.gameObject.SetActive(false);
        }
        else if (collision.gameObject.CompareTag("Border"))
        {
            if (audioSources != null)
            {
                audioSources[1].Play();
            }
            
            player_state = PlayerState.Damaged;
            this.gameObject.layer = 7;
            isBorder = true;
            GameManager.instance.OnPlayerDead();
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!isIce && collision.gameObject.CompareTag("Ice"))
        {
            isIce = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ice"))
        {
            isIce = false;
        }
    }

    public void Die(Vector2 targetPos)
    {
        if (!invincibility)
        {
            onDamaged(targetPos);
            //게임 매니저의 게임오버 처리 실행
            GameManager.instance.OnPlayerDead();
        }
    }

    public void onDamaged(Vector2 targetPos)
    {

        if (audioSources != null)
        {
            audioSources[1].Play();
        }
        
        //맞은 상태
        player_state = PlayerState.Damaged;
        //레이어변경
        this.gameObject.layer = 7;
        //투명하게 바꾸기
        sprite_renderer.color = new Color(1, 1, 1, 0.4f);
        //리액션
        int dirc = transform.position.x - targetPos.x > 0 ? 1 : -1;
        if (!isBorder)
        {
            rigid.AddForce(new Vector2(dirc, 1) * 7, ForceMode2D.Impulse);
        }
    }

    //화면밖으로 나감: 죽음
    private void OnBecameInvisible()
    {

        if (audioSources != null)
        {
            audioSources[1].Play();
        }
      
        player_state = PlayerState.Damaged;

        this.gameObject.layer = 7;
        GameManager.instance.OnPlayerDead();
        this.gameObject.SetActive(false);

    }
  

    IEnumerator BurnAndDie()
    {
        player_state = PlayerState.Damaged;
        this.gameObject.layer = 7;
        anim.SetBool("isExplosion", true);
        anim.SetBool("isJump", false);
        yield return new WaitForSeconds(1.0f);
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

    public void ChangeSprites()
    {
        string realItem = PlayerPrefs.GetString("RealItem");
        switch (realItem)
        {
            case "SnowWhite":
                anim.runtimeAnimatorController =
                    (RuntimeAnimatorController)RuntimeAnimatorController.Instantiate(Resources.Load("Anim/Player_RealItem_SnowWhite",
                        typeof(RuntimeAnimatorController)));
                break;
            default:
                anim.runtimeAnimatorController =
                    (RuntimeAnimatorController)RuntimeAnimatorController.Instantiate(Resources.Load("Anim/Player_Default",
                        typeof(RuntimeAnimatorController)));
                break;
        }
    }
}

