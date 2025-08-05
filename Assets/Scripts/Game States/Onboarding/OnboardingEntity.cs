using System;

public class OnboardingEntity
{
    public event Action OnStartReady;
    public event Action OnStartRequest;

    public void StartReady() => OnStartReady?.Invoke();
    public void StartRequest() => OnStartRequest?.Invoke();
}
