using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{
    PhysicsMaterial2D physicsMaterial; 
    private void Start()
    {
        // Physics Material 2D 생성
        physicsMaterial = new PhysicsMaterial2D();
        physicsMaterial.friction = 0f; // 마찰력 설정
        physicsMaterial.bounciness = 0.2f; // 탄성 계수 설정

        // Collider2D에 Physics Material 2D 할당
        Collider2D collider = GetComponent<Collider2D>();
        collider.sharedMaterial = physicsMaterial;
    }
}
