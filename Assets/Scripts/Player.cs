using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour
{
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
    void Update()//´Ü¹ßÀû ÀÔ·Â: ¾÷µ¥ÀÌÆ®ÇÔ¼ö
    {
        //Á¡ÇÁ
        if ((Input.GetButtonDown("Jump")&&!anim.GetBool("isJump")))
        {
            rigid.AddForce(Vector2.up * jump_power, ForceMode2D.Impulse);
            anim.SetBool("isJump", true);
        }
        //ºê·¹ÀÌÅ©
        if (Input.GetButtonUp("Horizontal"))
        {
            //normalized: º¤ÅÍÅ©±â¸¦ 1·Î ¸¸µç »óÅÂ. ¹æÇâ±¸ÇÒ ¶§ ¾¸
            //¹æÇâ¿¡ ¼Ó·ÂÀ» 0À¸·Î 
            rigid.velocity = new Vector2(rigid.velocity.normalized.x * 0.0000001f, rigid.velocity.y);
        }

        //¹æÇâÀüÈ¯
        if (Input.GetButton("Horizontal"))
            sprite_renderer.flipX = Input.GetAxisRaw("Horizontal") == -1;

        //¾Ö´Ï¸ŞÀÌ¼Ç
        if (rigid.velocity.normalized.x == 0)
        {
            anim.SetBool("isWalk", false);
        }
        else
        {
            anim.SetBool("isWalk", true);
        }
    }
    private void FixedUpdate()//¹°¸® update
    {
        //Å° ÄÁÆ®·Ñ·Î ¿òÁ÷ÀÌ±â
        float h = Input.GetAxisRaw("Horizontal");
        rigid.AddForce(Vector2.right * h, ForceMode2D.Impulse);
        
        if (rigid.velocity.x> max_speed)//¿À¸¥ÂÊ
        {
            rigid.velocity = new Vector2(max_speed, rigid.velocity.y);
        }
        else if (rigid.velocity.x < max_speed*(-1))//¿ŞÂÊ
        {
            rigid.velocity = new Vector2(max_speed*(-1), rigid.velocity.y);
        }

        //¹öÆ° ÀÌµ¿
        if (isButtonPressed)
        {
            // ¹öÆ°À» °è¼Ó ´©¸£°í ÀÖÀ» ¶§ È£ÃâÇÒ ¸Ş¼Òµå¸¦ ¿©±â¿¡ ÀÛ¼º.
            if (isJumpButton)
            {
                //Á¡ÇÁ
                if (!anim.GetBool("isJump"))
                {
                    rigid.AddForce(Vector2.up * jump_power, ForceMode2D.Impulse);
                    anim.SetBool("isJump", true);
                }
            }
            if (isLeftButton)
            {
                rigid.AddForce(Vector2.right * -1, ForceMode2D.Impulse);

                if (rigid.velocity.x < max_speed * (-1))//?ï¿½ï¿½ï¿??
                {
                    rigid.velocity = new Vector2(max_speed * (-1), rigid.velocity.y);
                }
            }
            if (isRightButton)
            {
                rigid.AddForce(Vector2.right * 1, ForceMode2D.Impulse);

                if (rigid.velocity.x > max_speed)//?ï¿½ï¿½ë¥¸ìª½
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
            //ê²Œì„ ë§¤ë‹ˆì €ì˜ ê²Œì„ì˜¤ë²„ ì²˜ë¦¬ ì‹¤í–‰
            GameManager.instance.OnPlayerDead();
        }

        if(collision.gameObject.tag=="Flag")
        {
            //ë¦¬ìŠ¤í° ìœ„ì¹˜ë¥¼ í•´ë‹¹ Flag ìœ„ì¹˜ë¡œ ì¬ì„¤ì •
            Vector3 flagPosition=collision.gameObject.transform.position;
            PlayerRespawn playerRespawn = GetComponent<PlayerRespawn>();
            playerRespawn.SetRespawnPoint(flagPosition);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision){
        if (collision.gameObject.tag == "Item")
        {
            //ÁøÂ¥ ¾ÆÀÌÅÛ ¸Ô¾úÀ» ¶§ animation ¹Ù²Ş
            anim.SetBool("isItemGet", true);
            collision.gameObject.SetActive(false);
        }
    }

    public void onDamaged(Vector2 targetPos)
    {
        //·¹ÀÌ¾î ¹Ù²Ù±â
        gameObject.layer = 7;

        //Åõ¸íÇÏ°Ô ¹Ù²Ù±â
        sprite_renderer.color = new Color(1, 1, 1, 0.4f);

        //¸®¾×¼Ç
        int dirc = transform.position.x - targetPos.x > 0 ? 1 : -1;
        rigid.AddForce(new Vector2(dirc, 1) * 7, ForceMode2D.Impulse);

    }

    //È­¸é¹ÛÀ¸·Î ³ª°¨: Á×À½
    private void OnBecameInvisible()
    {
        this.gameObject.SetActive(false);
    }


    //¹öÆ°À» ´­·¶´ÂÁö ¶Ã´ÂÁö
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
    
    //¹öÆ° ¹üÀ§¿¡¼­ ³ª°¬À¸¸é false
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
    //¹öÆ° ¹üÀ§ µé¾î¿À¸é true
    public void jumpButtonEnter()
    {
            isJumpButton = true;
    }
    public void leftButtonEnter()
    {
            isLeftButton = true;
    }
    public void rightButtonEnter()
    {
            isRightButton = true;
    }
    //¾Æ·¡ 3°³ ¸Ş¼Òµå : ¹öÆ°À» ²Ú ´©¸£°í ÀÖ´ÂÁö Ã¼Å©
    //¹öÆ°À» ´©¸£°í ÀÖ´Â µ¿¾È Ã³¸®ÇÏ´Â µ¿ÀÛ.
    public void OnPointerDown()
    {
        isButtonPressed = true;
    }

    //¹öÆ° ¶¼¸é false ÀüÈ¯
    public void OnPointerUp()
    {
        isButtonPressed = false;
    }
    //¹öÆ° ¹üÀ§ ³ª°¥ ¶§ 
    public void OnPointerExit()
    {
        isButtonPressed = false;        
    }
    public void OnPointerEnter()
    {
        isButtonPressed = true;
    }
}

