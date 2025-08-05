using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private SceneLoaderController sceneLoader;
    [SerializeField] private GameStates currentState;
    [SerializeField] private int targetFPS = 60;

    private PlayerEntity playerEntity;
    private GameplayEntity gameplayEntity;
    private GameStateBase currentGameState;

    public static Action<GameStateBase> SetState;

    private void Awake()
    {
        Application.targetFrameRate = targetFPS;
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
    }

    private void Start()
    {
        CreateDependencies();
        AddListeners();
        LoadDefaultScene();
    }

    private void CreateDependencies()
    {
        gameplayEntity = new GameplayEntity();
        playerEntity = new PlayerEntity();
    }

    private void AddListeners()
    {
        SetState += OnSetState;
    }

    private void LoadDefaultScene()
    {
        sceneLoader.Initialize();
        sceneLoader.LoadScene(currentState.ToString());
    }

    private void OnSetState(GameStateBase state)
    {
        Debug.Log($"Current state: {state.name}");

        currentGameState = state;
        currentGameState.FinishState += OnChangeState;
        StateConfiguration(currentGameState);
    }

    private void OnChangeState(GameStates nextState)
    {
        Debug.Log($"Next state: {nextState}");

        currentGameState.FinishState -= OnChangeState;

        sceneLoader.UnloadScene(currentState.ToString());
        sceneLoader.LoadScene(nextState.ToString());

        currentState = nextState;
    }

    private void StateConfiguration(GameStateBase gameState)
    {
        switch (gameState)
        {
            case MainState main:
                main.Dependencies();
                break;

            case OnboardingState onboarding:
                onboarding.Dependencies();
                break;

            case GameplayState gameplay:
                gameplay.Dependencies(gameplayEntity, playerEntity);
                break;

            case ResultsState results:
                results.Dependencies(gameplayEntity);
                break;
        }
    }
}
