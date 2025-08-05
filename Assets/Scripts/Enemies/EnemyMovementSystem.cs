using System.Collections;
using UnityEngine;

public class EnemyMovementSystem : MonoBehaviour
{
    [SerializeField] private Transform enemiesContainer;
    [SerializeField] private float stepDownAmount = 0.5f;
    [SerializeField] private float distanceLimit = 9f;

    private float currentSpeed;
    private int direction = 1;
    private Coroutine movementCoroutine;

    public void SetSpeed(float speed) => currentSpeed = speed;

    public void StartMovement()
    {
        if (movementCoroutine == null)
            movementCoroutine = StartCoroutine(MoveEnemies());
    }

    public void StopMovement()
    {
        if (movementCoroutine != null)
        {
            StopCoroutine(movementCoroutine);
            movementCoroutine = null;
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

    private void StepDown() => enemiesContainer.position += Vector3.down * stepDownAmount;
}
