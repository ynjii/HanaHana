using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TilemapSound : MonoBehaviour
{
    public AudioSource soundSource; // 사운드 재생을 위한 AudioSource 변수
    public AudioClip collisionSound; // 충돌 시 재생할 사운드 클립

    private bool hasPlayed = false; // 이미 사운드를 재생했는지 확인하는 변수

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!hasPlayed && collision.CompareTag("Player"))
        {
            // 플레이어와 충돌하면 효과음을 재생하고 사운드 플래그를 true로 변경
            if (soundSource != null && collisionSound != null)
            {
                soundSource.PlayOneShot(collisionSound);
                hasPlayed = true;
            }
        }
    }
}
