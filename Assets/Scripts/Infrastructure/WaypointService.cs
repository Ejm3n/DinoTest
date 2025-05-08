using System.Collections.Generic;
using UnityEngine;

public class WaypointService : MonoBehaviour, IWaypointService
{
    [SerializeField] private List<Transform> waypoints;

    private int index = 0;

    public Vector3 GetCurrent()
    {
        return waypoints[index].position;
    }

    public Vector3? GetNext()
    {
        if (index + 1 >= waypoints.Count)
            return null;

        return waypoints[index + 1].position;
    }

    public bool HasNext()
    {
        return index + 1 < waypoints.Count;
    }

    public void Advance()
    {
        if (HasNext())
        {
            index++;
        }
    }

    public void Reset()
    {
        index = 0;
    }
}
