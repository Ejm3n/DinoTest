using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class HealthDisplayLink : MonoBehaviour
{
    [SerializeField] private HealthBar barPrefab;
    private HealthBar instance;
    private Canvas canvas;
    private Enemy enemy;

    private void Start()
    {
        enemy = GetComponent<Enemy>();
        canvas = FindObjectOfType<Canvas>();

        instance = Instantiate(barPrefab, canvas.transform);
        instance.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
    }

    private void Update()
    {
        instance.SetHealth(enemy.CurrentHealth, enemy.MaxHealth);
    }

    private void OnDisable()
    {
        if (instance != null)
        {
            Destroy(instance.gameObject);
        }
    }
}
