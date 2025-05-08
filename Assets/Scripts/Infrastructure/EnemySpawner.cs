using UnityEngine;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour, IEnemySpawner
{
    [SerializeField] private Enemy enemyPrefab;
    [SerializeField] private Transform container;

    private List<Enemy> spawned = new();

    public void SpawnAt(Vector3 position)
    {
        var enemy = Instantiate(enemyPrefab, position, Quaternion.identity, container);
        spawned.Add(enemy);
    }

    public bool AreAllEnemiesDead()
    {
        return spawned.TrueForAll(e => e == null || !e.IsAlive);
    }
}
