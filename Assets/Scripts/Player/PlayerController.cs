using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Referências
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private Collider2D _collider;
    [SerializeField] private PlayerInputs _playerInputs;
    [SerializeField] private GameObject _magicProjectile;
    [SerializeField] private EquippedProjectileStruct _equippedMagic;

    // Variáveis
    [SerializeField] private float _speed;
    [SerializeField] private int _life = 10;  // Vida inicial do jogador
    private bool _canCastMagic;


    // Logic 
    private void Start()
    {
        EventManager.OnPlayerShootEvent += CreateBullet;
        EventManager.OnPlayerChangeMagicEvent += ChangePower;
        _canCastMagic = true;
    }

    private void OnDestroy()
    {
        EventManager.OnPlayerShootEvent -= CreateBullet;
    }

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

    private void ChangePower(int identifier)
    {
        _equippedMagic.equippedProjectile = _equippedMagic.magicList[identifier];
    }

    private void CreateBullet()
    {
        if (!_canCastMagic) return;

        Debug.Log("cast magic");
        GameObject magic = Instantiate(_magicProjectile, transform.position, Quaternion.identity);
        magic.GetComponent<Projectile>().SetProjectile(_equippedMagic.equippedProjectile);
        StartCoroutine(MagicCoolDown(magic.GetComponent<Projectile>().GetProjectileStats().coolDownTimer));
    }

    private IEnumerator MagicCoolDown(float timer)
    {
        _canCastMagic = false;
        yield return new WaitForSeconds(_equippedMagic.equippedProjectile.coolDownTimer);
        _canCastMagic = true;
    }
}
