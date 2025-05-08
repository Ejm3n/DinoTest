using System;
using UnityEngine;

public interface IMovementService
{
    void MoveTo(Vector3 position);
    event Action OnReachedDestination;
}
