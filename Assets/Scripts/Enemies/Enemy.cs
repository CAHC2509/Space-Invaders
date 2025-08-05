using UnityEngine;
using System;

public class Enemy : MonoBehaviour, IDamageable
{
    public event Action<Enemy> OnDestroyed;

    private void DestroyEnemy()
    {
        OnDestroyed?.Invoke(this);
        gameObject.SetActive(false);
    }

    public void OnHit() => DestroyEnemy();
}
