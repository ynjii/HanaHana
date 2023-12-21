using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class MossWall : MonoBehaviour
{
    public float slidingSpeed = 0.5f; // 미끄러질 속도 조절
    private bool isSliding = false;
    private GameObject playerOnWall;
    private Rigidbody2D playerRigidbody;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerOnWall = collision.gameObject;
            StartCoroutine(SlidePlayerDown(collision.gameObject.transform));
        }
    }

    private IEnumerator SlidePlayerDown(Transform playerTransform)
    {
        isSliding = true;

        // X축으로 고정
        playerRigidbody = playerTransform.GetComponent<Rigidbody2D>();
        if (playerRigidbody != null)
        {
            playerRigidbody.constraints = RigidbodyConstraints2D.FreezeAll;
        }

        // Y축으로 천천히 미끄러짐
        while (isSliding)
        {
            playerTransform.Translate(Vector3.down * slidingSpeed * Time.deltaTime);
            yield return null;
        }
    }

    void Update()
    {
        if (playerOnWall&& (Input.GetButtonDown("Jump") || Input.GetKeyDown(KeyCode.Space)))
        {
            ExitMossWall();
        }
    }

    private void ExitMossWall()
    {
        // 이끼벽을 떠날 때 실행되는 부분
        isSliding = false;

        // Rigidbody의 제약 조건을 모두 해제
        if (playerRigidbody != null)
        {
            playerRigidbody.constraints = RigidbodyConstraints2D.FreezeRotation;
        }
    }
}
