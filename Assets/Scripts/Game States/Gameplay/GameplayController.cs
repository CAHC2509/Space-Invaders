using System.Collections;
using UnityEngine;

public class GameplayController : ControllerBase
{
    [SerializeField] private BulletPool bulletPool;
    [SerializeField] private EnemyManager enemyManager;
    [SerializeField] private PlayerController playerPrefab;
    [SerializeField] private Transform playerSpawn;
    [SerializeField] private int maxLevel = 10;


    private PlayerController player;
    private GameplayEntity gameplayEntity;
    private PlayerEntity playerEntity;

    public void Dependencies(GameplayEntity gameplayEntity, PlayerEntity playerEntity)
    {
        this.gameplayEntity = gameplayEntity;
        this.playerEntity = playerEntity;

        if (player == null)
        {
            GameObject instantiatedObject = Instantiate(playerPrefab.gameObject, playerSpawn.position, playerSpawn.rotation);
            player = instantiatedObject.GetComponent<PlayerController>();
            player.Dependencies(playerEntity, bulletPool);
        }

        enemyManager.Dependencies(gameplayEntity);
    }

    public override void Initialize()
    {
        base.Initialize();

        enemyManager.Initialize();
        player.Initialize();
        bulletPool.Initialize();
    }

    public override void Conclude()
    {
        base.Conclude();

        enemyManager.Conclude();
        player.Conclude();
        bulletPool.Conclude();
    }

    protected override void AddListeners()
    {
        gameplayEntity.OnWarmingStart += DisablePlayerActions;
        gameplayEntity.OnWarmingStart += ResetPosition;
        gameplayEntity.OnWarmingStart += ResetScore;
        gameplayEntity.OnWarmingStart += ResetLifes;
        gameplayEntity.OnGameStart += EnablePlayerActions;
        gameplayEntity.OnLevelWon += NextLevel;
    }

    protected override void RemoveListeners()
    {
        gameplayEntity.OnWarmingStart -= DisablePlayerActions;
        gameplayEntity.OnWarmingStart -= ResetPosition;
        gameplayEntity.OnWarmingStart -= ResetScore;
        gameplayEntity.OnWarmingStart -= ResetLifes;
        gameplayEntity.OnGameStart -= EnablePlayerActions;
        gameplayEntity.OnLevelWon -= NextLevel;
    }

    private IEnumerator NextLevelRoutine()
    {
        DisablePlayerActions();
        ResetPosition();

        yield return new WaitForSeconds(1.5f);

        enemyManager.Conclude();
        enemyManager.Initialize();

        EnablePlayerActions();
    }

    private void NextLevel()
    {
        if (gameplayEntity.CurrentLevel > maxLevel)
        {
            gameplayEntity.GameWon();
            return;
        }

        StartCoroutine(NextLevelRoutine());
    }

    private void EnablePlayerActions() => player.EnableActions();
    private void DisablePlayerActions() => player.DisableActions();
    private void ResetPosition() => player.transform.position = playerSpawn.position;
    private void ResetScore() => playerEntity.ResetScore();
    private void ResetLifes() => playerEntity.ResetLifes();
}
