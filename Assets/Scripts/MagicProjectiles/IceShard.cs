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
        if(other.gameObject.CompareTag("Enemies") || other.gameObject.CompareTag("Wall"))
        {
            GetComponent<Collider2D>().enabled = false;
            GetComponentInChildren<SpriteRenderer>().enabled = false;
            Destroy(gameObject, 1f);
        }
    }
}
