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

        AddListeners();
    }

    public void Conclude()
    {
        ActionEnabled = false;
        rb.linearVelocity = Vector2.zero;

        RemoveListeners();
    }

    private void AddListeners() => PlayerInputReader.OnMovementInput += Move;
    private void RemoveListeners() => PlayerInputReader.OnMovementInput -= Move;
    private void LateUpdate() => CheckLimits();

    private void Move(Vector2 movementInput)
    {
        if (!ActionEnabled) return;

        rb.linearVelocityX = movementInput.x * movementSpeed * Time.deltaTime;
    }

    private void CheckLimits()
    {
        if (transform.position.x > distanceLimit || transform.position.x < -distanceLimit)
        {
            rb.linearVelocityX = 0f;
            Vector3 currentPosition = transform.position;
            currentPosition.x = Mathf.Sign(transform.position.x) * distanceLimit;
            transform.position = currentPosition;
        }
    }
}
