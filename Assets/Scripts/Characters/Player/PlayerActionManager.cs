using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace TMD
{
    [RequireComponent(typeof(InputManager))]
    [RequireComponent(typeof(PlayerMovementStateMachine))]
    [RequireComponent(typeof(AnimatorManager))]
    [RequireComponent(typeof(InventoryManager))]
    [RequireComponent(typeof(PlayerStats))]
    public class PlayerActionManager : MonoBehaviour
    {
        [HideInInspector] public InputManager inputManager;
        [HideInInspector] public PlayerMovementStateMachine playerMovementStateMachine;
        [HideInInspector] public AnimatorManager animatorManager;
        [HideInInspector] public InventoryManager inventoryManager;
        [HideInInspector] public PlayerStats playerStats;

        private void Awake()
        {
            inputManager = GetComponent<InputManager>();
            playerMovementStateMachine = GetComponent<PlayerMovementStateMachine>();
            animatorManager = GetComponent<AnimatorManager>();
            inventoryManager = GetComponent<InventoryManager>();
            playerStats = GetComponent<PlayerStats>();
        }

        private void Start()
        {
            inputManager.playerControls.PlayerAction.LeftArrow.performed += HandleLeftHandQuickSlotInput;
            inputManager.playerControls.PlayerAction.RightArrow.performed += HandleRightHandQuickSlotInput;
        }

        private void OnDestroy()
        {
            inputManager.playerControls.PlayerAction.LeftArrow.performed -= HandleLeftHandQuickSlotInput;
            inputManager.playerControls.PlayerAction.RightArrow.performed -= HandleRightHandQuickSlotInput;
        }

        public void HandleRightHandQuickSlotInput(InputAction.CallbackContext context)
        {
            inventoryManager.SwitchRightHandItems();
        }

        public void HandleLeftHandQuickSlotInput(InputAction.CallbackContext context)
        {
            inventoryManager.SwitchLeftHandItems();
        }
    }
}
