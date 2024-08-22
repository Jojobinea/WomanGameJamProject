using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    // References

    protected GameObject _player;
    private NavMeshAgent _agent;
    private bool _playerIsAlive = true;

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
        EventManager.OnPlayerDeathEvent += DetectPlayerDeath;

        _player = GameObject.FindGameObjectWithTag("Player");

        InitializeAgent();
    }

    private void Update()
    {
        if (_playerIsAlive)
            _agent.SetDestination(_player.transform.position);
        else
            _agent.SetDestination(transform.position);
    }

    private void OnDestroy()
    {
        EventManager.OnPlayerDeathEvent -= DetectPlayerDeath;


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
            TakeDamage(1);
        }
        else if (other.gameObject.CompareTag("Wall"))
        {
            Debug.Log("Colidiu com a parede");
        }
    }

    private void DetectPlayerDeath()
    {
        _playerIsAlive = false;
    }

}
