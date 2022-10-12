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

        public LayerMask interactableLayers;
        public GameObject interactableItem;
        public Coroutine checkForInteractableObject;

        [Header("Check For Interactable Object Attr")]
        public float checkObjectInterval = 0.5f;
        public float checkObjectRayThickness = 1f;
        public float checkObjectRayLength = 2f;
        private void Awake()
        {
            inputManager = GetComponent<InputManager>();
            playerLocomotion = GetComponent<PlayerLocomotion>();
            animatorManager = GetComponent<AnimatorManager>();
            playerAttacker = GetComponent<PlayerAttacker>();
            inventoryManager = GetComponent<InventoryManager>();
            playerStats = GetComponent<PlayerStats>();

            if (interactableLayers == 0)
            {
                interactableLayers = (int)~(CameraManager.LayerMasks.TransparentFX | CameraManager.LayerMasks.IgnoreRaycast |
                    CameraManager.LayerMasks.UI | CameraManager.LayerMasks.Controller | CameraManager.LayerMasks.Ground |
                    CameraManager.LayerMasks.Water | CameraManager.LayerMasks.Environment | CameraManager.LayerMasks.Player);
            }
        }

        private void Start()
        {
            inventoryManager.EquipLeftHandItems();
            inventoryManager.EquipRightHandItems();
            inputManager.playerControls.PlayerAction.LeftArrow.performed += HandleLeftHandQuickSlotInput;
            inputManager.playerControls.PlayerAction.RightArrow.performed += HandleRightHandQuickSlotInput;
            inputManager.playerControls.PlayerAction.Interact.performed += HandleInteractingObjectInput;

            checkForInteractableObject = StartCoroutine(CheckForInteractableObject());

        }

        private void OnDestroy()
        {
            inputManager.playerControls.PlayerAction.LeftArrow.performed -= HandleLeftHandQuickSlotInput;
            inputManager.playerControls.PlayerAction.RightArrow.performed -= HandleRightHandQuickSlotInput;
            inputManager.playerControls.PlayerAction.Interact.performed -= HandleInteractingObjectInput;
            StopCoroutine(checkForInteractableObject);  // OnDestroy is enough? also when player dead?
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

        public void HandleInteractingObjectInput(InputAction.CallbackContext context)
        {
            if (interactableItem == null)
            {
                return;
            }
            Interactable interactableComponent = interactableItem.GetComponent<Interactable>();
            if (interactableComponent == null)
            {
                return;
            }
            interactableComponent.Interact(new PickUpCommand(this));
        }

        public void PickUpItem(ItemObject item)
        {
            playerLocomotion.StopMovingXZ();  // Stop moving while picking up item
            animatorManager.PlayTargetAnimation(animatorManager.pickUpAnimation);
            inventoryManager.AddItemToInventory(item);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, transform.position + gameObject.transform.forward * 2f);
            Gizmos.DrawWireSphere(transform.position, 1f);
        }

        IEnumerator CheckForInteractableObject()
        {
            RaycastHit hit;
            while (true)
            {
                yield return new WaitForSeconds(checkObjectInterval);
                // why interactableLayers while the param is ignore layers???
                if (Physics.SphereCast(transform.position, checkObjectRayThickness, 
                    transform.forward, out hit, checkObjectRayLength, interactableLayers))
                {
                    if (hit.collider.gameObject.GetComponent<Interactable>() != null)
                    {
                        Debug.Log("interactableItem: " + hit.collider.name);
                        interactableItem = hit.collider.gameObject;
                        continue;
                    }
                }
                interactableItem = null;
            }
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
