using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovementSystem : MonoBehaviour
{
    [SerializeField] private Transform enemiesContainer;
    [SerializeField] private float stepDownAmount = 0.5f;
    [SerializeField] private float distanceLimit = 9f;

    private float currentSpeed;
    private int direction = 1;
    private Coroutine movementCoroutine;

    private Dictionary<Transform, Vector3> initialPositions = new();

    public void SetSpeed(float speed) => currentSpeed = speed;

    public void CacheInitialPositions()
    {
        foreach (Transform enemy in enemiesContainer)
            initialPositions[enemy] = enemy.localPosition;
    }

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

    public void ResetPosition()
    {
        enemiesContainer.position = Vector3.zero;

        foreach (var kvp in initialPositions)
        {
            if (kvp.Key != null)
                kvp.Key.localPosition = kvp.Value;
        }

        direction = 1;
    }

    private void StepDown() => enemiesContainer.position += Vector3.down * stepDownAmount;
}
