using UnityEngine;

public class ProjectileFactory : IProjectileService
{
    private readonly ObjectPool<Projectile> pool;
    private readonly Transform shootOrigin;
    private readonly ProjectileConfig config;

    public ProjectileFactory(ObjectPool<Projectile> pool, Transform origin, ProjectileConfig config)
    {
        this.pool = pool;
        this.shootOrigin = origin;
        this.config = config;
    }

    public void Shoot(Vector3 targetPosition)
    {
        var projectile = pool.GetFreeElement();
        projectile.transform.position = shootOrigin.position;
        projectile.Init(config);
        projectile.Launch(targetPosition);
    }
}
