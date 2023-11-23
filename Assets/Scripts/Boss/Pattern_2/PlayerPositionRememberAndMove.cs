using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//player에 있는 위치를 기억했다가 1초후 player 있었던 위치로 달려가는 코드다. 이 기믹은 자주 써먹을 수 있을 것 같다.
public class PlayerPositionRememberAndMove : MonoBehaviour
{
    private float accumTimeAfterUpdate;
    private float moveSpeed = 20f;
    private Vector3 myPosition;
    private Vector3 fixedPos;

    private bool isMoving = false;

    private GameObject player; //주인공 오브젝트를 저장하는 변수

    private void Start()
    {
        myPosition = transform.position; //현재 obstacle의 위치 (이 스크립트가 부착되어 있는 object)
        player = GameObject.FindWithTag("Player");
        fixedPos = player.transform.position;
        Invoke("startMoving", 1f);
    }

    private void startMoving()
    {
        isMoving = true;
    }

    private void Update()
    {
        if (isMoving)
        {
            Vector3 vec3Dir = fixedPos - myPosition;
            vec3Dir.Normalize();
            transform.position = transform.position + vec3Dir * moveSpeed * Time.deltaTime; ;
        }
    }
}
