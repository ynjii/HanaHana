using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeHeadController : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Player와 충돌했을 때 해당 오브젝트의 자식 오브젝트들을 확인
            foreach (Transform child in transform)
            {
                // 자식 오브젝트의 Swing 스크립트 비활성화
                Swing swingScript = child.GetComponent<Swing>();
                if (swingScript != null)
                {
                    swingScript.enabled = false;
                }

                // 자식 오브젝트의 Rotate2 스크립트 활성화
                Rotate2 rotate2Script = child.GetComponent<Rotate2>();
                if (rotate2Script != null)
                {
                    rotate2Script.enabled = true;
                }
            }
        }
    }
}
