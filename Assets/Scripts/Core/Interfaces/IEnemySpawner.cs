using UnityEngine;

public interface IEnemySpawner
{
    void SpawnAt(Enemy prefab, Vector3 position, int hp);
    bool AreAllEnemiesDead();
}
