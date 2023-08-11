using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReverseCamera : MonoBehaviour
{
    [SerializeField] private GameObject backgroundCanvas;
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            // 현재 메인 카메라 가져오기
            Camera mainCamera = Camera.main;
            Transform camTransform = mainCamera.transform;

            // 현재 카메라의 위치 가져오기
            Vector3 camPos = camTransform.position;

            // 카메라 위치 변경 (z값을 반전)
            camPos.z *= -1f;
            camTransform.position = camPos;
        }
    }
}






