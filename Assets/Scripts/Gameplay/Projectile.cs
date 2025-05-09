using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Projectile : MonoBehaviour
{
    [SerializeField] private float lifeTime = 5f;

    private float speed;
    private int damage;
    private Vector3 direction;
    private float lifeTimer;

    public void Init(ProjectileConfig config)
    {
        speed = config.speed;
        damage = config.damage;
    }

    public void Launch(Vector3 targetPosition)
    {
        direction = (targetPosition - transform.position).normalized;
        lifeTimer = lifeTime;
        gameObject.SetActive(true);
    }

    private void Update()
    {
        transform.position += direction * speed * Time.deltaTime;

        lifeTimer -= Time.deltaTime;
        if (lifeTimer <= 0f)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("OnCollisionEnter: " + collision.gameObject.name);

        if (collision.collider.TryGetComponent<IDamageable>(out var damageable))
        {
            damageable.TakeDamage(damage);
        }

        gameObject.SetActive(false);
    }
}
