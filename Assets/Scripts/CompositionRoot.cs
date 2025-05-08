using UnityEngine;

public class CompositionRoot : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField] private Projectile projectilePrefab;
    [SerializeField] private Transform projectileContainer;
    [SerializeField] private Transform shootOrigin;

    [SerializeField] private UnityInputService inputService;
    [SerializeField] private NavMeshMovementService movementService;
    [SerializeField] private WaypointService waypointService;
    [SerializeField] private NavMeshCharacterMover characterMover;

    [SerializeField] private ProjectileConfig projectileConfig;

    private void Awake()
    {
        // ? ���������� ��� ���������� � pool
        var pool = new ObjectPool<Projectile>(projectilePrefab, 10, projectileContainer)
        {
            AutoExpand = true
        };

        // ? ������� shootOrigin, projectileConfig, � pool
        var projectileService = new ProjectileFactory(pool, shootOrigin, projectileConfig);

        // ? ���� ��������� ����������
        IInputService input = inputService;
        IMovementService movement = movementService;

        // ? ���������� ��� ���������� projectile -> projectileService
        var controller = new GameController(input, projectileService, movement);

        var waypointNav = new WaypointNavigator(waypointService, characterMover);
        waypointNav.StartPath();
    }
}
