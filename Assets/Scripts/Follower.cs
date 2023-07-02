using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follower : MonoBehaviour
{
    public Vector3 followPos;
    [SerializeField]
    private int followDelay;
    public Transform player;
    private Queue<Vector3> playerPos;
    private bool isTriggered = false;
    
    // Start is called before the first frame update
    void OnTriggerEnter2D(Collider2D collision) {
        if(collision.gameObject.CompareTag("Player"))
        {
            isTriggered = true;   
        }
        if(collision.gameObject.CompareTag("Enemy")){
            //여기서 부딪히면 같이 죽게...
        }
    }

    void Awake()
    {
        playerPos = new Queue<Vector3>();
    }
    // Update is called once per frame
    void Update()
    {
        if(isTriggered){
            Watch();
            Follow();
        }   
    }
    
    void Watch()
    {
        if(!playerPos.Contains(player.position))
            playerPos.Enqueue(player.position);
        if(playerPos.Count > followDelay)
            followPos = playerPos.Dequeue();
    }

    void Follow()
    {
        transform.position = followPos;
    }

}
