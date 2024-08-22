using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    // Referências
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private Collider2D _collider;
    [SerializeField] private PlayerInputs _playerInputs;
    [SerializeField] private EquippedProjectileStruct _equippedProjectile;
    [SerializeField] private LineRenderer _lineRenderer;
    [SerializeField] private Slider _hpSlider;
    [SerializeField] private Slider _manaSlider;

    // Variáveis
    [SerializeField] private float _speed;
    [SerializeField] private int _maxLife = 10;  // Vida inicial do jogador
    private int _currentLife;
    [SerializeField] private int _maxMana = 10;
    private int _currentMana;
    [SerializeField] private float _manaRegenTime = 3;
    private bool _canCastMagic;
    private bool _isCastingLighting;


    // Logic 
    private void Start()
    {
        EventManager.OnPlayerShootEvent += CastMagic;
        EventManager.OnPlayerChangeMagicEvent += ChangePower;
        _canCastMagic = true;
        _isCastingLighting = false;

        _currentLife = _maxLife;
        _currentMana = _maxMana;

        _hpSlider.maxValue = _maxLife;
        _hpSlider.value = _currentLife;

        _manaSlider.maxValue = _maxMana;
        _manaSlider.value = _currentMana;

        StartCoroutine(ManaRestoration());
    }

    private void OnDestroy()
    {
        EventManager.OnPlayerShootEvent -= CastMagic;
    }

    private void Update()
    {
        // Movimento
        Vector2 inputVector = _playerInputs.GetMovementVectorValue();
        _rb.velocity = inputVector * _speed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemies") || collision.gameObject.CompareTag("ProjetilEnemy"))
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
        _currentLife -= damage;
        _hpSlider.value = _currentLife;
        Debug.Log("Vida restante do jogador: " + _maxLife);

        if (_currentLife <= 0)
        {
            EventManager.OnPlayerDeathTrigger();
            Destroy(gameObject);
            SceneManager.LoadScene("GameOver");
        }
    }

    private void ChangePower(int identifier)
    {
        _equippedProjectile.equippedMagic = identifier;
    }

    private void CastMagic()
    {
        if (_equippedProjectile.equippedMagic == 0)
        {
            CastFireBall();
        }
        else if (_equippedProjectile.equippedMagic == 1)
        {
            CastIceShard();
        }
    }

    private void CastFireBall()
    {
        if (!_canCastMagic) return;
        if (_currentMana <= 0) return;

        Debug.Log("cast fire");
        GameObject magic = Instantiate(_equippedProjectile.magicList[0], transform.position, Quaternion.identity);
        _currentMana -= 1;
        _manaSlider.value = _currentMana;
        StartCoroutine(MagicCoolDown(magic.GetComponent<Projectile>().coolDownTimer));
    }

    private void CastIceShard()
    {
        if (!_canCastMagic) return;
        if (_currentMana <= 0) return;

        Debug.Log("cast ice");

        int quantity = _equippedProjectile.magicList[1].GetComponent<IceShard>()._shardQuantity;
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = (mousePosition - (Vector2)transform.position).normalized;

        for (int i = 0; i < quantity; i++)
        {
            float angleOffset = (i - (quantity - 1) / 2f) * 25f;
            Vector2 newDirection = Quaternion.Euler(0, 0, angleOffset) * direction;

            GameObject magic = Instantiate(_equippedProjectile.magicList[1], transform.position, Quaternion.identity);
            Rigidbody2D rb = magic.GetComponent<Rigidbody2D>();
            rb.velocity = newDirection * magic.GetComponent<Projectile>().GetProjectileSpeed();
        }

        _currentMana -= 1;
        _manaSlider.value = _currentMana;
        StartCoroutine(MagicCoolDown(_equippedProjectile.magicList[1].GetComponent<Projectile>().coolDownTimer));
    }

    private IEnumerator MagicCoolDown(float timer)
    {
        _canCastMagic = false;
        yield return new WaitForSeconds(timer);
        _canCastMagic = true;
    }

    private IEnumerator ManaRestoration()
    {
        yield return new WaitForSeconds(_manaRegenTime);
        if (_currentMana < _maxMana) _currentMana += 1;
        _manaSlider.value = _currentMana;
        StartCoroutine(ManaRestoration());
    }
}
