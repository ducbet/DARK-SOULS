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
        }
        protected override void OnDestroy()
        {
            base.OnDestroy();
            inputManager.playerControls.PlayerMovement.Roll.performed -= SetIsRollingPerformed;
            inputManager.playerControls.PlayerMovement.Roll.canceled -= SetIsRollingCanceled;
            inputManager.playerControls.PlayerAction.Interact.performed -= SetIsInteractingObjectPerformed;
            inputManager.playerControls.PlayerAction.Interact.canceled -= SetIsInteractingObjectCanceled;
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
    }
}
