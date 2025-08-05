using UnityEngine;

public class EnemyController : ControllerBase
{
    [SerializeField] private EnemyMovementSystem movementSystem;
    [SerializeField] private EnemyRegistry registry;
    [SerializeField] private Transform enemiesContainer;
    [SerializeField] private float baseSpeed = 2f;
    [SerializeField] private float speedIncreasePerLevel = 0.25f;

    private GameplayEntity gameplayEntity;

    public void Dependencies(GameplayEntity gameplayEntity)
    {
        this.gameplayEntity = gameplayEntity;
        registry.Dependencies(gameplayEntity);
    }

    protected override void AddListeners()
    {
        gameplayEntity.OnWarmingStart += EnableEnemies;
        gameplayEntity.OnGameStart += StartLevel;
        gameplayEntity.OnLevelWon += NextLevel;
        gameplayEntity.OnGameLost += Stop;
        gameplayEntity.OnLevelLost += RestartLevel;
    }

    protected override void RemoveListeners()
    {
        gameplayEntity.OnWarmingStart -= EnableEnemies;
        gameplayEntity.OnGameStart -= StartLevel;
        gameplayEntity.OnLevelWon -= NextLevel;
        gameplayEntity.OnGameLost -= Stop;
        gameplayEntity.OnLevelLost -= RestartLevel;
    }

    private void StartLevel()
    {
        float speed = baseSpeed;
        movementSystem.SetSpeed(speed);
        movementSystem.StartMovement();
    }

    private void NextLevel()
    {
        float speed = baseSpeed + gameplayEntity.CurrentLevel * speedIncreasePerLevel;
        movementSystem.StopMovement();
        movementSystem.SetSpeed(speed);
        movementSystem.StartMovement();
    }

    private void RestartLevel()
    {
        EnableEnemies();
        movementSystem.StopMovement();
    }

    private void Stop()
    {
        movementSystem.StopMovement();
    }

    private void EnableEnemies()
    {
        registry.Reset();

        foreach (Transform enemy in enemiesContainer)
        {
            if (!enemy.gameObject.activeSelf)
                enemy.gameObject.SetActive(true);

            if (enemy.TryGetComponent(out Enemy enemyComponent))
                registry.Register(enemyComponent);
        }
    }
}
