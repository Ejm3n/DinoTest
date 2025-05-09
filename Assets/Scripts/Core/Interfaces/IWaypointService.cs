using System.Collections.Generic;
using UnityEngine;

public interface IWaypointService
{
    Vector3 GetCurrent();
    Vector3? GetNext();
    bool HasNext();
    void Advance();
    void Reset();
    WaypointSpawnZone GetCurrentSpawnZone();
    List<WaypointSpawnZone> GetAllSpawnZones();
}
