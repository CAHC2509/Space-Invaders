using UnityEngine;

public class OnboardingState : GameStateBase
{
    [SerializeField] private OnboardingController controller;
    [SerializeField] private OnboardingView view;

    private OnboardingEntity onboardingEntity;

    public void Dependencies()
    {
        onboardingEntity = new OnboardingEntity();
        controller.Dependencies(onboardingEntity);
        controller.Initialize();
        view.Dependencies(onboardingEntity);
        view.Initialize();

        EnterState();
    }

    protected override void EnterState()
    {
        base.EnterState();

        AddListeners();

        nextState = GameStates.Gameplay;
    }

    protected override void ExitState()
    {
        base.ExitState();

        RemoveListeners();

        controller.Conclude();
        view.Conclude();
    }

    private void AddListeners()
    {
        onboardingEntity.OnStartRequest += ExitState;
    }

    private void RemoveListeners()
    {
        onboardingEntity.OnStartRequest -= ExitState;
    }
}
