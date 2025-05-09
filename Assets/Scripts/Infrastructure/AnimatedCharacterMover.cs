using System;
using UnityEngine;
using UnityEngine.AI;

public class AnimatedCharacterMover : MonoBehaviour, IMovementService
{
    public event Action OnReachedDestination;

    [SerializeField] private Animator visualAnimator; 

    private NavMeshAgent agent;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    public void MoveTo(Vector3 position)
    {
        agent.SetDestination(position);
        visualAnimator?.SetBool("IsMoving", true);
    }

    private void Update()
    {
        if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
        {
            if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
            {
                visualAnimator?.SetBool("IsMoving", false);
                OnReachedDestination?.Invoke();
            }
        }
    }
}
