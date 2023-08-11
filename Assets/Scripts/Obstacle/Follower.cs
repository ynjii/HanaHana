using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class Follower : MonoBehaviour
{
    public Vector3 followPos;

    [SerializeField]
    private int followDelay;

    public Transform player;
    private Queue<Vector3> playerPos;
    private bool isTriggered = false;
    private Player playerScript;
    // Start is called before the first frame update
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isTriggered = true;
            this.gameObject.layer = 3;
            playerScript = collision.gameObject.GetComponent<Player>();
        }
        if (isTriggered && collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("부딪힘");
            playerScript.onDamaged(collision.transform.position);
            //게임 매니저의 게임오버 처리 실행
            GameManager.instance.OnPlayerDead();
        }
        Debug.Log(collision);
    }

    void Awake()
    {
        playerPos = new Queue<Vector3>();
    }
    // Update is called once per frame
    void Update()
    {
        if (isTriggered)
        {
            Watch();
            Follow();
        }
    }

    void Watch()
    {
        if (!playerPos.Contains(player.position))
            playerPos.Enqueue(player.position);
        if (playerPos.Count > followDelay)
            followPos = playerPos.Dequeue();
    }

    void Follow()
    {
        transform.position = followPos;
    }

}
