using System;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshMovementService : MonoBehaviour, IMovementService
{
    public event Action OnReachedDestination;

    [SerializeField] private NavMeshAgent agent;

    public void MoveTo(Vector3 position)
    {
        agent.SetDestination(position);
    }

    private void Update()
    {
        if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
        {
            if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
            {
                OnReachedDestination?.Invoke();
            }
        }
    }
}
