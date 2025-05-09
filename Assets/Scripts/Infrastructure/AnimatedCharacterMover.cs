using System;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class AnimatedCharacterMover : MonoBehaviour, ICharacterMover
{
    public event Action OnArrived;

    [SerializeField] private CharacterAnimator characterAnimator;

    private NavMeshAgent agent;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    public void MoveTo(Vector3 position)
    {
        agent.SetDestination(position);
        characterAnimator?.SetMoving(true);
    }

    private void Update()
    {
        if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
        {
            if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
            {
                characterAnimator?.SetMoving(false);
                OnArrived?.Invoke();
            }
        }
    }
}
