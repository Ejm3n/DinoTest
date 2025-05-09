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

    public void SpawnAt(Enemy prefab, Vector3 position, int hp)
{
    var enemy = pool.GetFreeElement(); // если хочешь — пул на каждый префаб
    enemy.transform.position = position;
    enemy.transform.rotation = Quaternion.identity;
    enemy.SetHealth(hp); // добавим этот метод

    if (!activeEnemies.Contains(enemy))
        activeEnemies.Add(enemy);
}

    public bool AreAllEnemiesDead()
    {
        // Убираем мёртвых и null-ссылки
        activeEnemies.RemoveAll(e => e == null || !e.IsAlive);
        return activeEnemies.Count == 0;
    }
}
