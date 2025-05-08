using System;
using UnityEngine;

public interface ICharacterMover
{
    void MoveTo(Vector3 position);
    event Action OnArrived;
}
