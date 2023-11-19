using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//얘도 주인공 따라다니는데 follower랑은 살짝 다름.  follower는 player가 간 길을 그대로 따라가고 얘는 말그대로 player가 목표
public class Missile : MonoBehaviour
{
    public float speed = 5f;
    public float inertiaStrength = 1.5f; // ���� ���� ����
    public Transform player;
    private Rigidbody2D rigid;

    [SerializeField]
    private Vector3 targetPos = new Vector3(0, 0, 0);

    //이거는 유진님이 만든거라 잘 모르겠습니다....
    [SerializeField]
    private bool has_rigid = false;
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
            rigid = GetComponent<Rigidbody2D>();
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
            if (player.position != null)
            {
                Vector3 direction = (player.position + targetPos) - transform.position;
                direction.Normalize();

                transform.position += direction * speed * Time.deltaTime;
            }
        }
    }

    private void inertiaMissle()
    {
        if (player == null)
            return;

        Vector2 toPlayer = player.position - transform.position;
        Vector2 desiredDirection = toPlayer.normalized;

        // ���� �ӵ��� ������ �����Ͽ� �̵� ���� ����
        Vector2 newVelocity = rigid.velocity + desiredDirection * (speed * Time.fixedDeltaTime);
        newVelocity *= inertiaStrength;

        rigid.velocity = newVelocity;
    }

}
