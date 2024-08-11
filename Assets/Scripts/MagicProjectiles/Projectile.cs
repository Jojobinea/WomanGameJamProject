using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [Header("References")]
    [SerializeField] protected SpriteRenderer _sprite;
    [SerializeField] protected Rigidbody2D _rb;

    [Header("Variables")]
    [SerializeField] protected int _baseDamage;
    [SerializeField] protected int _currentDamage;
    [SerializeField] protected float _speed;
    public float coolDownTimer;


    protected virtual void Start()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Vector2 direction = (mousePos - _rb.position).normalized;

        _rb.velocity = direction * _speed;
    }

    public float GetProjectileSpeed()
    {
        return _speed;
    }
}