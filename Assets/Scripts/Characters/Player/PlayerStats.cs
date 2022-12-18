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

        private PlayerMovementStateMachine playerMovementStateMachine;
        private int maxHealth;
        private int currentHealth;
        private int maxStamina;
        private int currentStamina;


        private void Awake()
        {
            playerMovementStateMachine = GetComponent<PlayerMovementStateMachine>();
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
                healthBar.SetValue(currentHealth);
                playerMovementStateMachine.SwitchState(MovementStateMachine.MOVEMENT_STATE_ENUMS.Die);
                return;
            }
            healthBar.SetValue(currentHealth);
            if (State.IsAssignableFromState<PlaneMoveState>(playerMovementStateMachine.currentState))
            {
                playerMovementStateMachine.SwitchState(MovementStateMachine.MOVEMENT_STATE_ENUMS.DodgingBack);
            }
        }

        public void DrainStamina(int staminaAmount)
        {
            currentStamina -= staminaAmount;
            staminaBar.SetValue(currentStamina);
        }
    }
}
