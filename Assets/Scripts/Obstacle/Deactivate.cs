using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deactivate : MonoBehaviour
{
    public enum activition{
        Activate,//활성화
        Deactivate//비활성화
    }
    
    [SerializeField] private activition actType; //obstacle의 type을 inspector에서 받아옴.
    [SerializeField] private bool isCol;
    
    public GameObject other;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isCol) return;
        
        if((collision.gameObject.CompareTag("Player")))
        {
            if(actType==activition.Activate){
                 other.SetActive(true); 
            }
             else{
                 other.SetActive(false);
             }
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (!isCol) return;

        if ((other.gameObject.CompareTag("Player")))
        {
            if (actType == activition.Activate)
            {
                this.other.gameObject.SetActive(true);
            }
            else
            { 
                this.other.gameObject.SetActive(false);
            }
        }
    }
}
