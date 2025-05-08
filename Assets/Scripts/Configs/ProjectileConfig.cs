using UnityEngine;

[CreateAssetMenu(fileName = "ProjectileConfig", menuName = "Configs/Projectile", order = 0)]
public class ProjectileConfig : ScriptableObject
{
    public float speed = 15f;
    public int damage = 1;
}
