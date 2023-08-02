using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderActivation : MonoBehaviour
{
    public GameObject childObject; // 활성화할 자식 오브젝트

    // 다른 콜라이더와 충돌했을 때 호출되는 함수
    private void OnTriggerEnter(Collider other)
    {
            Debug.Log("뭐꼬");
            // 자식 오브젝트 활성화
            childObject.SetActive(true);
    }
}
