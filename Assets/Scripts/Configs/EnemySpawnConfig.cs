using UnityEngine;

[CreateAssetMenu(fileName = "EnemySpawnConfig", menuName = "Configs/Enemy Spawn", order = 0)]
public class EnemySpawnConfig : ScriptableObject
{
    public Enemy enemyPrefab;
    public int health = 3;
}
