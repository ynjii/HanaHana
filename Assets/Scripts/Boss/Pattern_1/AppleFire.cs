using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppleFire : MonoBehaviour
{
    public float speed = 10f;  // 미사일 이동 속도
    private float lifetime = 5f;  // 미사일 수명 (초)
    private float spawnTime;  // 미사일 생성 시간

    void Start()
    {
        spawnTime = Time.time;  // 현재 시간을 저장
    }

    void Update()
    {
        // 일정한 속도로 위쪽으로 이동
        transform.position += new Vector3(0, speed * Time.deltaTime, 0);

        // 미사일의 수명이 다 되면 삭제
        if (Time.time - spawnTime > lifetime)
        {
            Destroy(gameObject);
        }
    }
}
