using System.Linq;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable
{
    [SerializeField] private int maxHealth = 3;
    private int currentHealth;

    private Rigidbody[] ragdollBodies;
    private Collider[] ragdollColliders;
    private Animator animator;

    public bool IsAlive => currentHealth > 0;
    public int CurrentHealth => currentHealth;
    public int MaxHealth => maxHealth;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        ragdollBodies = GetComponentsInChildren<Rigidbody>(includeInactive: true)
            .Where(rb => rb != GetComponent<Rigidbody>())
            .ToArray();

        ragdollColliders = ragdollBodies
            .Select(rb => rb.GetComponent<Collider>())
            .Where(col => col != null)
            .ToArray();

        SetRagdoll(false);
        currentHealth = maxHealth;
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;

        if (currentHealth <= 0)
            Die();
    }

    private void Die()
    {
        animator.enabled = false;
        SetRagdoll(true);
        gameObject.layer = LayerMask.NameToLayer("Ignore Raycast"); // можно игнорить коллизии
    }

    private void SetRagdoll(bool enabled)
    {
        foreach (var rb in ragdollBodies)
            rb.isKinematic = !enabled;

        foreach (var col in ragdollColliders)
            col.enabled = enabled;
    }

    private void OnEnable()
    {
        currentHealth = maxHealth;
        SetRagdoll(false);
        if (animator != null) animator.enabled = true;
    }
}
