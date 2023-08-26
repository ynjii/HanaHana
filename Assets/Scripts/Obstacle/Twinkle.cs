using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Twinkle : MonoBehaviour
{
    private Renderer renderer;
    private Color my_color;

    private void Update()
    {
        //���İ��� ���� ���ϸ� ����ص� ok,�±�- transparent, ���̾�- transparent
        if (my_color.a <=0.001f)
        {
            gameObject.tag = "Transparent";
            gameObject.layer = 10;
        }
        //else: ���� �ʰ��� �±�: Enemy, ���̾�-Enemy
        else
        {
            gameObject.tag = "Enemy";
            gameObject.layer = 8;
        }
    }

    void Start()
    {
        renderer = GetComponent<Renderer>();
        my_color = new Color(1f, 1f, 1f, 1f);
        renderer.material.color = my_color;
        StartCoroutine(TwinkleCoroutine());
    }
    
    IEnumerator TwinkleCoroutine()
    {
        while (true)
        {
            // ����������
            while (my_color.a > 0f)
            {
                my_color.a -= 0.05f;
                renderer.material.color = my_color;
                yield return new WaitForSeconds(0.01f);
            }
            // �����ϸ� ���
            yield return new WaitForSeconds(0.7f);

            // ��� �� �� �� ����
            my_color.a = 1f;
        }
    }
}
