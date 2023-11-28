using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 배경스크롤.
/// [전유진] [오후 9:37] https://www.youtube.com/watch?app=desktop&v=asraLkuR3Jg
/// 똑같이 따라침
/// 배경스크롤 사용법 알고싶으면 위에 영상 보기
/// </summary>
public class BGScroller : MonoBehaviour
{
    private MeshRenderer m_renderer;
    private float offset;
    [SerializeField]
    private float speed;
    private void Awake()
    {
        m_renderer = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        offset += Time.deltaTime * speed;
        m_renderer.material.mainTextureOffset = new Vector2(offset, 0);
    }
}
