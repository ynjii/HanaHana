using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//탄막 느낌으로 obj를 날립니다. 
public class ThrowObj : MonoBehaviour
{
    public GameObject objPrefab;       // 작은 불 프리팹
    public float spewInterval = 1f;     // 불을 뱉는 간격
    public float spewDuration = 3f;
    public float force = 10f;
    private float timeSinceLastSpew = -2f; //임시 나중에 waitingTime 추가할 것 

    // 각도 증가 단계
    [SerializeField]
    private float angleStep = 30f;

    //각도 시작. 여기서 조심할게... 내가 설정을 잘못해서 우리가 흔히 아는 x=0 측이 각도 0 이 아니라 -90도 각도가 0으로 취급된댜. 
    [SerializeField]
    private float angleStart = 0f;

    //각도의 끝
    [SerializeField]
    private float angleEnd = 180f;

    //설명하자면 이대로라면 0도 (지각 아래방향), 30도, 60도, 90도, 120도, 150도, 180도 (직각 위 방향)로 동시에 불이 나온다.

    public enum ObjType
    {
        typeA, //위에 방향으로 던질것인지
        typeB //아니면 그냥 랜덤으로 툭툭 던질 것인지. 
    }

    [SerializeField]
    private ObjType objType;

    [SerializeField]
    private bool wantParent = false; //부모에 상속시키려고

    [SerializeField]
    private Transform parentTransform; //부모에 상속시키려고


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

        // 시작 방향과 최종 방향 설정 여기서 애초에 y를 -1f 로 설정해서 직각 아래가 첫 시작인거임... 불편하면 (1f, 0f, 0f)로 수정하겠음. 논의 ㄱ
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
                if (wantParent)
                {
                    objInstance.transform.parent = parentTransform;
                }
                Rigidbody2D objRigidbody = objInstance.GetComponent<Rigidbody2D>();

                if (objRigidbody != null)
                {
                    //여기서 해당 방향으로 밀어주는 것까지 함
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
            if (wantParent)
            {
                objInstance.transform.parent = parentTransform;
            }
            Rigidbody2D objRigidbody = objInstance.GetComponent<Rigidbody2D>();
            if (objRigidbody != null)
            {
                // 랜덤한 방향과 힘을 생성하여 불을 튀어오르게 만듦
                Vector3 randomDirection = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0f).normalized;

                objRigidbody.AddForce(randomDirection * force, ForceMode2D.Impulse);
            }

            Destroy(objInstance, spewDuration);  // 불을 spewDuration 시간 후에 파괴
        }
    }

}