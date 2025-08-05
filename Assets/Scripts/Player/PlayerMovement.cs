using UnityEngine;

public class PlayerMovement : MonoBehaviour, IPlayerComponent
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float movementSpeed = 500f;
    [SerializeField] private float distanceLimit = 10f;

    public bool ActionEnabled { get; set; }

    public void Initialize()
    {
        ActionEnabled = true;
        rb.gravityScale = 0f;
    }

    public void Conclude()
    {
        ActionEnabled = false;
        rb.linearVelocity = Vector2.zero;
    }

    private void Update()
    {
        if (!ActionEnabled) return;

        Move();
    }

    private void Move()
    {
        rb.linearVelocityX = PlayerInputReader.MovementInput.x * movementSpeed * Time.deltaTime;

        if (transform.position.x > distanceLimit || transform.position.x < -distanceLimit)
        {
            rb.linearVelocityX = 0f;
            Vector3 currentPosition = transform.position;
            currentPosition.x = Mathf.Sign(transform.position.x) * distanceLimit;
            transform.position = currentPosition;
        }
    }
}
