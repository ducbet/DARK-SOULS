using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace TMD
{
    public class PlayerActionStateMachine : ActionStateMachine
    {
        private InputManager inputManager;
        protected override void Awake()
        {
            base.Awake();
            inputManager = GetComponent<InputManager>();
        }

        protected override void Start()
        {
            base.Start();
            inputManager.playerControls.PlayerMovement.Roll.performed += SetIsRollingPerformed;
            inputManager.playerControls.PlayerMovement.Roll.canceled += SetIsRollingCanceled;
            inputManager.playerControls.PlayerAction.Interact.performed += SetIsInteractingObjectPerformed;
            inputManager.playerControls.PlayerAction.Interact.canceled += SetIsInteractingObjectCanceled;
            inputManager.playerControls.PlayerAction.Jump.performed += SetIsJumpingPerformed;
            inputManager.playerControls.PlayerAction.Jump.canceled += SetIsJumpingCanceled;
            inputManager.playerControls.PlayerAction.LeftClick.performed += SetIsLeftClickPerformed;
            inputManager.playerControls.PlayerAction.LeftClick.canceled += SetIsLeftClickCanceled;
        }
        protected override void OnDestroy()
        {
            base.OnDestroy();
            inputManager.playerControls.PlayerMovement.Roll.performed -= SetIsRollingPerformed;
            inputManager.playerControls.PlayerMovement.Roll.canceled -= SetIsRollingCanceled;
            inputManager.playerControls.PlayerAction.Interact.performed -= SetIsInteractingObjectPerformed;
            inputManager.playerControls.PlayerAction.Interact.canceled -= SetIsInteractingObjectCanceled;
            inputManager.playerControls.PlayerAction.Jump.performed -= SetIsJumpingPerformed;
            inputManager.playerControls.PlayerAction.Jump.canceled -= SetIsJumpingCanceled;
            inputManager.playerControls.PlayerAction.LeftClick.performed -= SetIsLeftClickPerformed;
            inputManager.playerControls.PlayerAction.LeftClick.canceled -= SetIsLeftClickCanceled;
        }

        private void SetIsRollingPerformed(InputAction.CallbackContext context)
        {
            isRolling = true;
        }
        private void SetIsRollingCanceled(InputAction.CallbackContext context)
        {
            isRolling = false;
        }
        private void SetIsInteractingObjectPerformed(InputAction.CallbackContext context)
        {
            isInteractingObject = true;
        }
        private void SetIsInteractingObjectCanceled(InputAction.CallbackContext context)
        {
            isInteractingObject = false;
        }
        private void SetIsJumpingPerformed(InputAction.CallbackContext context)
        {
            isJumping = true;
        }
        private void SetIsJumpingCanceled(InputAction.CallbackContext context)
        {
            isJumping = false;
        }
        private void SetIsLeftClickPerformed(InputAction.CallbackContext context)
        {
            isLeftClick = true;
        }
        private void SetIsLeftClickCanceled(InputAction.CallbackContext context)
        {
            isLeftClick = false;
        }
    }
}
