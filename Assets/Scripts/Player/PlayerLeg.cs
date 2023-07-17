using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLeg : MonoBehaviour
{
    public Animator player_anim;
    PlayerHead player_head;

    private void Awake()
    {
        player_head = GetComponentInChildren<PlayerHead>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Platform")
        {
            player_anim.SetBool("isJump", false);
            player_head.ignore_Input = false;
        }
        if (collision.gameObject.tag == "Enemy")
        {
            //레이어변경
            this.gameObject.layer = 7;
        }

    }
}
