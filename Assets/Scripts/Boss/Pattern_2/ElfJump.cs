using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElfJump : MonoBehaviour
{
    [SerializeField]float jumpingSpeed = 5f;    
    private Rigidbody2D rigid;

    void Start()
    {    
    rigid = GetComponent<Rigidbody2D>();    
    float randomTime=0;
        while(true){
            randomTime=Random.Range(0.3f, 3.0f);
            Invoke("Jump", randomTime);
        }    
    }
    
    private void Jump()
    {
        Vector3 force = Vector3.up * jumpingSpeed;
        rigid.AddForce(force, ForceMode2D.Impulse);
    }
}
