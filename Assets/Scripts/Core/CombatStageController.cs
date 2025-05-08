using UnityEngine;

public class CombatStageController
{
    private readonly IInputService input;
    private readonly IProjectileService projectile;
    private readonly IWaypointService waypoints;
    private readonly ICharacterMover mover;
    private readonly IEnemySpawner enemySpawner;
    private readonly ITimer timer;

    private bool isCombatPhase;

    public CombatStageController(
        IInputService input,
        IProjectileService projectile,
        IWaypointService waypoints,
        ICharacterMover mover,
        IEnemySpawner enemySpawner,
        ITimer timer)
    {
        this.input = input;
        this.projectile = projectile;
        this.waypoints = waypoints;
        this.mover = mover;
        this.enemySpawner = enemySpawner;
        this.timer = timer;

        mover.OnArrived += OnWaypointReached;
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
        isCombatPhase = true;

        Vector3 basePoint = waypoints.GetCurrent() + Vector3.forward * 2f;
        enemySpawner.SpawnAt(basePoint);
        enemySpawner.SpawnAt(basePoint + Vector3.left * 2f);
        enemySpawner.SpawnAt(basePoint + Vector3.right * 2f);

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
