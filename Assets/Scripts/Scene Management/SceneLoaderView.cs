using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SceneLoaderView : ViewBase
{
    [SerializeField] private Image progressBar;
    [SerializeField] private TMP_Text progressText;

    public override void Initialize()
    {
        base.Initialize();
        SetProgress(0f);
        DisableView();
    }

    public override void Conclude()
    {
        base.Conclude();
        DisableView();
    }

    public void SetProgress(float progress)
    {
        if (progressBar != null)
            progressBar.fillAmount = progress;

        if (progressText != null)
            progressText.text = $"{(progress * 100f):0}%";
    }
}
