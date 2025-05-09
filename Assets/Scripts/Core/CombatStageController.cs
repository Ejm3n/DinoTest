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
    private bool hasStarted;

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
        SpawnAllEnemies();
        // Игрок ждёт тап, движение не запускается
    }

    private void SpawnAllEnemies()
    {
        var allZones = waypoints.GetAllSpawnZones();

        foreach (var zone in allZones)
        {
            foreach (var entry in zone.GetSpawns())
            {
                enemySpawner.SpawnAt(entry.config.enemyPrefab, entry.spawnPoint.position, entry.config.health, zone);
            }

            usedZones.Add(zone);
        }
    }

    private void OnTap(Vector3 point)
    {
        if (!hasStarted)
        {
            hasStarted = true;
            mover.MoveTo(waypoints.GetCurrent());
            return;
        }

        if (isCombatPhase)
        {
            projectile.Shoot(point);
        }
    }

    private void OnWaypointReached()
    {
        StartCombat();
    }

   private void StartCombat()
{
    if (isCombatPhase) return;

    var spawnZone = waypoints.GetCurrentSpawnZone();

    if (spawnZone == null)
    {
        EndCombat();
        return;
    }

    isCombatPhase = true;

    // ✅ ВСЕГДА ждём, пока враги на зоне будут убиты
    timer.WaitUntil(() => enemySpawner.AreEnemiesDeadAt(spawnZone), EndCombat);
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
