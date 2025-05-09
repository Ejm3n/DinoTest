using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Slider slider;

    public void SetHealth(int current, int max)
    {
        slider.maxValue = max;
        slider.value = current;
        if(slider.value <= 0)
        {
           slider.gameObject.SetActive(false);
        }
        else
        {
            slider.gameObject.SetActive(true);
        }
    }
}
