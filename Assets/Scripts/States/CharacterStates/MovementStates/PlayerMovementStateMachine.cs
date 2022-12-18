using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace TMD
{
    [RequireComponent(typeof(InputManager))]
    public class PlayerMovementStateMachine : MovementStateMachine
    {
        private InputManager inputManager;
        private Transform cameraTransform;

        protected override void Awake()
        {
            base.Awake();
            inputManager = GetComponent<InputManager>();
            cameraTransform = Camera.main.transform;
        }
        protected override void Start()
        {
            base.Start();
            inputManager.playerControls.PlayerMovement.Sprint.performed += SetIsSprintingPerformed;
            inputManager.playerControls.PlayerMovement.Sprint.canceled += SetIsSprintingCanceled;
            inputManager.playerControls.PlayerMovement.Walk.performed += SetIsWalkingPerformed;
            inputManager.playerControls.PlayerMovement.Walk.canceled += SetIsWalkingCanceled;
            inputManager.playerControls.PlayerMovement.Roll.performed += SetIsRollingPerformed;
            inputManager.playerControls.PlayerMovement.Roll.canceled += SetIsRollingCanceled;
            inputManager.playerControls.PlayerAction.Interact.performed += SetIsInteractingObjectPerformed;
            inputManager.playerControls.PlayerAction.Interact.canceled += SetIsInteractingObjectCanceled;
            inputManager.playerControls.PlayerAction.Jump.performed += SetIsJumpingPerformed;
            inputManager.playerControls.PlayerAction.Jump.canceled += SetIsJumpingCanceled;

            // TODO: will be moved to PlayerActionStateMachine in the future
            inputManager.playerControls.PlayerAction.LeftClick.performed += SetIsLeftClickPerformed;
            inputManager.playerControls.PlayerAction.LeftClick.canceled += SetIsLeftClickCanceled;
        }
        protected override void OnDestroy()
        {
            base.OnDestroy();
            inputManager.playerControls.PlayerMovement.Sprint.performed -= SetIsSprintingPerformed;
            inputManager.playerControls.PlayerMovement.Sprint.canceled -= SetIsSprintingCanceled;
            inputManager.playerControls.PlayerMovement.Walk.performed -= SetIsWalkingPerformed;
            inputManager.playerControls.PlayerMovement.Walk.canceled -= SetIsWalkingCanceled;
            inputManager.playerControls.PlayerMovement.Roll.performed -= SetIsRollingPerformed;
            inputManager.playerControls.PlayerMovement.Roll.canceled -= SetIsRollingCanceled;
            inputManager.playerControls.PlayerAction.Interact.performed -= SetIsInteractingObjectPerformed;
            inputManager.playerControls.PlayerAction.Interact.canceled -= SetIsInteractingObjectCanceled;
            inputManager.playerControls.PlayerAction.Jump.performed -= SetIsJumpingPerformed;
            inputManager.playerControls.PlayerAction.Jump.canceled -= SetIsJumpingCanceled;

            // TODO: will be moved to PlayerActionStateMachine in the future
            inputManager.playerControls.PlayerAction.LeftClick.performed -= SetIsLeftClickPerformed;
            inputManager.playerControls.PlayerAction.LeftClick.canceled -= SetIsLeftClickCanceled;
        }
        private void SetIsSprintingPerformed(InputAction.CallbackContext context)
        {
            isSprinting = true;
        }
        private void SetIsSprintingCanceled(InputAction.CallbackContext context)
        {
            isSprinting = false;
        }
        private void SetIsWalkingPerformed(InputAction.CallbackContext context)
        {
            isWalking = true;
        }
        private void SetIsWalkingCanceled(InputAction.CallbackContext context)
        {
            isWalking = false;
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

        // TODO: will be moved to PlayerActionStateMachine in the future
        private void SetIsLeftClickPerformed(InputAction.CallbackContext context)
        {
            isLeftClick = true;
        }
        private void SetIsLeftClickCanceled(InputAction.CallbackContext context)
        {
            isLeftClick = false;
        }
        private void SetIsJumpingPerformed(InputAction.CallbackContext context)
        {
            isJumping = true;
        }
        private void SetIsJumpingCanceled(InputAction.CallbackContext context)
        {
            isJumping = false;
        }
        
        public override void CalculateMoveDirection()
        {
            base.CalculateMoveDirection();
            Vector3 _moveDirection = cameraTransform.forward * inputManager.playerMovementY + cameraTransform.right * inputManager.playerMovementX;
            _moveDirection.y = 0;
            moveDirection = _moveDirection;
            moveDirection.Normalize();
        }

        public override void CalculateMoveMagnitude()
        {
            base.CalculateMoveMagnitude();
            moveMagnitude = inputManager.playerMovement.magnitude;
        }
    }
}
