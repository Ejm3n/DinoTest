using UnityEngine;

public class GameController
{
    private readonly IInputService inputService;
    private readonly IProjectileService projectileService;
    private readonly IMovementService movementService;

    public GameController(IInputService input, IProjectileService projectile, IMovementService movement)
    {
        inputService = input;
        projectileService = projectile;
        movementService = movement;

        Subscribe();
    }

    private void Subscribe()
    {
        inputService.OnTap += HandleTap;
        movementService.OnReachedDestination += HandleArrived;
    }

    private void HandleTap(Vector3 point)
    {
        projectileService.Shoot(point);
    }

    private void HandleArrived()
    {
        // –асстрел врагов, затем движение к следующей точке
    }
}
