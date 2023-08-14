using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Boss : MonoBehaviour
{
    private float time=0;
    public SpriteRenderer mirror_renderer;


    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (time>=0.15f)
            mirror_renderer.color= new Color(1,1,1,0.7f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("bullet"))
        {
            mirror_renderer.color = new Color(1, 0.54f, 0.54f, 0.77f);
            time = 0f;
        }
    }
}
