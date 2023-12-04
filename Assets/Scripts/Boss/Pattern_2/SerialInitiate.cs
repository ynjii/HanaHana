using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SerialInitiate : MonoBehaviour
{
    public List<GameObject> prefabsToSpawn; // 인스펙터 창에서 프리팹 리스트 지정
    [SerializeField] private float totalTime = 5.5f; // 생성을 진행할 총 시간
    [SerializeField] private float interval = 0.5f; // 생성 간격

    void Start()
    {
        StartCoroutine(SerialInitiateCoroutine());
    }

    IEnumerator SerialInitiateCoroutine()
    {
        float elapsedTime = 0.0f;

        int prefabIndex = 0; // 현재 프리팹 인덱스

        while (elapsedTime <= totalTime)
        {
            // 프리팹을 생성하면서 프리팹이 가지고 있는 초기 위치로 설정
            GameObject prefabInstance = Instantiate(prefabsToSpawn[prefabIndex], prefabsToSpawn[prefabIndex].transform.position, Quaternion.identity);

            yield return new WaitForSeconds(interval);
            elapsedTime += interval;

            prefabIndex = (prefabIndex + 1) % prefabsToSpawn.Count;
        }
    }
}