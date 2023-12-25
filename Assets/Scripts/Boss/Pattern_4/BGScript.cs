using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGScript : MonoBehaviour
{
    public float speed = 2;
    SpriteRenderer spr;
    // Start is called before the first frame update
    void Start()
    {
        spr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.left * Time.deltaTime * speed;
        Vector3 pos = transform.position;
        if (pos.x + spr.bounds.size.x / 2 < -8)
        {
            float size = spr.bounds.size.x * 2;//다시 스크롤 될 위치로 이동. (대기조)
            pos.x += size;// x에 대입
            transform.position = pos;//position에 대입함으로써 완전히 이동된 상태.
        }
    }
}