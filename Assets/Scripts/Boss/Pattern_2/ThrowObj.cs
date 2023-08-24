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

    private void Update()
    {
        timeSinceLastSpew += Time.deltaTime;

        if (timeSinceLastSpew >= spewInterval)
        {
            StartSpewing();
        }
    }

    private void StartSpewing()
    {
        timeSinceLastSpew = 0f;

        if (objPrefab != null)
        {
            GameObject objInstance = Instantiate(objPrefab, transform.position, Quaternion.identity);
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