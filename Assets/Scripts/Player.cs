using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour
{
    [SerializeField] 
    private Sprite itemPlayer; //¾ÆÀÌÅÛ ¾òÀº ÈÄ
    [SerializeField]
    private float max_speed;
    [SerializeField]
    private float jump_power;
    Rigidbody2D rigid;
    SpriteRenderer sprite_renderer;
    Animator anim;
    public bool isJumpButton=false;
    public bool isLeftButton = false;
    public bool isRightButton = false;
    public bool isButtonPressed = false;

   

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        sprite_renderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        max_speed = 3;
        jump_power = 8;
    }

    // Update is called once per frame
    void Update()//?‹¨ë°œì  ?…? ¥: ?—…?°?´?Š¸?•¨?ˆ˜
    {
        //? ?”„
        if ((Input.GetButtonDown("Jump")&&!anim.GetBool("isJump")))
        {
            rigid.AddForce(Vector2.up * jump_power, ForceMode2D.Impulse);
            anim.SetBool("isJump", true);
        }
        //ë¸Œë ˆ?´?¬
        if (Input.GetButtonUp("Horizontal"))
        {
            //normalized: ë²¡í„°?¬ê¸°ë?? 1ë¡? ë§Œë“  ?ƒ?ƒœ. ë°©í–¥êµ¬í•  ?•Œ ???
            //ë°©í–¥?— ?†? ¥?„ 0?œ¼ë¡? 
            rigid.velocity = new Vector2(rigid.velocity.normalized.x * 0.0000001f, rigid.velocity.y);
        }

        //ë°©í–¥? „?™˜
        if (Input.GetButton("Horizontal"))
            sprite_renderer.flipX = Input.GetAxisRaw("Horizontal") == -1;

        //?• ?‹ˆë©”ì´?…˜
        if (rigid.velocity.normalized.x == 0)
        {
            anim.SetBool("isWalk", false);
        }
        else
        {
            anim.SetBool("isWalk", true);
        }
    }
    private void FixedUpdate()//ë¬¼ë¦¬ update
    {
        //?‚¤ ì»¨íŠ¸ë¡¤ë¡œ ???ì§ì´ê¸?
        float h = Input.GetAxisRaw("Horizontal");
        rigid.AddForce(Vector2.right * h, ForceMode2D.Impulse);
      
        if (rigid.velocity.x> max_speed)//?˜¤ë¥¸ìª½
        {
            rigid.velocity = new Vector2(max_speed, rigid.velocity.y);
        }
        else if (rigid.velocity.x < max_speed*(-1))//?™¼ìª?
        {
            rigid.velocity = new Vector2(max_speed*(-1), rigid.velocity.y);
        }

        //ë²„íŠ¼ ?´?™
        if (isButtonPressed)
        {
            // ë²„íŠ¼?„ ê³„ì† ?ˆ„ë¥´ê³  ?ˆ?„ ?•Œ ?˜¸ì¶œí•  ë©”ì†Œ?“œë¥? ?—¬ê¸°ì— ?‘?„±.
            if (isJumpButton)
            {
                //? ?”„
                if (!anim.GetBool("isJump"))
                {
                    rigid.AddForce(Vector2.up * jump_power, ForceMode2D.Impulse);
                    anim.SetBool("isJump", true);
                }
            }
            if (isLeftButton)
            {
                rigid.AddForce(Vector2.right * -1, ForceMode2D.Impulse);

                if (rigid.velocity.x < max_speed * (-1))//?™¼ìª?
                {
                    rigid.velocity = new Vector2(max_speed * (-1), rigid.velocity.y);
                }
            }
            if (isRightButton)
            {
                rigid.AddForce(Vector2.right * 1, ForceMode2D.Impulse);

                if (rigid.velocity.x > max_speed)//?˜¤ë¥¸ìª½
                {
                    rigid.velocity = new Vector2(max_speed, rigid.velocity.y);
                }
            }
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
    }

    private void OnTriggerEnter2D(Collider2D collision){
        if (collision.gameObject.tag == "Item")
        {
            //¾ÆÀÌÅÛ ¾òÀº ÈÄ ¾Ö´Ï¸ŞÀÌ¼ÇÀÌ µû·Î ÀÖ¾î¾ßÇÒµí
            anim.SetBool("isItemGet", true);
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

    public void onDamaged(Vector2 targetPos)
    {
        //? ˆ?´?–´ ë°”ê¾¸ê¸?
        gameObject.layer = 7;

        //?ˆ¬ëª…í•˜ê²? ë°”ê¾¸ê¸?
        sprite_renderer.color = new Color(1, 1, 1, 0.4f);

        //ë¦¬ì•¡?…˜
        int dirc = transform.position.x - targetPos.x > 0 ? 1 : -1;
        rigid.AddForce(new Vector2(dirc, 1) * 7, ForceMode2D.Impulse);

    }

    //?™”ë©´ë°–?œ¼ë¡? ?‚˜ê°?: ì£½ìŒ
    private void OnBecameInvisible()
    {
        this.gameObject.SetActive(false);
    }


    //ë²„íŠ¼?„ ?ˆŒ????Š”ì§? ?—?Š”ì§?
    public void jumpButtonDown()
    {
        isJumpButton = true;
    }
    public void jumpButtonUp()
    {
        isJumpButton = false;
    }
    public void leftButtonDown()
    {
        isLeftButton = true;
    }
    public void leftButtonUp()
    {
        isLeftButton = false;
    }
    public void rightButtonDown()
    {
        isRightButton = true;
    }
    public void rightButtonUp()
    {
        isRightButton = false;
    }
    
    //ë²„íŠ¼ ë²”ìœ„?—?„œ ?‚˜ê°”ìœ¼ë©? false
    public void jumpButtonExit()
    {
        isJumpButton= false;
    }
    public void leftButtonExit()
    {
        isLeftButton = false;
    }
    public void rightButtonExit()
    {
        isRightButton = false;
    }
    //ë²„íŠ¼ ë²”ìœ„ ?“¤?–´?˜¤ë©? true
    public void jumpButtonEnter()
    {
        if(isButtonPressed)
            isJumpButton = true;
    }
    public void leftButtonEnter()
    {
        if (isButtonPressed)
            isLeftButton = true;
    }
    public void rightButtonEnter()
    {
        if (isButtonPressed)
            isRightButton = true;
    }
    //?•„?˜ 3ê°? ë©”ì†Œ?“œ : ë²„íŠ¼?„ ê¾? ?ˆ„ë¥´ê³  ?ˆ?Š”ì§? ì²´í¬
    //ë²„íŠ¼?„ ?ˆ„ë¥´ê³  ?ˆ?Š” ?™?•ˆ ì²˜ë¦¬?•˜?Š” ?™?‘.
    public void OnPointerDown()
    {
        isButtonPressed = true;
    }

    //ë²„íŠ¼ ?–¼ë©? false ? „?™˜
    public void OnPointerUp()
    {
        isButtonPressed = false;
    }
    //ë²„íŠ¼ ë²”ìœ„ ?‚˜ê°? ?•Œ 
    public void OnPointerExit()
    {
        isButtonPressed = false;        
    }
    public void OnPointerEnter()
    {
        isButtonPressed = true;
    }
}

