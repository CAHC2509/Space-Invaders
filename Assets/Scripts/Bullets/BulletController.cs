using UnityEngine;

public class BulletController : PooledObject
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float speed;
    [SerializeField] private float moveDirection = 1f;

    private void FixedUpdate() => rb.linearVelocityY = speed * moveDirection;
}
