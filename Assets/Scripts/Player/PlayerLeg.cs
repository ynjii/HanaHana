using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLeg : MonoBehaviour
{
    public Animator player_anim;
  
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Platform")
        {
            player_anim.SetBool("isJump", false);
        }
        if (collision.gameObject.tag == "Enemy")
        {
            //레이어변경
            this.gameObject.layer = 7;
        }

    }
}
