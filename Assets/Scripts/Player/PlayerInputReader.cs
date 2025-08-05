using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputReader : MonoBehaviour
{
    [SerializeField] private InputActionReference movementInput;

    public static Vector2 MovementInput;

    private void OnEnable()
    {
        movementInput.action.performed += OnMovementInput;
        movementInput.action.canceled += OnMovementInput;
    }

    private void OnDisable()
    {
        movementInput.action.performed -= OnMovementInput;
        movementInput.action.canceled -= OnMovementInput;
    }

    private void OnMovementInput(InputAction.CallbackContext context) => MovementInput = context.action.ReadValue<Vector2>();
}
