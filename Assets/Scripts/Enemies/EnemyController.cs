using System.Collections;
using UnityEngine;

public class EnemyController : ControllerBase
{
    [SerializeField] private Transform enemiesContainer;
    [SerializeField] private float baseSpeed = 2f;
    [SerializeField] private float speedIncreasePerLevel = 0.25f;
    [SerializeField] private float stepDownAmount = 0.5f;
    [SerializeField] private float distanceLimit = 10f;

    private float currentSpeed;
    private int direction = 1;
    private int aliveEnemiesCount;
    private Coroutine movementCoroutine;
    private GameplayEntity gameplayEntity;

    public void Dependencies(GameplayEntity gameplayEntity)
    {
        this.gameplayEntity = gameplayEntity;
        currentSpeed = baseSpeed;
    }

    protected override void AddListeners()
    {
        gameplayEntity.OnWarmingStart += EnableEnemies;
        gameplayEntity.OnGameStart += OnGameStart;
        gameplayEntity.OnGameLost += StopMovement;
        gameplayEntity.OnLevelWon += OnLevelWon;
        gameplayEntity.OnLevelLost += OnLevelLost;
    }

    protected override void RemoveListeners()
    {
        gameplayEntity.OnWarmingStart -= EnableEnemies;
        gameplayEntity.OnGameStart -= OnGameStart;
        gameplayEntity.OnGameLost -= StopMovement;
        gameplayEntity.OnLevelWon -= OnLevelWon;
        gameplayEntity.OnLevelLost -= OnLevelLost;
    }

    private void OnGameStart()
    {
        currentSpeed = baseSpeed;
        StartMovement();
    }

    private void OnLevelWon()
    {
        currentSpeed = baseSpeed + speedIncreasePerLevel * gameplayEntity.CurrentLevel;
        StopMovement();
        StartMovement();
    }

    private void OnLevelLost()
    {
        EnableEnemies();
        StopMovement();
    }

    private void StartMovement()
    {
        if (movementCoroutine == null)
            movementCoroutine = StartCoroutine(MoveEnemies());
    }

    private void StopMovement()
    {
        if (movementCoroutine != null)
        {
            StopCoroutine(movementCoroutine);
            movementCoroutine = null;
        }

        foreach (Transform enemy in enemiesContainer)
        {
            Enemy enemyComponent = enemy.GetComponent<Enemy>();
            if (enemyComponent != null)
                enemyComponent.OnDestroyed -= OnEnemyDestroyed;
        }
    }


    private IEnumerator MoveEnemies()
    {
        while (true)
        {
            enemiesContainer.position += Vector3.right * direction * currentSpeed * Time.deltaTime;

            foreach (Transform enemy in enemiesContainer)
            {
                if (Mathf.Abs(enemy.position.x) >= distanceLimit)
                {
                    StepDown();
                    direction *= -1;
                    break;
                }
            }

            yield return null;
        }
    }

    private void EnableEnemies()
    {
        aliveEnemiesCount = 0;

        foreach (Transform enemy in enemiesContainer)
        {
            if (!enemy.gameObject.activeSelf)
                enemy.gameObject.SetActive(true);

            Enemy enemyComponent = enemy.GetComponent<Enemy>();
            if (enemyComponent != null)
            {
                RegisterEnemy(enemyComponent);
            }
        }
    }

    public void RegisterEnemy(Enemy enemy)
    {
        aliveEnemiesCount++;
        enemy.OnDestroyed -= OnEnemyDestroyed;
        enemy.OnDestroyed += OnEnemyDestroyed;
    }

    private void OnEnemyDestroyed(Enemy enemy)
    {
        aliveEnemiesCount--;

        if (aliveEnemiesCount <= 0)
            gameplayEntity.LevelWon();
    }

    private void StepDown()
    {
        enemiesContainer.position += Vector3.down * stepDownAmount;
    }
}
