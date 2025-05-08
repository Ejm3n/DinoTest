using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable
{
    [SerializeField] private int maxHealth = 3;
    private int currentHealth;
    public int CurrentHealth => currentHealth;
    public int MaxHealth => maxHealth;

    public bool IsAlive => currentHealth > 0;

    [SerializeField] private Rigidbody[] ragdollBones;
    [SerializeField] private Animator animator;

    private void Awake()
    {
        currentHealth = maxHealth;
        SetRagdoll(false);
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        animator.enabled = false;
        SetRagdoll(true);
        gameObject.layer = LayerMask.NameToLayer("Dead");
        // optionally: notify enemy manager
    }

    private void SetRagdoll(bool state)
    {
        foreach (var rb in ragdollBones)
        {
            rb.isKinematic = !state;
        }
    }

    private void OnEnable()
    {
        currentHealth = maxHealth;
        SetRagdoll(false);
        if (animator != null)
            animator.enabled = true;
    }
}
