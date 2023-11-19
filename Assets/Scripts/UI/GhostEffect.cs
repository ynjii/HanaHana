using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostEffect : MonoBehaviour
{
    public GameObject ghost; // 잔상으로 사용할 게임 오브젝트
    public float ghostDelay = 0.1f; // 잔상 생성 간격

    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        StartCoroutine(CreateGhost());
    }

    private IEnumerator CreateGhost() //ghost: 잔상, soul : 정령 
    {
        while (true) //이거 잔상임.
        {
            yield return new WaitForSecondsRealtime(ghostDelay);
            GameObject currentGhost = Instantiate(ghost, transform.position, transform.rotation);
            currentGhost.transform.localScale = transform.localScale;
            currentGhost.GetComponent<SpriteRenderer>().sprite = spriteRenderer.sprite;
            Destroy(currentGhost, 0.01f); //이게 0.01초 안 기다리면 삭제 안 됐던 것 같음... 내가 timescale = 0f로 해놨더니 생긴 일. 아마 시간 안 지나서
        }
    }
}
