using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputs : MonoBehaviour
{
    private PlayerInputActions _playerInputActions;

    private void Awake()
    {
        _playerInputActions = new PlayerInputActions();
        _playerInputActions.Player.Enable();
    }

    private void Update()
    {
        if(_playerInputActions.Player.Shoot.IsPressed())
        {
            EventManager.OnPlayerShootTrigger();
        }

        if(_playerInputActions.Player.SelectFireball.IsPressed())
        {
            EventManager.OnPlayerChangeMagicTrigger(0);
        }
        else if(_playerInputActions.Player.SelectIceShard.IsPressed())
        {
            EventManager.OnPlayerChangeMagicTrigger(1);
        }
        else if(_playerInputActions.Player.SelectLightingBolt.IsPressed())
        {
            EventManager.OnPlayerChangeMagicTrigger(2);
        }
    }

    public Vector2 GetMovementVectorValue()
    {
        Vector2 input = _playerInputActions.Player.Movement.ReadValue<Vector2>();

        input = input.normalized;

        return input;
    }
}
