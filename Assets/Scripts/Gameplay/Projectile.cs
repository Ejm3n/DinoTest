using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Vector3 target;
    private float speed = 15f;

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
        gameObject.SetActive(false);
        // Здесь можно уведомлять о попадании
    }
}
