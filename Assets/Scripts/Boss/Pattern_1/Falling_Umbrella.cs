using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Falling_Umbrella : MonoBehaviour
{
    private bool item_get = false;
    public bool Item_get
    {
        get { return item_get; } 
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        item_get = true;
    
    }


}


 
    