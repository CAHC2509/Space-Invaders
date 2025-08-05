using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private SceneLoaderController sceneLoader;
    [SerializeField] private GameStates currentState;
    [SerializeField] private int targetFPS = 60;

    private GameStateBase currentGameState;

    public static Action<GameStateBase> SetState;

    private void Awake()
    {
        Application.targetFrameRate = targetFPS;
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
    }

    private void Start()
    {
        AddListeners();
        LoadDefaultScene();
    }

    private void LoadDefaultScene()
    {
        sceneLoader.Initialize();
        sceneLoader.LoadScene(currentState.ToString());
    }

    private void AddListeners()
    {
        SetState += OnSetState;
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
            default:
                break;
        }
    }
}
