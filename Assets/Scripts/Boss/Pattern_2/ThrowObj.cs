using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowObj : MonoBehaviour
{
    public GameObject objPrefab;       // 작은 불 프리팹
    public float spewInterval = 1f;     // 불을 뱉는 간격
    public float spewDuration = 3f;
    public float force = 10f;
    private float timeSinceLastSpew = 0f;

    // 각도 증가 단계
    [SerializeField]
    private float angleStep = 30f;

    //각도 시작
    [SerializeField]
    private float angleStart = 0f;

    //각도 끝
    [SerializeField]
    private float angleEnd = 180f;

    public enum ObjType
    {
        typeA,
        typeB
    }

    [SerializeField]
    private ObjType objType;



    private void Update()
    {
        timeSinceLastSpew += Time.deltaTime;

        if (timeSinceLastSpew >= spewInterval)
        {

            switch (objType)
            {
                case ObjType.typeA:
                    StartSpewingA();
                    break;

                case ObjType.typeB:
                    StartSpewingB();
                    break;
            }
        }
    }

    private void StartSpewingA()
    {
        timeSinceLastSpew = 0f;

        // 시작 방향과 최종 방향 설정
        Vector3 startDirection = new Vector3(0f, -1f, 0f);
        Vector3 targetDirection = new Vector3(0f, 1f, 0f);

        if (objPrefab != null)
        {

            for (float angle = angleStart; angle <= angleEnd; angle += angleStep)
            {
                // 각도를 Quaternion으로 변환하여 방향 계산
                Quaternion rotation = Quaternion.Euler(0f, 0f, angle);
                Vector3 direction = rotation * startDirection;

                GameObject objInstance = Instantiate(objPrefab, transform.position, Quaternion.identity);
                Rigidbody2D objRigidbody = objInstance.GetComponent<Rigidbody2D>();

                if (objRigidbody != null)
                {
                    objRigidbody.AddForce(direction * force, ForceMode2D.Impulse);
                }

                Destroy(objInstance, spewDuration);  // 불을 spewDuration 시간 후에 파괴
            }
        }
    }

    private void StartSpewingB()
    {
        timeSinceLastSpew = 0f;

        if (objPrefab != null)
        {
            GameObject objInstance = Instantiate(objPrefab, transform.position, Quaternion.identity);
            Rigidbody2D objRigidbody = objInstance.GetComponent<Rigidbody2D>();
            if (objRigidbody != null)
            {
                // 랜덤한 방향과 힘을 생성하여 불을 튀어오르게 만듦
                Vector3 randomDirection = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 0.2f), 0f).normalized;

                objRigidbody.AddForce(randomDirection * force, ForceMode2D.Impulse);
            }

            Destroy(objInstance, spewDuration);  // 불을 spewDuration 시간 후에 파괴
        }
    }

}