using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TMD
{
    public class PlayerStats : CharacterStats
    {
        public SliderBar healthBar;
        public SliderBar staminaBar;

        private PlayerActionStateMachine playerActionStateMachine;

        private void Awake()
        {
            playerActionStateMachine = GetComponent<PlayerActionStateMachine>();
        }
        private void Start()
        {
            healthBar = GameObject.Find("PlayerGUI").transform.Find("HealthBar").GetComponent<SliderBar>();
            staminaBar = GameObject.Find("PlayerGUI").transform.Find("StaminaBar").GetComponent<SliderBar>();
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

        public override void TakeDamage(int damageAmount)
        {
            currentHealth -= damageAmount;
            if (currentHealth <= 0)
            {
                currentHealth = 0;
                healthBar.SetValue(currentHealth);
                playerActionStateMachine.SwitchState(ActionStateMachine.ACTION_STATE_ENUMS.Die);
                return;
            }
            healthBar.SetValue(currentHealth);
            if (playerActionStateMachine.IsEmptyAction())
            {
                playerActionStateMachine.SwitchState(ActionStateMachine.ACTION_STATE_ENUMS.DodgingBack);
            }
        }

        public void DrainStamina(int staminaAmount)
        {
            currentStamina -= staminaAmount;
            staminaBar.SetValue(currentStamina);
        }
    }
}
