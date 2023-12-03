using UnityEngine;

public class IfExitDie : MonoBehaviour
{
    public GameObject player;
    private float checkInterval = 0.1f; // 플레이어 위치 확인 간격
    private bool isPlayerInsideCollider = false;

    void Start()
    {
        // 일정 간격으로 플레이어 위치를 확인하는 코루틴 시작
        InvokeRepeating("CheckPlayerPosition", 0.0f, checkInterval);
    }

    void CheckPlayerPosition()
    {
        // 플레이어의 위치를 확인
        CheckIfPlayerInsideCollider();

        // 플레이어가 collider 밖에 있고, isPlayerInsideCollider가 false인 경우 die 함수 실행
        if (!isPlayerInsideCollider)
        {
            Die();
        }
    }

    void CheckIfPlayerInsideCollider()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, GetComponent<Collider2D>().bounds.extents.x);

        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("Player"))
            {
                isPlayerInsideCollider = true;
                return;
            }
        }

        isPlayerInsideCollider = false;
    }

    void Die()
    {
        Debug.Log("씨발 대체 왜?");
        player.GetComponent<Player>().Die(transform.position);
    }

    private void OnDisable()
    {
        CancelInvoke("CheckPlayerPosition");
    }
    void OnDestroy()
    {
        // 스크립트가 제거될 때 InvokeRepeating을 정지시킴
        CancelInvoke("CheckPlayerPosition");
    }
}
