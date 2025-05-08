using UnityEngine;

public class CompositionRoot : MonoBehaviour
{
    [Header("Projectile Dependencies")]
    [SerializeField] private Projectile projectilePrefab;
    [SerializeField] private Transform projectileContainer;
    [SerializeField] private Transform shootOrigin;
    [SerializeField] private ProjectileConfig projectileConfig;

    [Header("Infrastructure")]
    [SerializeField] private UnityTimer timer;

    [Header("Gameplay Dependencies")]
    [SerializeField] private EnemySpawner enemySpawner;
    [SerializeField] private UnityInputService inputService;
    [SerializeField] private NavMeshMovementService movementService;
    [SerializeField] private WaypointService waypointService;
    [SerializeField] private NavMeshCharacterMover characterMover;

    private void Awake()
    {
        // Projectile pooling
        var pool = new ObjectPool<Projectile>(projectilePrefab, 10, projectileContainer)
        {
            AutoExpand = true
        };
        var projectileService = new ProjectileFactory(pool, shootOrigin, projectileConfig);

        // Interfaces
        IInputService input = inputService;
        IMovementService movement = movementService;

        // Combat flow controller
        var combatController = new CombatStageController(
            input, projectileService, waypointService, characterMover, enemySpawner, timer
        );

        combatController.StartGame();
    }
}
