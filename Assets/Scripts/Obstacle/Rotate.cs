using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*위에 있는 오브젝트 비활성하고(겹쳐있다.), 자기가 540도 도는 함수-한 바퀴 전부 도는 게 어때? 리모델링 필요*/
public class Rotate : MonoBehaviour
{
    [SerializeField]
    private float rotationSpeed = 360f; //회전 속도 (도/초)
    [SerializeField]
    private float endDegree=540f;
    public GameObject other;
    /*
    1. Trigger 되면
    2. 스크립트 비활성화 하고
    3. Rotate 0~360도 한 번 회전 후 끝.
    */

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(RotateObject());
        }
    }

    private IEnumerator RotateObject()
    {
        float startRotation = transform.rotation.eulerAngles.z;//현재 회전 각도
        float targetRotation = startRotation + endDegree; //목표 회전 각도
        float t = 0f;
        float currentRotation=startRotation;

        while (currentRotation < targetRotation)
        {
            t += Time.deltaTime*rotationSpeed;

            currentRotation = Mathf.Lerp(startRotation, targetRotation, t);
            transform.rotation = Quaternion.Euler(0f, 0f, currentRotation);
            
            yield return null;
        }
    }
}


