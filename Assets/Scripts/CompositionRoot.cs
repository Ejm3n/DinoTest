using UnityEngine;

public class CompositionRoot : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField] private Projectile projectilePrefab;
    [SerializeField] private Transform projectileContainer;
    [SerializeField] private UnityInputService inputService;
    [SerializeField] private NavMeshMovementService movementService;

    private void Awake()
    {
        var pool = new ObjectPool<Projectile>(projectilePrefab, 10, projectileContainer)
        {
            AutoExpand = true
        };

        IProjectileService projectile = new ProjectileFactory(pool);
        IInputService input = inputService;
        IMovementService movement = movementService;

        var controller = new GameController(input, projectile, movement);
    }
}
