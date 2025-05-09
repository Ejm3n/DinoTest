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
    [SerializeField] private WaypointService waypointService;
    [SerializeField] private AnimatedCharacterMover animatedMover;
private CombatStageController combatController;

    private void Awake()
    {
        var pool = new ObjectPool<Projectile>(projectilePrefab, 10, projectileContainer)
        {
            AutoExpand = true
        };

        var projectileService = new ProjectileFactory(pool, shootOrigin, projectileConfig);

        IInputService input = inputService;
        IMovementService movement = (IMovementService)animatedMover;

        combatController = new CombatStageController(
            input,
            projectileService,
            waypointService,
            animatedMover,
            enemySpawner,
            timer
        );
    }

    void Start()
    {
         combatController.StartGame();
    }
}
