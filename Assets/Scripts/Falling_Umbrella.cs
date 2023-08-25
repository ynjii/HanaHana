using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Falling_Umbrella : MonoBehaviour
{
    [SerializeField] private GameObject lightning;
    [SerializeField] private float time=3;
    [SerializeField] private bool isCol;
    //float forceGravity = 0.5f;
    /*private void FixedUpdate()
    {
        GetComponent<Rigidbody>().AddForce(Vector3.down * forceGravity);
    }*/

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(!isCol) return;
        if (collision.gameObject.CompareTag("Player"))
        {
            //collision.transform.SetParent(transform);
            StartCoroutine(DelayCoroution());
        } 
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isCol) return;
        if (collision.gameObject.CompareTag("Player"))
        {
            //collision.transform.SetParent(transform);
            StartCoroutine(DelayCoroution());
        } 
    }


    IEnumerator DelayCoroution()
    {
        yield return new WaitForSeconds(time);
        lightning.SetActive(true);
    }
}


 
    