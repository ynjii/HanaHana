using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TransparentIntoOpaque : MonoBehaviour
{
    private TilemapRenderer tile_map_renderer;
    private bool is_triggerd=false;
    private Color my_color;
    private bool is_expired=false;
    // Start is called before the first frame update
    void Start()
    {
        tile_map_renderer = GetComponent<TilemapRenderer>();
        tile_map_renderer.material.color = new Color(1f, 1f, 1f, 0f);
        my_color = new Color(1f, 1f, 1f, 0f);
    }

    private void Update()
    {
        if (is_triggerd&&!is_expired)
        {
            my_color.a += 0.05f; // 알파 값 조정

            // 알파 값이 1을 넘어가면 1로 고정
            if (my_color.a > 1f)
            {
                my_color.a = 1f;
                is_expired = true;
            }
            // 오브젝트의 머티리얼 또는 스프라이트 렌더러의 색상 설정
            GetComponent<Renderer>().material.color = my_color;            
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            is_triggerd = true;
        }
    }
}
