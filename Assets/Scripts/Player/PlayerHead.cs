using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHead : MonoBehaviour
{
    public bool ignore_Input=false;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag=="Platform") 
        {
            ignore_Input = true;
            //레이어변경
            this.gameObject.layer = 7;
        }
    }
}
