using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{
    PhysicsMaterial2D physicsMaterial; 
    private void Start()
    {
        // Physics Material 2D ����
        physicsMaterial = new PhysicsMaterial2D();
        physicsMaterial.friction = 0f; // ������ ����
        physicsMaterial.bounciness = 0.2f; // ź�� ��� ����

        // Collider2D�� Physics Material 2D �Ҵ�
        Collider2D collider = GetComponent<Collider2D>();
        collider.sharedMaterial = physicsMaterial;
    }
}
