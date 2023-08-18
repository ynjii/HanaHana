using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGScroller : MonoBehaviour
{
    private MeshRenderer m_renderer;
    private float offset;
    [SerializeField]
    private float speed;
    private void Awake()
    {
        m_renderer=GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        offset+=Time.deltaTime*speed;
        m_renderer.material.mainTextureOffset = new Vector2(offset, 0);
    }
}
