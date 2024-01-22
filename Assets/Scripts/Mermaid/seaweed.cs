using UnityEngine;

public class seaweed : MonoBehaviour
{
    private bool isPlayerInside = false;
    private Rigidbody2D playerRigidbody;

    private void Start()
    {
        playerRigidbody = null;
    }

    private void Update()
    {
        if (isPlayerInside)
        {
            // 플레이어의 움직임을 고정
            if (playerRigidbody != null)
            {
                playerRigidbody.velocity = Vector2.zero;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInside = true;
            playerRigidbody = other.GetComponent<Rigidbody2D>();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // 플레이어가 영역을 벗어났을 때 초기화
            ResetState();
        }
    }

    private void ResetState()
    {
        // 여기에 플레이어의 상태를 초기화하는 코드를 작성
        // 예를 들어, Rigidbody의 constraints를 초기화하거나 고정된 위치를 해제하는 등의 조치를 취할 수 있습니다.

        isPlayerInside = false;
        playerRigidbody = null;
    }
}
