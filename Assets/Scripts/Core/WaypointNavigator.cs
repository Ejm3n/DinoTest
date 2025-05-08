using UnityEngine;

public class WaypointNavigator
{
    private readonly IWaypointService waypointService;
    private readonly ICharacterMover mover;

    public WaypointNavigator(IWaypointService waypointService, ICharacterMover mover)
    {
        this.waypointService = waypointService;
        this.mover = mover;
    }

    public void StartPath()
    {
        mover.MoveTo(waypointService.GetCurrent());
        mover.OnArrived += OnArrived;
    }

    private void OnArrived()
    {
        // ����� ����� ����������� ��������/��������/�������� ����� ��������� �����
        if (waypointService.HasNext())
        {
            waypointService.Advance();
            mover.MoveTo(waypointService.GetCurrent());
        }
        else
        {
            Debug.Log("Final waypoint reached � you can trigger level completion.");
        }
    }
}
