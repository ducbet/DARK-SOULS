using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TMD
{
    public class PlayerStats : MonoBehaviour
    {
        public int healthLevel = 10;
        public int staminaLevel = 10;
        public SliderBar healthBar;
        public SliderBar staminaBar;

        private AnimatorManager animatorManager;
        private int maxHealth;
        private int currentHealth;
        private int maxStamina;
        private int currentStamina;


        private void Awake()
        {
            animatorManager = GetComponent<AnimatorManager>();
        }
        private void Start()
        {
            SetMaxHealthFromHealthLevel();
            SetMaxStaminaFromStaminaLevel();
        }
        private void SetMaxHealthFromHealthLevel()
        {
            maxHealth = healthLevel * 10;
            currentHealth = maxHealth;
            healthBar.SetMaxHealth(maxHealth);
        }

        private void SetMaxStaminaFromStaminaLevel()
        {
            maxStamina = staminaLevel * 10;
            currentStamina = maxStamina;
            staminaBar.SetMaxHealth(maxStamina);
        }

        public void TakeDamage(int damageAmount)
        {
            currentHealth -= damageAmount;
            if (currentHealth <= 0)
            {
                currentHealth = 0;
                if (!animatorManager.isInteracting)
                {
                    animatorManager.PlayTargetAnimation(animatorManager.dieAnimation);
                    return;
                }
            }
            healthBar.SetValue(currentHealth);
            if (!animatorManager.isInteracting)
            {
                animatorManager.PlayTargetAnimation(animatorManager.dodgeBackAnimation);
            }
        }

        public void DrainStamina(int staminaAmount)
        {
            currentStamina -= staminaAmount;
            staminaBar.SetValue(currentStamina);
        }
    }
}
