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
        gameplayEntity.OnLevelWon += OnLevelWonHandler;
        gameplayEntity.OnGameLost += Stop;
        gameplayEntity.OnLevelLost += OnLevelLostHandler;
    }

    protected override void RemoveListeners()
    {
        gameplayEntity.OnWarmingStart -= EnableEnemies;
        gameplayEntity.OnGameStart -= StartLevel;
        gameplayEntity.OnLevelWon -= OnLevelWonHandler;
        gameplayEntity.OnGameLost -= Stop;
        gameplayEntity.OnLevelLost -= OnLevelLostHandler;
    }


    private void StartLevel()
    {
        float speed = baseSpeed;
        movementSystem.CacheInitialPositions();
        movementSystem.SetSpeed(speed);
        movementSystem.StartMovement();
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

    private void RestartLevel(bool increaseSpeed)
    {
        movementSystem.StopMovement();
        EnableEnemies();

        float speed = baseSpeed;
        if (increaseSpeed)
            speed += speedIncreasePerLevel * (gameplayEntity.CurrentLevel - 1);

        movementSystem.CacheInitialPositions();
        movementSystem.SetSpeed(speed);
        movementSystem.ResetPosition();
        movementSystem.StartMovement();
    }

    private void Stop() => movementSystem.StopMovement();
    private void OnLevelWonHandler() => RestartLevel(true);
    private void OnLevelLostHandler() => RestartLevel(false);
}
