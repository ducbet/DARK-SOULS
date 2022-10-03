using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TMD
{
    [RequireComponent(typeof(InputManager))]
    [RequireComponent(typeof(PlayerLocomotion))]
    [RequireComponent(typeof(AnimatorManager))]
    [RequireComponent(typeof(PlayerAttacker))]
    public class PlayerActionManager : MonoBehaviour
    {
        /*
        Control all actions that are not related to locomotion 
        */

        [HideInInspector] public InputManager inputManager;
        [HideInInspector] public PlayerLocomotion playerLocomotion;
        [HideInInspector] public AnimatorManager animatorManager;
        [HideInInspector] public PlayerAttacker playerAttacker;
        [HideInInspector] public InventoryManager inventoryManager;

        private void Awake()
        {
            inputManager = GetComponent<InputManager>();
            playerLocomotion = GetComponent<PlayerLocomotion>();
            animatorManager = GetComponent<AnimatorManager>();
            playerAttacker = GetComponent<PlayerAttacker>();
            inventoryManager = GetComponent<InventoryManager>();
        }

        private void Start()
        {
            inventoryManager.EquipItems();
        }

        public void HandleAllActions()
        {
            HandleAttack();
            HandleRollingOrDodgeBack();
        }

        private void HandleAttack()
        {
            if (animatorManager.isInteracting)
            {
                return;
            }
            if (inputManager.isLeftClick)
            {
                Attack();
            }
        }

        private void Attack()
        {
            playerAttacker.ClearComboAttack();
            playerAttacker.Attack();
        }
        public void HandleComboAttack()
        {
            if (inputManager.isLeftClick)
            {
                playerAttacker.Attack();
            }
        }

        private void HandleRollingOrDodgeBack()
        {
            if (animatorManager.isInteracting)
            {
                return;
            }
            if (!inputManager.isRolling)
            {
                return;
            }
            if (playerLocomotion.GetMovementState() == PlayerLocomotion.MOVEMENT_STATE.Idle)
            {
                animatorManager.PlayTargetAnimation(animatorManager.dodgeBackAnimation);
            }
            else
            {
                animatorManager.PlayTargetAnimation(animatorManager.rollAnimation);
            }
        }
    }
}
