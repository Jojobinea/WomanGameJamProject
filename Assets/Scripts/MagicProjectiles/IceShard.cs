using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceShard : Projectile
{
    public int _shardQuantity;

    override protected void Start()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.gameObject.CompareTag("Enemy") || other.gameObject.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }
    }
}
