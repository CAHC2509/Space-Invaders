using UnityEngine;
using TMPro;

public class GameplayView : ViewBase
{
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI lifesText;
    [SerializeField] private TextMeshProUGUI levelText;

    private GameplayEntity gameplayEntity;
    private PlayerEntity playerEntity;

    public void Dependencies(GameplayEntity gameplayEntity, PlayerEntity playerEntity)
    {
        this.gameplayEntity = gameplayEntity;
        this.playerEntity = playerEntity;
    }

    protected override void AddListeners()
    {
        base.AddListeners();

        gameplayEntity.OnWarmingStart += UpdateLevelText;
        gameplayEntity.OnLevelWon += UpdateLevelText;
        playerEntity.OnScoreUpdated += UpdateScoreText;
        playerEntity.OnLifesUpdated += UpdateLifesText;
    }

    protected override void RemoveListeners()
    {
        base.RemoveListeners();

        gameplayEntity.OnWarmingStart -= UpdateLevelText;
        gameplayEntity.OnLevelWon -= UpdateLevelText;
        playerEntity.OnScoreUpdated -= UpdateScoreText;
        playerEntity.OnLifesUpdated -= UpdateLifesText;
    }

    private void UpdateScoreText() => scoreText.text = $"Score: {playerEntity.Score}";
    private void UpdateLifesText() => lifesText.text = $"Lifes: {playerEntity.Lifes}";
    private void UpdateLevelText() => levelText.text = $"Level: {gameplayEntity.CurrentLevel}";
}
