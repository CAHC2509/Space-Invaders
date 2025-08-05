using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputReader : MonoBehaviour
{
    [SerializeField] private InputActionReference movementInput;
    [SerializeField] private InputActionReference shootInput;

    public static Action<Vector2> OnMovementInput;
    public static Action OnShootInput;

    private void OnEnable()
    {
        movementInput.action.performed += HandleMovementInput;
        movementInput.action.canceled += HandleMovementInput;

        shootInput.action.performed += HandleShootInput;
    }

    private void OnDisable()
    {
        movementInput.action.performed -= HandleMovementInput;
        movementInput.action.canceled -= HandleMovementInput;

        shootInput.action.performed -= HandleShootInput;
    }

    private void HandleMovementInput(InputAction.CallbackContext context) => OnMovementInput?.Invoke(context.action.ReadValue<Vector2>());
    private void HandleShootInput(InputAction.CallbackContext context) => OnShootInput?.Invoke();
}
