using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ResultsView : ViewBase
{
    [SerializeField] private TextMeshProUGUI resultText;
    [SerializeField] private Button playButton;
    [SerializeField] private Button menuButton;

    private ResultsEntity resultsEntity;

    public void Dependencies(ResultsEntity resultsEntity)
    {
        this.resultsEntity = resultsEntity;
        defaultSelection = playButton.gameObject;
    }

    protected override void AddListeners()
    {
        base.AddListeners();

        playButton.onClick.AddListener(() => resultsEntity.PlayPressed());
        menuButton.onClick.AddListener(() => resultsEntity.MenuPressed());
    }

    protected override void RemoveListeners()
    {
        base.RemoveListeners();

        playButton.onClick.RemoveAllListeners();
        menuButton.onClick.RemoveAllListeners();
    }

    public override void Initialize()
    {
        base.Initialize();
        resultText.text = resultsEntity.ResultText;
    }
}
