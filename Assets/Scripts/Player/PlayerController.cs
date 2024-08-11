using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Referências
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private Collider2D _collider;
    [SerializeField] private PlayerInputs _playerInputs;

    // Variáveis
    [SerializeField] private float _speed;
    [SerializeField] private int _life = 10;  // Vida inicial do jogador

    private void Update()
    {
        // Movimento
        Vector2 inputVector = _playerInputs.GetMovementVectorValue();
        _rb.velocity = inputVector * _speed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemies"))
        {
            TakeDamage(1);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("ProjectilEnemy"))
        {
            TakeDamage(1);
        }
    }

    private void TakeDamage(int damage)
    {
        _life -= damage;
        Debug.Log("Vida restante do jogador: " + _life);

        if (_life <= 0)
        {
            Destroy(gameObject);
        }
    }
}
