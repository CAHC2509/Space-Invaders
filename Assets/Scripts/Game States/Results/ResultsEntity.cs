using System;

public class ResultsEntity
{
    public string ResultText { get; private set; }

    public event Action OnPlayPressed;
    public event Action OnMenuPressed;

    public void SetResultText(string text) => ResultText = text;
    public void PlayPressed() => OnPlayPressed?.Invoke();
    public void MenuPressed() => OnMenuPressed?.Invoke();
}
