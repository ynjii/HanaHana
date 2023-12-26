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
            foreach (Transform child in this.transform)
            {
                // 자식 오브젝트의 Swing 스크립트 비활성화
                Swing swingScript = child.GetComponent<Swing>();
                if (swingScript != null)
                {
                    swingScript.enabled = false;
                }

                // 자식 오브젝트의 Swing2 스크립트 활성화
                Swing2 swing2Script = child.GetComponent<Swing2>();
                if (swing2Script != null)
                {
                    swing2Script.enabled = true;
                }
            }
        }
    }
}
