using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lightning : MonoBehaviour
{
    [SerializeField] private GameObject need_item; // 아이템을 먹어야 켜짐-> 아이템에 대한 정보 필요
    [SerializeField] private GameObject[] lightnings;
    [SerializeField] private bool isCol;

    private void OnCollisionEnter2D(Collision2D other)
    {       
        if(!isCol) return;
        if ((other.gameObject.CompareTag("Player"))&&need_item.GetComponent<Falling_Umbrella>().Item_get){
            foreach (GameObject gameObject in lightnings)
            {
                gameObject.SetActive(true);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isCol) return;
        if ((other.gameObject.CompareTag("Player")) && need_item.GetComponent<Falling_Umbrella>().Item_get)
        {
            foreach (GameObject gameObject in lightnings)
            {
                gameObject.SetActive(true);
            }
        }
    }
}
