using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TMD
{
    [RequireComponent(typeof(AnimatorManager))]
    public class EnemyStats : CharacterStats
    {
        [HideInInspector] public AnimatorManager animatorManager;
        private void Awake()
        {
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
        }

        public override void TakeDamage(int damageAmount)
        {
            currentHealth -= damageAmount;
            if (currentHealth <= 0)
            {
                currentHealth = 0;
                animatorManager.PlayTargetAnimation("Die");
                return;
            }
            animatorManager.PlayTargetAnimation("Dodge Back");
        }
    }
}
