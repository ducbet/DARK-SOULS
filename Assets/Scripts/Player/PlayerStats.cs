using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TMD
{
    public class PlayerStats : MonoBehaviour
    {
        public int healthLevel = 10;
        public int maxHealth;
        public int currentHealth;

        public HealthBar healthBar;
        public AnimatorManager animatorManager;

        private void Awake()
        {
            if (healthBar == null)
            {
                healthBar = FindObjectOfType<HealthBar>();
            }
            animatorManager = GetComponent<AnimatorManager>();
        }
        private void Start()
        {
            SetMaxHealthFromHealthLevel();
        }
        private void SetMaxHealthFromHealthLevel()
        {
            maxHealth = healthLevel * 10;
            currentHealth = maxHealth;
            healthBar.SetMaxHealth(maxHealth);
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
            healthBar.SetCurrentHealth(currentHealth);
            if (!animatorManager.isInteracting)
            {
                animatorManager.PlayTargetAnimation(animatorManager.dodgeBackAnimation);
            }
        }
    }
}
