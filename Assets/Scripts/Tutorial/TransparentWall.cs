using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransparentWall : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Awake()
    {
        if (PlayerPrefs.GetString("TransparentWall") == "False")
        {
            Debug.Log("투명벽삭제하기");
            this.GetComponent<BoxCollider2D>().enabled = false;
        }
    }

}
