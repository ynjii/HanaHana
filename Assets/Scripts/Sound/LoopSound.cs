using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoopSound : MonoBehaviour
{
    public AudioSource soundSource; // 사운드 재생을 위한 AudioSource 변수
    public AudioClip collisionSound; // 충돌 시 재생할 사운드 클립

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // 플레이어와 충돌하면 효과음을 재생
            if (soundSource != null && collisionSound != null)
            {
                soundSource.PlayOneShot(collisionSound);
            }
        }
    }
}
