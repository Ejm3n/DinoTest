using System.Collections.Generic;
using UnityEngine;

public class CombatStageController
{
    private readonly IInputService input;
    private readonly IProjectileService projectile;
    private readonly IWaypointService waypoints;
    private readonly IMovementService mover;
    private readonly IEnemySpawner enemySpawner;
    private readonly ITimer timer;
    private readonly HashSet<WaypointSpawnZone> usedZones = new();
    private bool isCombatPhase;

    public CombatStageController(
        IInputService input,
        IProjectileService projectile,
        IWaypointService waypoints,
        IMovementService mover,
        IEnemySpawner enemySpawner,
        ITimer timer)
    {
        this.input = input;
        this.projectile = projectile;
        this.waypoints = waypoints;
        this.mover = mover;
        this.enemySpawner = enemySpawner;
        this.timer = timer;

        mover.OnReachedDestination += OnWaypointReached;
        input.OnTap += OnTap;
    }

    public void StartGame()
    {
        mover.MoveTo(waypoints.GetCurrent());
    }

    private void OnWaypointReached()
    {
        StartCombat();
    }

private void StartCombat()
{
    if (isCombatPhase) return;
    isCombatPhase = true;

    var spawnZone = waypoints.GetCurrentSpawnZone();

    if (spawnZone != null && !usedZones.Contains(spawnZone))
    {
        foreach (var pos in spawnZone.GetSpawnPositions())
        {
            enemySpawner.SpawnAt(pos); // ✅ только один раз
        }

        usedZones.Add(spawnZone);
    }

    timer.WaitUntil(() => enemySpawner.AreAllEnemiesDead(), EndCombat);
}


    private void OnTap(Vector3 point)
    {
        if (isCombatPhase)
            projectile.Shoot(point);
    }

    private void EndCombat()
    {
        isCombatPhase = false;

        if (waypoints.HasNext())
        {
            waypoints.Advance();
            mover.MoveTo(waypoints.GetCurrent());
        }
        else
        {
            SceneLoader.RestartCurrent();
        }
    }
}
