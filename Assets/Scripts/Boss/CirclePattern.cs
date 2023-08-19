using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CirclePattern : MonoBehaviour
{
    private Rigidbody2D rigid;
    public GameObject prefab;
    // Start is called before the first frame update
    void Start()
    {
        rigid=prefab.GetComponent<Rigidbody2D>();
    }

    /// <summary>
    /// rigidbody2D있어야 사용가능
    /// </summary>
    /// <param name="prefab"></param>
    public void CircleLaunch(GameObject prefab,Transform transform)  //스킬 버튼 누르면 원모양으로 불이 퍼져나감 
    {

        int roundNumA = 25;


        for (int i = 0; i < roundNumA; i++)
        {
            GameObject fireObj = Instantiate(prefab, transform.position, Quaternion.identity);
            Rigidbody2D rigid = fireObj.GetComponent<Rigidbody2D>();
            Vector2 dirVec = new Vector2(Mathf.Cos(Mathf.PI * 2 * i / roundNumA),
                             Mathf.Sin(Mathf.PI * 2 * i / roundNumA));  //원 형태로 발사
            rigid.AddForce(dirVec.normalized * 5, ForceMode2D.Impulse);

        }
    }
}
