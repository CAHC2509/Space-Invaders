using UnityEngine;
using UnityEngine.UI;

public class OnboardingView : ViewBase
{
    [SerializeField] private Button playButton;
    [SerializeField] private GameObject readyText;

    private OnboardingEntity onboardingEntity;

    public void Dependencies(OnboardingEntity onboardingEntity)
    {
        this.onboardingEntity = onboardingEntity;
        defaultSelection = playButton.gameObject;
    }

    public override void Initialize()
    {
        base.Initialize();

        readyText.SetActive(false);
        playButton.gameObject.SetActive(false);
    }

    protected override void AddListeners()
    {
        base.AddListeners();

        playButton.onClick.AddListener(() => onboardingEntity.StartRequest());

        onboardingEntity.OnStartReady += EnableReadyUI;
    }

    protected override void RemoveListeners()
    {
        base.RemoveListeners();

        playButton.onClick.RemoveListener(() => onboardingEntity.StartRequest());

        onboardingEntity.OnStartReady -= EnableReadyUI;
    }

    private void EnableReadyUI()
    {
        playButton.gameObject.SetActive(true);
        readyText.SetActive(true);
    }
}
