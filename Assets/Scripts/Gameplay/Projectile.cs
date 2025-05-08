using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Projectile : MonoBehaviour
{
    private float speed;
    private int damage;
    private Vector3 target;

    public void Init(ProjectileConfig config)
    {
        speed = config.speed;
        damage = config.damage;
    }

    public void Launch(Vector3 target)
    {
        this.target = target;
        gameObject.SetActive(true);
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
        if (Vector3.Distance(transform.position, target) < 0.1f)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.TryGetComponent<IDamageable>(out var damageable))
        {
            damageable.TakeDamage(damage);
        }

        gameObject.SetActive(false);
    }
}
