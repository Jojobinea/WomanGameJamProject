using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _sprite;
    [SerializeField] private Rigidbody2D _rb;
    private MagicProjectileDetail _magicProjectileStruct;

    private void Start()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Vector2 direction = (mousePos - _rb.position).normalized;

        _rb.velocity = direction * _magicProjectileStruct.speed;
    }

    public void SetProjectile(MagicProjectileDetail projectile)
    {
        _magicProjectileStruct = projectile;
        _sprite.color = _magicProjectileStruct.spriteColor;
    }

    public MagicProjectileDetail GetProjectileStats()
    {
        return _magicProjectileStruct;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Hit enemy");
            Destroy(gameObject);
        }
        else if(other.gameObject.CompareTag("Wall"))
        {
            Debug.Log("Hit wall");
            Destroy(gameObject);
        }
    }
}