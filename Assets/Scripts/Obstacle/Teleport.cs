using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    [SerializeField]
    Vector3 newPos = new Vector3(0, 0, 0);
    // 충돌 감지 시 호출되는 함수 인스펙터창에서 조정하면 됨. 
    private void OnTriggerEnter2D(Collider2D col)
    {
        // 충돌한 오브젝트가 A 오브젝트인지 확인
        if (col.gameObject.CompareTag("Player"))
        {
            col.gameObject.transform.position = newPos; //부딪히면 새로운 pos로 보낸다. 
        }
    }
}
