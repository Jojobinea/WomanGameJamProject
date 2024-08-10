using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputs : MonoBehaviour
{
    private PlayerInputActions _playerInputActions;

    private void Awake()
    {
        _playerInputActions = new PlayerInputActions();
        _playerInputActions.Player.Enable();
    }


    // Movement
    public Vector2 GetMovementVectorValue()
    {
        Vector2 input = _playerInputActions.Player.Movement.ReadValue<Vector2>();

        input = input.normalized;

        return input;
    }
}
