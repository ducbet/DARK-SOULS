using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TMD
{
    public class CharacterStats : MonoBehaviour
    {
        public int healthLevel = 10;
        public int staminaLevel = 10;
        protected int maxHealth;
        protected int currentHealth;
        protected int maxStamina;
        protected int currentStamina;

        public virtual void TakeDamage(int damageAmount)
        {

        }
    }
}
