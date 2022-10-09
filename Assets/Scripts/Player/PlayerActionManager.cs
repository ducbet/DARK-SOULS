using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace TMD
{
    [RequireComponent(typeof(InputManager))]
    [RequireComponent(typeof(PlayerLocomotion))]
    [RequireComponent(typeof(AnimatorManager))]
    [RequireComponent(typeof(PlayerAttacker))]
    [RequireComponent(typeof(InventoryManager))]
    [RequireComponent(typeof(PlayerStats))]
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
        [HideInInspector] public PlayerStats playerStats;

        private void Awake()
        {
            inputManager = GetComponent<InputManager>();
            playerLocomotion = GetComponent<PlayerLocomotion>();
            animatorManager = GetComponent<AnimatorManager>();
            playerAttacker = GetComponent<PlayerAttacker>();
            inventoryManager = GetComponent<InventoryManager>();
            playerStats = GetComponent<PlayerStats>();
        }

        private void Start()
        {
            inventoryManager.EquipLeftHandItems();
            inventoryManager.EquipRightHandItems();
            inputManager.playerControls.PlayerAction.LeftArrow.performed += HandleLeftHandQuickSlotInput;
            inputManager.playerControls.PlayerAction.RightArrow.performed += HandleRightHandQuickSlotInput;
        }

        private void OnDestroy()
        {
            inputManager.playerControls.PlayerAction.LeftArrow.performed -= HandleLeftHandQuickSlotInput;
            inputManager.playerControls.PlayerAction.RightArrow.performed -= HandleRightHandQuickSlotInput;
        }

        public void HandleAllActions()
        {
            HandleSingleAttack();
            HandleRollingOrDodgeBack();
        }
        public void HandleRightHandQuickSlotInput(InputAction.CallbackContext context)
        {
            inventoryManager.SwitchRightHandItems();
        }

        public void HandleLeftHandQuickSlotInput(InputAction.CallbackContext context)
        {
            inventoryManager.SwitchLeftHandItems();
        }

        private void HandleSingleAttack()
        {
            if (animatorManager.isInteracting)
            {
                return;
            }
            if (inputManager.isLeftClick)
            {
                playerAttacker.ClearComboAttack();
                Attack();
            }
        }

        private void Attack()
        {
            playerAttacker.Attack();
            playerStats.DrainStamina(playerAttacker.GetAttackStaminaCost(playerAttacker.lastAttackName));
        }

        public void HandleComboAttack()
        {
            if (inputManager.isLeftClick)
            {
                Attack();
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
