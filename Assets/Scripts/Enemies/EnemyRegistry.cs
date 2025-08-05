using UnityEngine;

public class EnemyRegistry : MonoBehaviour
{
    private int aliveEnemies = 0;
    private GameplayEntity gameplayEntity;

    public void Dependencies(GameplayEntity gameplayEntity)
    {
        this.gameplayEntity = gameplayEntity;
    }

    public void Register(Enemy enemy)
    {
        aliveEnemies++;
        enemy.OnDestroyed -= OnEnemyDestroyed;
        enemy.OnDestroyed += OnEnemyDestroyed;
    }

    private void OnEnemyDestroyed(Enemy enemy)
    {
        aliveEnemies--;

        if (aliveEnemies <= 0)
            gameplayEntity.LevelWon();
    }

    public void Reset()
    {
        aliveEnemies = 0;
    }
}
