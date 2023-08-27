using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransparentWall : MonoBehaviour
{


    private void Awake()
    {
        if (PlayerPrefs.GetString("TransparentWall") == "False")
        {
            this.GetComponent<BoxCollider2D>().enabled = false;
        }
    }

}
