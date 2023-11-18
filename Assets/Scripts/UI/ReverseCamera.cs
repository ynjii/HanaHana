using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//이거는.... 안 쓰이고 있음. 그냥 is Reverse 처리하면 됨. 나중에 삭제.
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






