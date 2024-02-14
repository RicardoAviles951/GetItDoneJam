using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenFader : MonoBehaviour
{
    public Image fadeImage;  // Assign the Image component in the Inspector
    public float fadeDuration = 2f;

    private void Start()
    {
        // Ensure the alpha of the fadeImage is initially set to 1 for a fully opaque screen
        fadeImage.color = new Color(0f, 0f, 0f, 1f);

        // Example: Start fading in immediately
        FadeIn(fadeDuration);
    }

    public void FadeIn(float duration)
    {
        StartCoroutine(Fade(1f, 0f, duration));
    }

    public void FadeOut(float duration)
    {
        StartCoroutine(Fade(0f, 1f, duration));
    }

    private IEnumerator Fade(float startAlpha, float targetAlpha, float duration)
    {
        float elapsedTime = 0f;
        Color color = fadeImage.color;

        while (elapsedTime < duration)
        {
            color.a = Mathf.Lerp(startAlpha, targetAlpha, elapsedTime / duration);
            fadeImage.color = color;

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        color.a = targetAlpha;
        fadeImage.color = color;
    }

}
