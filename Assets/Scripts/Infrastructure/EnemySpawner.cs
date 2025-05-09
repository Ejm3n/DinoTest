using UnityEngine;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour, IEnemySpawner
{
    [SerializeField] private Transform container;

    private readonly Dictionary<Enemy, ObjectPool<Enemy>> pools = new();
    private readonly Dictionary<WaypointSpawnZone, List<Enemy>> zoneEnemies = new();

    public void SpawnAt(Enemy prefab, Vector3 position, int hp, WaypointSpawnZone zone)
    {
        if (!pools.ContainsKey(prefab))
        {
            var pool = new ObjectPool<Enemy>(prefab, 5, container)
            {
                AutoExpand = true
            };
            pools[prefab] = pool;
        }

        var enemy = pools[prefab].GetFreeElement();
        enemy.transform.position = position;
        enemy.transform.rotation = Quaternion.identity;
        enemy.SetHealth(hp);

        if (!zoneEnemies.ContainsKey(zone))
        {
            zoneEnemies[zone] = new List<Enemy>();
        }

        if (!zoneEnemies[zone].Contains(enemy))
        {
            zoneEnemies[zone].Add(enemy);
        }
    }

    public bool AreEnemiesDeadAt(WaypointSpawnZone zone)
    {
        if (!zoneEnemies.ContainsKey(zone))
            return true;

        zoneEnemies[zone].RemoveAll(e => e == null || !e.IsAlive);
        return zoneEnemies[zone].Count == 0;
    }

    public bool AreAllEnemiesDead()
    {
        foreach (var kvp in zoneEnemies)
        {
            if (kvp.Value.Exists(e => e != null && e.IsAlive))
                return false;
        }
        return true;
    }
}
