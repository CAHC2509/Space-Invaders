using UnityEngine;

public class ResultsState : GameStateBase
{
    [SerializeField] private ResultsView view;

    private ResultsEntity resultsEntity;

    public void Dependencies(GameplayEntity gameplayEntity)
    {
        resultsEntity = new ResultsEntity();

        string result = gameplayEntity.LastResult == MatchResult.Victory ? "Victory" : "Game Over";
        resultsEntity.SetResultText(result);

        view.Dependencies(resultsEntity);
        view.Initialize();

        EnterState();
    }

    protected override void EnterState()
    {
        base.EnterState();
        AddListeners();
    }

    protected override void ExitState()
    {
        base.ExitState();
        RemoveListeners();
        view.Conclude();
    }

    private void AddListeners()
    {
        resultsEntity.OnPlayPressed += GoToGameplay;
        resultsEntity.OnMenuPressed += GoToMain;
    }

    private void RemoveListeners()
    {
        resultsEntity.OnPlayPressed -= GoToGameplay;
        resultsEntity.OnMenuPressed -= GoToMain;
    }

    private void GoToGameplay()
    {
        nextState = GameStates.Gameplay;
        ExitState();
    }

    private void GoToMain()
    {
        nextState = GameStates.Main;
        ExitState();
    }
}
