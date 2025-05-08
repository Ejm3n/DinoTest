using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float speed = 15f;
    [SerializeField] private int damage = 1;

    private Vector3 target;

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
