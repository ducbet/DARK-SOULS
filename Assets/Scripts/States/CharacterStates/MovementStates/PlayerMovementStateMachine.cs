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

        // TODO: will be moved to PlayerActionStateMachine in the future
        private void SetIsLeftClickPerformed(InputAction.CallbackContext context)
        {
            isLeftClick = true;
        }
        private void SetIsLeftClickCanceled(InputAction.CallbackContext context)
        {
            isLeftClick = false;
        }

        public override void CalculateMoveDirection()
        {
            base.CalculateMoveDirection();
            
            Vector3 _moveDirection = cameraTransform.forward * inputManager.playerMovement.y + cameraTransform.right * inputManager.playerMovement.x;
            _moveDirection.y = 0;
            moveDirection = Vector3.Normalize(_moveDirection);
        }

        public override void CalculateMoveMagnitude()
        {
            base.CalculateMoveMagnitude();
            moveMagnitude = inputManager.playerMovement.magnitude;
        }

        public override float GetPlayerMovementHorizontal()
        {
            return inputManager.playerMovement.x;
        }

        public override float GetPlayerMovementVertical()
        {
            return inputManager.playerMovement.y;
        }
    }
}
