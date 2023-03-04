using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TMD
{
    [RequireComponent(typeof(Animator))]
    public class EnemyStats : CharacterStats
    {
        public Animator animator;
        private void Awake()
        {
            animator = GetComponent<Animator>();
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
                animator.Play("Die");
                return;
            }
            animator.Play("Dodge Back");
        }
    }
}
