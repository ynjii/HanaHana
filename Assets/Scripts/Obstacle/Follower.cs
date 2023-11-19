using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

//player 졸졸 따라다님. 펫 기능 생각하면 됨.
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
            this.gameObject.layer = 3; //layer을 player로 바꿈 player랑 똑같이 collider 조건 맞추려고
            playerScript = collision.gameObject.GetComponent<Player>();
        }
        else if (isTriggered && collision.gameObject.CompareTag("Enemy"))
        {
            playerScript.onDamaged(collision.transform.position);
            //게임 매니저의 게임오버 처리 실행
            GameManager.instance.OnPlayerDead();
        }
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

    //playerPos를 차곡차곡 스택에 담는다.
    void Watch()
    {
        if (!playerPos.Contains(player.position))
            playerPos.Enqueue(player.position);
        if (playerPos.Count > followDelay)
            followPos = playerPos.Dequeue();
    }

    //player 따라감.
    void Follow()
    {
        transform.position = followPos;
    }

}
