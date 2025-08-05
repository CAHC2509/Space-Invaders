using UnityEngine;

public class GameplayController : ControllerBase
{
    private GameplayEntity gameplayEntity;
    private PlayerEntity playerEntity;

    public void Dependencies(GameplayEntity gameplayEntity, PlayerEntity playerEntity)
    {
        this.gameplayEntity = gameplayEntity;
        this.playerEntity = playerEntity;
    }

    protected override void AddListeners()
    {
        gameplayEntity.OnWarmingStart += ResetScore;
        gameplayEntity.OnWarmingStart += ResetLifes;
    }

    protected override void RemoveListeners()
    {
        gameplayEntity.OnWarmingStart -= ResetScore;
        gameplayEntity.OnWarmingStart -= ResetLifes;
    }

    private void ResetScore()
    {
        playerEntity.ResetScore();
    }

    private void ResetLifes()
    {
        playerEntity.ResetLifes();
    }
}
