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
        right,
        upRight, // �밢�� ���� �߰�
        upLeft,  // �밢�� ���� �߰�
        downRight, // �밢�� ���� �߰�
        downLeft,  // �밢�� ���� �߰�
    }

    [SerializeField]
    private ObType direction; // ����
    [SerializeField]
    private float speed; // �ӵ�

    private int num = 0;
    private float time = 0;

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (num >= 2 || time >= 10f)
        {
            Destroy(gameObject);
        }
        switch (direction)
        {

            case ObType.right:
                transform.Translate(transform.right * speed * Time.deltaTime);
                break;
            case ObType.left:
                transform.Translate(-1f * transform.right * speed * Time.deltaTime);
                break;
            case ObType.up:
                transform.Translate(transform.up * speed * Time.deltaTime);
                break;
            case ObType.down:
                transform.Translate(-1f * transform.up * speed * Time.deltaTime);
                break;
            case ObType.upRight:
                transform.Translate((transform.right + transform.up).normalized * speed * Time.deltaTime);
                break;
            case ObType.upLeft:
                transform.Translate((-transform.right + transform.up).normalized * speed * Time.deltaTime);
                break;
            case ObType.downRight:
                transform.Translate((transform.right - transform.up).normalized * speed * Time.deltaTime);
                break;
            case ObType.downLeft:
                transform.Translate((-transform.right - transform.up).normalized * speed * Time.deltaTime);
                break;
        }
    }



    private void OnCollisionEnter2D(Collision2D collision)
    {
        num++;
        Destroy(gameObject);

    }
}
