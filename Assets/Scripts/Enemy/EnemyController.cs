using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    // References
    protected GameObject _player;
    private NavMeshAgent _agent;


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
}
