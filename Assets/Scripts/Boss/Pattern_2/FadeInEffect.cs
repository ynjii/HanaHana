using UnityEngine;
using System.Collections;

public class FadeInEffect : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    [SerializeField] private float fadeInDuration = 2f; // 나타나는 데 걸리는 시간 (초)
    [SerializeField] private float maxAlpha = 1f; // 최대 알파 값 (투명도)

    private void OnEnable()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
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
    }
}