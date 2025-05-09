using UnityEngine;

public interface IEnemySpawner
{
    void SpawnAt(Enemy prefab, Vector3 position, int hp, WaypointSpawnZone zone);
    bool AreAllEnemiesDead();
    bool AreEnemiesDeadAt(WaypointSpawnZone zone);
}
