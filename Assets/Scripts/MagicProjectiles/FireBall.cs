using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : Projectile
{
    [SerializeField] protected GameObject _fireballExplosion;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Enemies") || other.gameObject.CompareTag("Wall"))
        {
            FireBallExplosion();
        }
    }

    private void FireBallExplosion()
    {
        GetComponent<Collider2D>().enabled = false;
        GetComponentInChildren<SpriteRenderer>().enabled = false;
        Instantiate(_fireballExplosion, transform.position, Quaternion.identity);
        Destroy(gameObject, 1f);
    }
}
