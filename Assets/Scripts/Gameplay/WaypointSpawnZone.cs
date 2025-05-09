using UnityEngine;
using System.Collections.Generic;

public class WaypointSpawnZone : MonoBehaviour
{
    [System.Serializable]
    public class SpawnEntry
    {
        public Transform spawnPoint;
        public EnemySpawnConfig config;
    }

    [SerializeField] private List<SpawnEntry> entries;

    public IEnumerable<SpawnEntry> GetSpawns()
    {
        return entries;
    }
}
