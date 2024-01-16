using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ladder : MonoBehaviour
{
    /*
    스페이스 키를 누르면 올라가고 디폴트는 내려감
    */
    public float slidingSpeed = 1f; // 미끄러질 속도 조절
    private bool isSliding = false;
    private GameObject playerOnLadder;
    private Player playerScript;
    private Rigidbody2D playerRigidbody;
    private float saveGravity;
    
    private void OnTriggerEnter2D(Collider2D collision) 
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerOnLadder = collision.gameObject;
            // player 스크립트 가져오기
            playerScript = playerOnLadder.GetComponent<Player>();

            // player 스크립트가 null이 아닌지와 변수가 있는지 확인
            if (playerScript != null)
                --playerScript.isMoss;//isMoss JumpScreenTouch, max is 1, if I -- then at least it's not jump when I first initiate
            StartCoroutine(SlidePlayerDown(collision.gameObject.transform));
        }
    }

    private IEnumerator SlidePlayerDown(Transform playerTransform)
    {
        isSliding = true;
        // X축으로 고정
        playerRigidbody = playerTransform.GetComponent<Rigidbody2D>();
        saveGravity=playerRigidbody.gravityScale;
        playerRigidbody.gravityScale=0;
        /*if (playerRigidbody != null)
        {
            playerRigidbody.constraints = RigidbodyConstraints2D.FreezeAll;
        }*/

        // Y축으로 천천히 미끄러짐
        while (isSliding)
        {
            playerTransform.Translate(Vector3.up * slidingSpeed * Time.deltaTime);
            yield return null;
        }
    }
    /*
    void Update()
    {
        if (playerOnWall && isSliding) {
        if (Input.GetButtonDown("Jump") || Input.GetKeyDown(KeyCode.Space)||(playerScript != null&& playerScript.isMoss == 1))
        {
            playerScript.isMoss = 0;
            
            }
        }
    }*/
}
