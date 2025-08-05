using System.Collections;
using UnityEngine;

public class GameplayState : GameStateBase
{
    [SerializeField] private MatchCountdownInteraction countdownInteraction;
    [SerializeField] private GameplayController controller;
    [SerializeField] private GameplayView view;

    private PlayerEntity playerEntity;
    private GameplayEntity gameplayEntity;

    public void Dependencies(GameplayEntity gameplayEntity, PlayerEntity playerEntity)
    {
        this.gameplayEntity = gameplayEntity;
        this.playerEntity = playerEntity;

        controller.Dependencies(gameplayEntity, playerEntity);
        controller.Initialize();
        view.Dependencies(gameplayEntity, playerEntity);
        view.Initialize();

        EnterState();
    }

    protected override void EnterState()
    {
        base.EnterState();

        AddListeners();

        nextState = GameStates.Results;

        StartMatch();
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
        gameplayEntity.OnGameWon += ExitState;
        gameplayEntity.OnGameLost += ExitState;
    }

    private void RemoveListeners()
    {
        gameplayEntity.OnGameWon -= ExitState;
        gameplayEntity.OnGameLost -= ExitState;
    }

    private void StartMatch()
    {
        StartCoroutine(CountdownCoroutine());
    }

    private IEnumerator CountdownCoroutine()
    {
        gameplayEntity.WarmingStart();

        countdownInteraction.Initialize();
        countdownInteraction.BeginInteraction();

        yield return new WaitForSeconds(countdownInteraction.CountdownDelay);

        countdownInteraction.FinishInteraction();
        countdownInteraction.Conclude();

        gameplayEntity.GameStart();
    }
}
