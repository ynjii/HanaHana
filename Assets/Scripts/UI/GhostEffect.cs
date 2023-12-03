using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostEffect : MonoBehaviour
{
    [SerializeField] private GameObject ghost; // 잔상으로 사용할 게임 오브젝트
    [SerializeField] private float ghostDelay = 0.1f; // 잔상 생성 간격
    [SerializeField]
    private float ghostDestroyTime = 0.01f;

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
            Destroy(currentGhost, ghostDestroyTime); //이게 0.01초 안 기다리면 삭제 안 됐던 것 같음... 내가 timescale = 0f로 해놨더니 생긴 일. 아마 시간 안 지나서
        }
    }
}
