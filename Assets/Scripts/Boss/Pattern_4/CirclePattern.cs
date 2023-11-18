using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 원형 총알 발사시키는 스크립트
/// rigidbody2D있어야 사용가능
/// </summary>
public class CirclePattern : MonoBehaviour
{
    //프리팹이랑 발사시킬 위치 넣어준다
    public void CircleLaunch(GameObject prefab,Transform transform)   
    {
        // 발사할 게임 오브젝트의 수
        int roundNumA = 15;

        // 주어진 수만큼 반복하여 게임 오브젝트 발사
        for (int i = 0; i < roundNumA; i++)
        {
            // 게임 오브젝트 프리팹을 복제하고, 발사 위치에 생성
            GameObject fireObj = Instantiate(prefab, transform.position, Quaternion.identity);
            // 생성된 게임 오브젝트의 Rigidbody2D 컴포넌트 획득
            Rigidbody2D rigid = fireObj.GetComponent<Rigidbody2D>();
            // 원 형태로 발사하기 위한 방향 벡터 계산
            /*
             * 각도0: 2파이*0/15, 각도1: 2파이*1/15, ...
             * 원의 둘레를 2π로 나누면 한 바퀴(360도)가 됨
             */
            Vector2 dirVec = new Vector2(Mathf.Cos(Mathf.PI * 2 * i / roundNumA),
                             Mathf.Sin(Mathf.PI * 2 * i / roundNumA));  //원 형태로 발사
            // 계산된 방향 벡터에 일정한 크기의 힘을 가하여 발사
            rigid.AddForce(dirVec.normalized * 5, ForceMode2D.Impulse);

        }
    }

    /// <summary>
    /// C모양으로 쏘기
    /// hole_num: 구멍 프리팹 몇 개만큼 뚫을건지 
    /// </summary>
    /// <param name="prefab"></param>
    /// <param name="transform"></param>
    public void CLaunch(GameObject prefab, Transform transform, int hole_num)
    {
        int roundNumA = 15;
        int whole_start=Random.Range(0, roundNumA-hole_num);//0~roundNumA-whole_num-1
        int whole_count = 0;
        for (int i = 0; i < roundNumA; i++)
        {
            if (whole_start == i&&whole_count<hole_num)
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
    /// 인자: 리스트
    /// 오버로드버전
    /// 
    /// 다 다른 모양으로 원형발사 하고싶을 때 이거 쓰기
    /// 예시) 보스패턴4 보스 죽이고 거울 쨍그랑 할 때 
    /// </summary>
    /// <param name="prefab"></param>
    /// <param name="transform"></param>
    public void CircleLaunch(List<GameObject> prefab, Transform transform, float speed)
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
    /// 최상위 오브젝트를 인자로 넣기
    /// 최상위 오브젝트의 하위 오브젝트들을 바깥 방향으로 쏘는 함수임.
    /// 예시) 바람개비(PinWheel) 
    /// 바람개비 프리팹 어떻게 생겼나 보고오면 이해 됨
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
