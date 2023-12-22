using UnityEngine;
using System.Collections;

public class FadeInEffect : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    [SerializeField] private float fadeInDuration = 2f; // 나타나는 데 걸리는 시간 (초)
    [SerializeField] private float maxAlpha = 1f; // 최대 알파 값 (투명도)

    [SerializeField] private bool wantColliderSetActive = false;


    private void OnEnable()
    {
        spriteRenderer = GetComponent<SpriteRenderer>(); //만약 완전히 나타나고 나서 collider가 생기길 원한다면.
        StartCoroutine(FadeIn());
    }

    IEnumerator FadeIn()
    {
        float elapsedTime = 0f;
        Color objectColor = spriteRenderer.color;
        objectColor.a = 0f; // 초기에 투명하게 설정

        while (elapsedTime < fadeInDuration)
        {
            elapsedTime += Time.deltaTime;
            float newAlpha = Mathf.Lerp(0f, maxAlpha, elapsedTime / fadeInDuration);
            objectColor.a = newAlpha;
            spriteRenderer.color = objectColor;
            yield return null;
        }

        // 페이드 인이 완료된 후에 최대 알파로 설정
        objectColor.a = maxAlpha;
        spriteRenderer.color = objectColor;
        if (wantColliderSetActive)
        {
            this.GetComponent<Collider2D>().enabled = true;
        }
    }
}