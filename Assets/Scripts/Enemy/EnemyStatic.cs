// EnemyStatic.cs
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyStatic : EnemyController
{
    private GameObject _player;
    private ParticleSystem _particleSystem;
    public float attackInterval = 2.0f;

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _particleSystem = GetComponentInChildren<ParticleSystem>();

        InitializeAgent();

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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
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
                if (!IsWithinNavMesh(particles[i].position))
                {
                    particles[i].remainingLifetime = 0;
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
