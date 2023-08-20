using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Missile : MonoBehaviour
{
    public float speed = 5f;
    public float inertiaStrength = 1.5f; // 관성 강도 조절
    public Transform player;
    private Rigidbody2D rigid;
    [SerializeField]
    private bool has_rigid=false;
    [SerializeField]
    private bool is_prefab = false;
    [SerializeField]
    private bool follow_once = false;
    private void Awake()
    {
        if (is_prefab)
        {
            player = GameObject.FindWithTag("Player").GetComponent<Transform>();
        }
        if (has_rigid)
        {
            rigid=GetComponent<Rigidbody2D>();
        }
        if (has_rigid)
        {
            inertiaMissle();
        }
    }
    void Update()
    {
        if (!follow_once)
        {
            Vector3 direction = player.position - transform.position;
            direction.Normalize();

            transform.position += direction * speed * Time.deltaTime;
        }
    }

    private void inertiaMissle()
    {
        if (player == null)
            return;

        Vector2 toPlayer = player.position - transform.position;
        Vector2 desiredDirection = toPlayer.normalized;

        // 현재 속도와 관성을 고려하여 이동 방향 결정
        Vector2 newVelocity = rigid.velocity + desiredDirection * (speed * Time.fixedDeltaTime);
        newVelocity *= inertiaStrength;

        rigid.velocity = newVelocity;
    }

}
