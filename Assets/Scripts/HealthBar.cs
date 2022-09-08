using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TMD
{
    public class HealthBar : MonoBehaviour
    {
        public Slider slider;

        private void Awake()
        {
            slider = GetComponent<Slider>();
        }

        public void SetMaxHealth(int maxValue)
        {
            slider.maxValue = maxValue;
            slider.value = maxValue;
        }

        public void SetCurrentHealth(int currentHealth)
        {
            slider.value = currentHealth;
        }
    }
}
