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

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        sprite_renderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        max_speed = 3;
        jump_power = 8;
    }

    // Update is called once per frame
    void Update()//�ܹ��� �Է�: ������Ʈ�Լ�
    {
        //����
        if ((Input.GetButtonDown("Jump")&&!anim.GetBool("isJump")))
        { 
            rigid.AddForce(Vector2.up * jump_power, ForceMode2D.Impulse);
            anim.SetBool("isJump", true);
        }
        //�극��ũ
        if (Input.GetButtonUp("Horizontal"))
        {
            //normalized: ����ũ�⸦ 1�� ���� ����. ���ⱸ�� �� ��
            //���⿡ �ӷ��� 0���� 
            rigid.velocity = new Vector2(rigid.velocity.normalized.x * 0.0000001f, rigid.velocity.y);
        }

        //������ȯ
        if (Input.GetButton("Horizontal"))
            sprite_renderer.flipX = Input.GetAxisRaw("Horizontal") == -1;

        //�ִϸ��̼�
        if (rigid.velocity.normalized.x == 0)
        {
            anim.SetBool("isWalk", false);
        }
        else
        {
            anim.SetBool("isWalk", true);
        }
    }
    private void FixedUpdate()//���� update
    {
        //Ű ��Ʈ�ѷ� �����̱�
        float h = Input.GetAxisRaw("Horizontal");
        rigid.AddForce(Vector2.right*h,ForceMode2D.Impulse);
        if(rigid.velocity.x> max_speed)//������
        {
            rigid.velocity = new Vector2(max_speed, rigid.velocity.y);
        }
        else if (rigid.velocity.x < max_speed*(-1))//����
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
            //게임 매니저의 게임오버 처리 실행
            GameManager.instance.OnPlayerDead();
        }

        if(collision.gameObject.tag=="Flag")
        {
            //리스폰 위치를 해당 Flag 위치로 재설정
            Vector3 flagPosition=collision.gameObject.transform.position;
            PlayerRespawn playerRespawn = GetComponent<PlayerRespawn>();
            playerRespawn.SetRespawnPoint(flagPosition);
        }
    }

    void onDamaged(Vector2 targetPos)
    {
        //���̾� �ٲٱ�
        gameObject.layer = 7;

        //�����ϰ� �ٲٱ�
        sprite_renderer.color = new Color(1, 1, 1, 0.4f);

        //���׼�
        int dirc = transform.position.x - targetPos.x > 0 ? 1 : -1;
        rigid.AddForce(new Vector2(dirc, 1) * 7, ForceMode2D.Impulse);

    }
    private void OnBecameInvisible()
    {
        this.gameObject.SetActive(false);
    }
   

}
