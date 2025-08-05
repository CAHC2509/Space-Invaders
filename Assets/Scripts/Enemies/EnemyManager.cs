using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] private EnemyController enemyController;

    private GameplayEntity gameplayEntity;

    public void Dependencies(GameplayEntity gameplayEntity)
    {
        this.gameplayEntity = gameplayEntity;
        enemyController.Dependencies(gameplayEntity);
    }

    public void Initialize()
    {
        enemyController.Initialize();
    }

    public void Conclude()
    {
        enemyController.Conclude();
    }
}
