using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputs : MonoBehaviour
{
    private PlayerInputActions _playerInputActions;
    private Animator _animator;
    private SpriteRenderer _spriteRenderer;


    private void Awake()
    {
        _playerInputActions = new PlayerInputActions();
        _playerInputActions.Player.Enable();
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void UpdateMovementAnimation()
    {
        Vector2 movementInput = GetMovementVectorValue();
        
        if (movementInput.y > 0)
        {
            _animator.SetTrigger("MovingForward");
        }
        else if (movementInput.y < 0)
        {
            _animator.SetTrigger("MovingBack");
        }
        else if (movementInput.x > 0)
        {
            _animator.SetTrigger("MovingRight");
            _spriteRenderer.flipX = false;
        }
        else if (movementInput.x < 0)
        {
            _animator.SetTrigger("MovingLeft");
            _spriteRenderer.flipX = true;
        }
        else
        {
            _animator.SetTrigger("Idle");
        }
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
    }

    public Vector2 GetMovementVectorValue()
    {
        Vector2 input = _playerInputActions.Player.Movement.ReadValue<Vector2>();

        input = input.normalized;

        return input;
    }
}
