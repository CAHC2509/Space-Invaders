using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class OnboardingController : ControllerBase
{
    [SerializeField] private float timeToSetStartReady = 2f;

    private OnboardingEntity onboardingEntity;
    private bool startReady;

    public void Dependencies(OnboardingEntity onboardingEntity)
    {
        this.onboardingEntity = onboardingEntity;
        startReady = false;
    }

    public override void Initialize()
    {
        base.Initialize();

        StartCoroutine(CountdownCoroutine());
    }

    private void Update()
    {
        if (startReady && Keyboard.current.spaceKey.wasPressedThisFrame)
            onboardingEntity.StartRequest();
    }

    protected override void AddListeners()
    {
        onboardingEntity.OnStartReady += SetStartReady;
    }

    protected override void RemoveListeners()
    {
        onboardingEntity.OnStartReady -= SetStartReady;
    }

    private IEnumerator CountdownCoroutine()
    {
        yield return new WaitForSeconds(timeToSetStartReady);
        onboardingEntity.StartReady();
    }

    private void SetStartReady() => startReady = true;
}
