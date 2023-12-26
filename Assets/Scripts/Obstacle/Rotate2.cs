using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate2 : MonoBehaviour
{
    [SerializeField] float rotationSpeed = 45f; // 초당 회전 속도 (예: 45도)
    [SerializeField] float jumpingSpeed = 10f;
    private GameObject playerOnRotate;
    private Rigidbody2D playerRigidbody;
    private bool jumpflags=true;

    void Update()
    {
        // Z축 중심으로 회전 (시계 반대 방향)
        float angle = -rotationSpeed * Time.deltaTime;
        transform.rotation *= Quaternion.Euler(0f, 0f, angle);

        // 각도를 0~360도로 순환
        Vector3 currentRotation = transform.rotation.eulerAngles;
        currentRotation.z = (currentRotation.z + 360f) % 360f;
        transform.rotation = Quaternion.Euler(currentRotation);

        if (playerOnRotate !=null && ((Input.GetButtonDown("Jump") || Input.GetKeyDown(KeyCode.Space)))&&jumpflags)
        {
            
            Vector3 force = Vector3.up * jumpingSpeed;
            playerRigidbody.AddForce(force, ForceMode2D.Impulse);
            jumpflags = false; ;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            jumpflags = true;
            playerOnRotate = collision.gameObject;
            playerRigidbody = playerOnRotate.GetComponent<Rigidbody2D>();
        }
    }

}
