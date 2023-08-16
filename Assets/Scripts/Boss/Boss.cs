using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Boss : MonoBehaviour
{
    private float time=0;
    public SpriteRenderer mirror_renderer;
    [SerializeField]
    public float boss_hp;
    private GameObject bullet;
    private Fire bullet_script;
    private void Awake()
    {
        bullet = GameObject.FindWithTag("bullet").GetComponent<Launch_Fire>().fire;
        bullet_script=bullet.GetComponent<Fire>();
    }

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
            boss_hp -= bullet_script.bullet_damage;
            time = 0f;
        }

    }
}
