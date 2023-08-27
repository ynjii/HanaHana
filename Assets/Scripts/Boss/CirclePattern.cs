using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CirclePattern : MonoBehaviour
{


    /// <summary>
    /// rigidbody2D있어야 사용가능
    /// </summary>
    /// <param name="prefab"></param>
    public void CircleLaunch(GameObject prefab,Transform transform)   
    {

        int roundNumA = 20;


        for (int i = 0; i < roundNumA; i++)
        {
            GameObject fireObj = Instantiate(prefab, transform.position, Quaternion.identity);
            Rigidbody2D rigid = fireObj.GetComponent<Rigidbody2D>();
            Vector2 dirVec = new Vector2(Mathf.Cos(Mathf.PI * 2 * i / roundNumA),
                             Mathf.Sin(Mathf.PI * 2 * i / roundNumA));  //원 형태로 발사
            rigid.AddForce(dirVec.normalized * 5, ForceMode2D.Impulse);

        }
    }

    /// <summary>
    /// 인자: 리스트
    /// 오버로드
    /// </summary>
    /// <param name="prefab"></param>
    /// <param name="transform"></param>
    public void CircleLaunch(List<GameObject> prefab, Transform transform,float speed)
    {

        int roundNumA = prefab.Count;
        for (int i = 0; i < roundNumA; i++)
        {
            GameObject fireObj = Instantiate(prefab[i], transform.position, Quaternion.identity);
            Rigidbody2D rigid = fireObj.GetComponent<Rigidbody2D>();
            Vector2 dirVec = new Vector2(Mathf.Cos(Mathf.PI * 2 * i / roundNumA),
                             Mathf.Sin(Mathf.PI * 2 * i / roundNumA));  //원 형태로 발사
            rigid.AddForce(dirVec.normalized * speed, ForceMode2D.Impulse);
        }
    }

    /// <summary>
    /// C모양으로 쏘기
    /// </summary>
    /// <param name="prefab"></param>
    /// <param name="transform"></param>
    public void CLaunch(GameObject prefab, Transform transform, int whole_num)
    {

        int roundNumA = 20;
        int whole_start=Random.Range(0, roundNumA-whole_num);//0~roundNumA-whole_num-1
        int whole_count = 0;
        for (int i = 0; i < roundNumA; i++)
        {
            if (whole_start == i&&whole_count<whole_num)
            {
                whole_start++;
                whole_count++;
                continue;
            }
            GameObject fireObj = Instantiate(prefab, transform.position, Quaternion.identity);
            Rigidbody2D rigid = fireObj.GetComponent<Rigidbody2D>();
            Vector2 dirVec = new Vector2(Mathf.Cos(Mathf.PI * 2 * i / roundNumA),
                             Mathf.Sin(Mathf.PI * 2 * i / roundNumA));  //원 형태로 발사
            rigid.AddForce(dirVec.normalized * 5, ForceMode2D.Impulse);

        }
    }


    /// <summary>
    /// 최상위 오브젝트를 인자로 넣기
    /// 최상위 오브젝트의 하위 오브젝트들을 바깥 방향으로 쏘는 함수임.
    /// </summary>
    /// <param name="parent_transform"></param>
    public void LaunchToOutside(Transform parent_transform, float launchForce, float spreadFactor)
    {
        int roundNumA = parent_transform.childCount;

        for (int i = 0; i < roundNumA; i++)
        {
            Transform child_transform = parent_transform.GetChild(i);
            Rigidbody2D rigid = child_transform.gameObject.GetComponent<Rigidbody2D>();

            Vector2 dirVec = new Vector2(Mathf.Cos(Mathf.PI * 2 * i / roundNumA),
                                         Mathf.Sin(Mathf.PI * 2 * i / roundNumA));  // 원 형태로 발사

            Vector2 spreadForce = dirVec.normalized * launchForce * Random.Range(1.0f, spreadFactor);
            rigid.AddForce(spreadForce, ForceMode2D.Impulse);
        }
    }
}
