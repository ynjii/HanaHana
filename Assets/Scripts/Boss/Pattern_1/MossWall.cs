using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class MossWall : MonoBehaviour
{
    public float slidingSpeed = 0.5f; // 미끄러질 속도 조절
    public AudioSource audioSource;

    [SerializeField] float jumpingSpeed = 12f;
    private bool isSliding = false;
    private bool hasAppliedForce = false;
    private bool autoExit = true;
    private GameObject playerOnWall;
    private Rigidbody2D playerRigidbody;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerOnWall = collision.gameObject;
            audioSource.Play(); //재생
            StartCoroutine(SlidePlayerDown(collision.gameObject.transform));
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player")&&autoExit)
        {
            ExitMossWall();
        }
    }




    private IEnumerator SlidePlayerDown(Transform playerTransform)
    {
        hasAppliedForce = false;
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
        if (playerOnWall && (Input.GetButtonDown("Jump") || Input.GetKeyDown(KeyCode.Space)))
        {
            autoExit = false;
            ExitMossWall();
            autoExit = true;
        }
    }

    private void ExitMossWall()
    {
        audioSource.Stop(); //정지

        // 이끼벽을 떠날 때 실행되는 부분
        isSliding = false;


        // Rigidbody의 제약 조건을 모두 해제
        if (playerRigidbody != null && !hasAppliedForce)
        {
            playerRigidbody.constraints = RigidbodyConstraints2D.FreezeRotation;
            if (!autoExit) {
                Vector3 force = Vector3.up * jumpingSpeed;
                playerRigidbody.AddForce(force, ForceMode2D.Impulse);
            }
            hasAppliedForce = true; // Set the flag to true after applying force
        }
    }
}
