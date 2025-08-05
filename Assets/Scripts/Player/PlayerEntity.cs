using System;
using UnityEngine;

[Serializable]
public class PlayerEntity
{
    public event Action OnScoreUpdated;
    public event Action OnLifesUpdated;

    public int Score => score;
    public int Lifes => lifes;

    private int maxLifes = 3;
    private int lifes;
    private int score;

    public void UpdateLifes(int amount)
    {
        lifes += amount;

        if (lifes > maxLifes)
            lifes = maxLifes;

        OnLifesUpdated?.Invoke();
    }

    public void UpdateScore(int amount)
    {
        score += amount;
        OnScoreUpdated?.Invoke();
    }

    public void ResetLifes() => UpdateLifes(maxLifes);
    public void ResetScore() => UpdateScore(-score);    
}
