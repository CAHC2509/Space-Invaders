using UnityEngine;

public class MainState : GameStateBase
{
    [SerializeField] private MainController controller;
    [SerializeField] private MainView view;

    private MainEntity mainEntity;

    public void Dependencies()
    {
        mainEntity = new MainEntity();
        controller.Dependencies(mainEntity);
        controller.Initialize();
        view.Dependencies(mainEntity);
        view.Initialize();

        EnterState();
    }

    protected override void EnterState()
    {
        base.EnterState();

        AddListeners();

        nextState = GameStates.Onboarding;
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
        mainEntity.OnPlayPressed += ExitState;
    }

    private void RemoveListeners()
    {
        mainEntity.OnPlayPressed -= ExitState;
    }
}
