using UnityEngine;

public interface IEnemySpawner
{
    void SpawnAt(Vector3 position);
    bool AreAllEnemiesDead();
}
