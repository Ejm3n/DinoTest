using System.Collections.Generic;
using UnityEngine;

public class WaypointSpawnZone : MonoBehaviour
{
    [SerializeField] private List<Transform> spawnPoints;

    public IEnumerable<Vector3> GetSpawnPositions()
    {
        foreach (var point in spawnPoints)
            yield return point.position;
    }
}
