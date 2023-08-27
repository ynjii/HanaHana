using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuntoPlayer : MonoBehaviour
{
    private float accumTimeAfterUpdate;
    private float moveSpeed = 4f;
    private Vector3 myPosition;

    private bool isMoving = false;

    [SerializeField] private Transform playerPos;
    private Vector3 fixedPos;

    private void Start()
    {
        myPosition = transform.position;
        fixedPos = playerPos.position;
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
            // PlayerPos가 아닌 playerPos를 사용해야 합니다.
            Vector3 vec3Dir = playerPos.position - myPosition;
            vec3Dir.Normalize();
            transform.position = transform.position + vec3Dir * moveSpeed * Time.deltaTime; ;
        }
    }
}
