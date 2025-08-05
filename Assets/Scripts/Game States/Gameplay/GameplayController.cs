using UnityEngine;

public class GameplayController : ControllerBase
{
    [SerializeField] private PlayerController playerPrefab;
    [SerializeField] private Transform playerSpawn;

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
            player.Dependencies(playerEntity);
        }
    }

    public override void Initialize()
    {
        base.Initialize();

        player.Initialize();
    }

    public override void Conclude()
    {
        base.Conclude();

        player.Conclude();
    }

    protected override void AddListeners()
    {
        gameplayEntity.OnWarmingStart += DisablePlayerActions;
        gameplayEntity.OnWarmingStart += ResetPosition;
        gameplayEntity.OnWarmingStart += ResetScore;
        gameplayEntity.OnWarmingStart += ResetLifes;
        gameplayEntity.OnGameStart += EnablePlayerActions;
    }

    protected override void RemoveListeners()
    {
        gameplayEntity.OnWarmingStart -= DisablePlayerActions;
        gameplayEntity.OnWarmingStart -= ResetPosition;
        gameplayEntity.OnWarmingStart -= ResetScore;
        gameplayEntity.OnWarmingStart -= ResetLifes;
        gameplayEntity.OnGameStart -= EnablePlayerActions;
    }


    private void EnablePlayerActions() => player.EnableActions();
    private void DisablePlayerActions() => player.DisableActions();
    private void ResetPosition() => player.transform.position = playerSpawn.position;
    private void ResetScore() => playerEntity.ResetScore();
    private void ResetLifes() => playerEntity.ResetLifes();
}
