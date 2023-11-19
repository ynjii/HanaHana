using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*성맵에서 빙글빙글 도는 바닥 4개, 플레이어 물리기 바닥의 물리를 이어받음*/
public class CircleMovement : MonoBehaviour
{
    public enum Direc
    {
        up,
        down,
        left,
        right
    }
    [SerializeField]
    Transform rotationCenter;

    [SerializeField]
    float rotationRadius = 2f, angularSpeed = 2f;

    [SerializeField]
    private Direc direc;



    float posX, posY, angle = 0f;

    void Awake()
    {
        switch (direc)
        {
            case Direc.up:
                angle = 1.5f;
                break;

            case Direc.down:
                angle = 4.6f;
                break;


            case Direc.left:
                angle = 3.1f;
                break;


            case Direc.right:
                angle = 0f;
                break;
        }
    }
    void Update()
    {
        posX = rotationCenter.position.x + Mathf.Cos(angle) * rotationRadius;
        posY = rotationCenter.position.y + Mathf.Sin(angle) * rotationRadius;

        transform.position = new Vector2(posX, posY);
        angle = angle + Time.deltaTime * angularSpeed;

        if (angle >= 360f)
        {
            angle = 0f;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.SetParent(transform);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            collision.transform.SetParent(null);
        }
    }
}
