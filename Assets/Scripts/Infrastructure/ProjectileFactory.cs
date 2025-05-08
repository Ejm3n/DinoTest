using UnityEngine;
using UnityEngine.Pool;

public class ProjectileFactory : IProjectileService
{
    private readonly ObjectPool<Projectile> pool;

    public ProjectileFactory(ObjectPool<Projectile> pool)
    {
        this.pool = pool;
    }

    public void Shoot(Vector3 targetPosition)
    {
        var projectile = pool.GetFreeElement();
        projectile.transform.position = Vector3.zero; // допустим, старт из world origin
        projectile.Launch(targetPosition);
    }
}
