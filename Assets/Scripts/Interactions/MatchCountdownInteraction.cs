using System.Collections;
using TMPro;
using UnityEngine;

public class MatchCountdownInteraction : InteractionBaseController
{
    [SerializeField] private TextMeshProUGUI countdownText;
    [SerializeField] private int countdownDelay = 5;

    private Coroutine countdownCoroutine;

    public int CountdownDelay => countdownDelay;

    public override void Initialize()
    {
        countdownText.gameObject.SetActive(true);
    }

    public override void Conclude()
    {
        countdownText.gameObject.SetActive(false);
    }

    public override void BeginInteraction()
    {
        if (countdownCoroutine != null)
            StopCoroutine(countdownCoroutine);

        countdownCoroutine = StartCoroutine(CountdownCoroutine());
    }

    public override void FinishInteraction()
    {
        countdownCoroutine = null;
    }

    private IEnumerator CountdownCoroutine()
    {
        int currentValue = countdownDelay;

        while (currentValue > 0)
        {
            countdownText.text = currentValue.ToString();

            yield return new WaitForSeconds(1f);

            currentValue--;
        }
    }
}