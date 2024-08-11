using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyStatic : EnemyController
{
    private GameObject _player;
    private ParticleSystem _particleSystem;
    public float attackInterval = 2.0f;
    private NavMeshSurface _navMeshSurface;

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _particleSystem = GetComponentInChildren<ParticleSystem>();
        _navMeshSurface = FindObjectOfType<NavMeshSurface>();

        InitializeAgent(); // Método da classe base

        StartCoroutine(AttackRoutine());
    }

    private void Update()
    {
        LookAtPlayer();
    }

    private void LookAtPlayer()
    {
        if (_player != null)
        {
            Vector3 direction = (_player.transform.position - transform.position).normalized;
            transform.rotation = Quaternion.LookRotation(direction);
        }
    }

    private IEnumerator AttackRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(attackInterval);
            LaunchParticles();
        }
    }

    private void LaunchParticles()
    {
        if (_particleSystem != null)
        {
            _particleSystem.Play();
        }
    }

    private void OnParticleCollision(GameObject other)
    {
        if (other.CompareTag("Player"))
        { D
            _particleSystem.Stop();
        }
    }

    private void LateUpdate()
    {
        if (_particleSystem != null)
        {
            var particles = new ParticleSystem.Particle[_particleSystem.main.maxParticles];
            int numParticlesAlive = _particleSystem.GetParticles(particles);

            for (int i = 0; i < numParticlesAlive; i++)
            {
                // Verifica se a partícula está fora do NavMesh
                if (!IsWithinNavMesh(particles[i].position))
                {
                    particles[i].remainingLifetime = 0; // Destroi a partícula
                }
            }

            _particleSystem.SetParticles(particles, numParticlesAlive);
        }
    }

    private bool IsWithinNavMesh(Vector3 position)
    {
        NavMeshHit hit;
        return NavMesh.SamplePosition(position, out hit, 1.0f, NavMesh.AllAreas);
    }
}
