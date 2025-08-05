using System;

public class GameplayEntity
{
    public event Action OnWarmingStart;
    public event Action OnGameStart;
    public event Action OnGameLost;
    public event Action OnGameWon;
    public event Action OnLevelWon;
    public event Action OnLevelLost;

    public MatchResult LastResult => lastResult;
    public int CurrentLevel => currentLevel;

    private MatchResult lastResult;
    private int currentLevel = 1;

    public void GameLost()
    {
        lastResult = MatchResult.GameOver;
        OnGameLost?.Invoke();
    }

    public void GameWon()
    {
        lastResult = MatchResult.Victory;
        OnGameWon?.Invoke();
    }

    public void LevelWon()
    {
        currentLevel++;
        OnLevelWon?.Invoke();
    }

    public void WarmingStart() => OnWarmingStart?.Invoke();
    public void GameStart() => OnGameStart?.Invoke();
    public void LevelLost() => OnLevelLost?.Invoke();
    public void ResetLevelsCount() => currentLevel = 1;
}

public enum MatchResult { GameOver, Victory }
