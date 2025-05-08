using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 offset;

    private Camera cam;

    private void Start()
    {
        cam = Camera.main;
    }

    private void LateUpdate()
    {
        if (target != null)
        {
            transform.position = cam.WorldToScreenPoint(target.position + offset);
        }
    }

    public void SetHealth(int current, int max)
    {
        slider.maxValue = max;
        slider.value = current;
    }
}
