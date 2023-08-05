using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deactivate : MonoBehaviour
{
    public enum activition{
        Activate,//활성화
        Deactivate//비활성화
    }
    [SerializeField]
    private activition actType; //obstacle의 type을 inspector에서 받아옴.
    public GameObject other;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if((collision.gameObject.CompareTag("Player"))){
            Debug.Log("dkseho");
            if(actType==activition.Activate){
            other.SetActive(true);
            }
        else{
            other.SetActive(false);
        }
        }
    }
}
