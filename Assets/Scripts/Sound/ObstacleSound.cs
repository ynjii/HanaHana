using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSound : MonoBehaviour
{
    [SerializeField]
    private bool isCol=false;
    private AudioSource audio;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isCol&&collision.gameObject.CompareTag("Player"))
        {
            if (audio != null)
            {
                audio.Play();
            }
        }
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((!isCol)&&collision.gameObject.CompareTag("Player"))
        {
            if (audio != null)
            {
                Debug.Log(audio.clip);
                audio.Play();
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        audio= GetComponent<AudioSource>();
    }

    private void OnBecameVisible()
    {
        if (audio != null)
        {
            audio.enabled = true;
        }
    }

    private void OnBecameInvisible()
    {
        if (audio != null)
        {
            audio.enabled=false;
        }
    }

   
}
