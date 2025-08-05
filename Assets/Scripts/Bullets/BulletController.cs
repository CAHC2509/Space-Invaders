using UnityEngine;

public class BulletController : PooledObject
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float speed;
    [SerializeField] private float moveDirection = 1f;

    private void FixedUpdate() => rb.linearVelocityY = speed * moveDirection;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<IDamageable>(out var damageable))
        {
            damageable.OnHit();
            gameObject.SetActive(false);
        }
    }
}
