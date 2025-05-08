using UnityEngine;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour, IEnemySpawner
{
    [SerializeField] private Enemy enemyPrefab;
    [SerializeField] private Transform container;

    private ObjectPool<Enemy> pool;
    private readonly List<Enemy> activeEnemies = new();

    private void Awake()
    {
        pool = new ObjectPool<Enemy>(enemyPrefab, 10, container)
        {
            AutoExpand = true
        };
    }

    public void SpawnAt(Vector3 position)
    {
        var enemy = pool.GetFreeElement();
        enemy.transform.position = position;
        enemy.transform.rotation = Quaternion.identity;
        activeEnemies.Add(enemy);
    }

    public bool AreAllEnemiesDead()
    {
        return activeEnemies.TrueForAll(e => e == null || !e.IsAlive);
    }
}
