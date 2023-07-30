using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ObstacleController;

public class Fire : MonoBehaviour
{
    // Start is called before the first frame update
    public enum ObType
    {
        down,
        up,
        left,
        right
    }

    [SerializeField]
    private ObType direction; // 방향
    [SerializeField]
    private float speed; // 속도

    private int num=0;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        switch(direction)
        {

            case ObType.right:
                transform.Translate(transform.right * speed * Time.deltaTime);
                break;
            case ObType.left:
                transform.Translate(-1f*transform.right * speed * Time.deltaTime);
                break;
        }
    }

   
    private void OnTriggerEnter2D(Collider2D collision)
    {
        num++;
    }

    private void destroyFire(int num)
    {
        if (num >= 2)
        {
            Destroy(gameObject);
        }
    }
}
