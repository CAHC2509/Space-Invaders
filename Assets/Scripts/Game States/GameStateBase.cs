using System;
using UnityEngine;

public class GameStateBase : MonoBehaviour
{
    public event Action<GameStates> FinishState;

    protected GameStates nextState;

    protected virtual void Start() => GameManager.SetState?.Invoke(this);
    protected virtual void EnterState() { }
    protected virtual void ExitState() => FinishState?.Invoke(nextState);
}
