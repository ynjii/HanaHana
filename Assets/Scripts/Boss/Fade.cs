using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Fade : MonoBehaviour
{
    public enum FadeType
    {
        FadeIn,
        FadeOut
    }

    [SerializeField] private FadeType fadeType;
    private float elapsedTime = 0f;
    private float fadeDuration = 2.0f;

    private void OnEnable()
    {
        Image image = GetComponent<Image>();

        if (fadeType == FadeType.FadeIn)
        {
            StartCoroutine(FadeIn(image));
        }
        else if (fadeType == FadeType.FadeOut)
        {
            StartCoroutine(FadeOut(image));
        }
    }

    private IEnumerator FadeIn(Image image)
    {
        Color originalColor = image.color;
        Color targetColor = new Color(originalColor.r, originalColor.g, originalColor.b, 1f);

        image.color = new Color(originalColor.r, originalColor.g, originalColor.b, 0f);

        while (elapsedTime < fadeDuration)
        {
            float alpha = Mathf.Lerp(0f, 1f, elapsedTime / fadeDuration);
            Color newColor = Color.Lerp(originalColor, targetColor, alpha);
            image.color = newColor;

            yield return null;

            elapsedTime += Time.unscaledDeltaTime;
        }

        image.color = targetColor;
    }

    private IEnumerator FadeOut(Image image)
    {
        Color originalColor = image.color;
        Color targetColor = new Color(originalColor.r, originalColor.g, originalColor.b, 0f);

        image.color = new Color(originalColor.r, originalColor.g, originalColor.b, 1f);

        while (elapsedTime < fadeDuration)
        {
            float alpha = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration);
            Color newColor = Color.Lerp(originalColor, targetColor, alpha);

            image.color = newColor;

            yield return null;

            elapsedTime += Time.unscaledDeltaTime;
        }

        image.color = targetColor;
    }
}