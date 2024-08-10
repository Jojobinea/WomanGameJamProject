using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // References
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private Collider2D _collider;
    [SerializeField] private PlayerInputs _playerInputs;

    // Variables
    [SerializeField] private float _speed;


    private void Update()
    {
        // Movement related
        Vector2 inputVector = _playerInputs.GetMovementVectorValue();

        _rb.velocity = inputVector * _speed;
    }
}
