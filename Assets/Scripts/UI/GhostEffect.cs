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

    private IEnumerator CreateGhost()
    {
        while (true)
        {
            yield return new WaitForSecondsRealtime(ghostDelay);
            GameObject currentGhost = Instantiate(ghost, transform.position, transform.rotation);
            currentGhost.transform.localScale = transform.localScale;
            currentGhost.GetComponent<SpriteRenderer>().sprite = spriteRenderer.sprite;
            Destroy(currentGhost, 0.01f);
        }
    }
}
