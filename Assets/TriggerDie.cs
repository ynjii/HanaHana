using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDie : MonoBehaviour
{
    public  GameObject player;
    private void OnTriggerEnter2D(Collider2D collision){
        if(collision.gameObject.CompareTag("Player"))
        {     
            //게임 매니저의 게임오버 처리 실행
            GameManager.instance.OnPlayerDead();
            player.SetActive(false);
        }
    }
}
