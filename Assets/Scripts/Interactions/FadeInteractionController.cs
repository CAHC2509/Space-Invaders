using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FadeInteractionController : InteractionBaseController
{
    [SerializeField] private Image fadeImage;
    [SerializeField] private float fadeDuration = 1f;

    public float FadeDuration => fadeDuration;

    public override void Initialize() => fadeImage.gameObject.SetActive(false);
    public override void Conclude() => fadeImage.gameObject.SetActive(false);
    public override void BeginInteraction() => StartCoroutine(FadeCoroutine(0f, 1f));
    public override void FinishInteraction() => StartCoroutine(FadeCoroutine(1f, 0f));

    private IEnumerator FadeCoroutine(float initialValue, float finalValue)
    {
        Color color = fadeImage.color;
        color.a = initialValue;
        fadeImage.color = color;

        fadeImage.gameObject.SetActive(true);

        float currentDuration = 0f;

        while (currentDuration < fadeDuration)
        {
            color.a = initialValue + (finalValue - initialValue) * currentDuration / fadeDuration;
            fadeImage.color = color;

            currentDuration += Time.deltaTime;
            yield return null;
        }

        color.a = finalValue;
        fadeImage.color = color;
        fadeImage.gameObject.SetActive(false);
    }
}
