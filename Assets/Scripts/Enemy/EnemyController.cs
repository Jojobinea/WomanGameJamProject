using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    // References
    protected GameObject _player;
    private NavMeshAgent _agent;

    [SerializeField] protected int enemyHealth = 1;
    [SerializeField] protected Animator animator;


    protected void InitializeAgent()
    {
        _agent = GetComponent<NavMeshAgent>();
        _agent.updateRotation = false;
        _agent.updateUpAxis = false;
    }

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");

        InitializeAgent();
    }

    private void Update()
    {
        _agent.SetDestination(_player.transform.position);
    }

    protected virtual void TakeDamage(int damage)
    {
        enemyHealth -= damage;
        Debug.Log("Vida restante do inimigo: " + enemyHealth);

        if (enemyHealth <= 0)
        {
            animator.SetBool("isDead", true);
            _agent.speed = 0;
            GetComponent<Collider2D>().enabled = false;
        }
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("ProjetilPlayer"))
        {
            Debug.Log("Colidiu com o jogador");
            TakeDamage(1);  // Reduz a vida do inimigo em 1
        }
        else if (other.gameObject.CompareTag("Wall"))
        {
            Debug.Log("Colidiu com a parede");
        }
    }
    
}
