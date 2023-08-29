using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSound : MonoBehaviour
{
    [SerializeField]
    private bool isCol=false;
    private AudioSource audio;
    [SerializeField]
    private bool isMoving = false; 

    [SerializeField]
    private float waitingTime = 0f;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isCol&&collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(SetIsmoving(true));
        }
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((!isCol)&&collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(SetIsmoving(true));
        }
    }

    private void Update()
    {
        if (audio != null && isMoving)
        {
            if (!audio.loop)
            {
                audio.Play();
                isMoving = false;
            }
        }
    }
    // Start is called before the first frame update
    void Awake()
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

    IEnumerator SetIsmoving(bool n)
    {
        yield return new WaitForSeconds(waitingTime);
        this.isMoving = n;
    }
}
