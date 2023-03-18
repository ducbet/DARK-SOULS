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
        }
        protected override void OnDestroy()
        {
            base.OnDestroy();
            inputManager.playerControls.PlayerMovement.Roll.performed -= SetIsRollingPerformed;
            inputManager.playerControls.PlayerMovement.Roll.canceled -= SetIsRollingCanceled;
        }

        private void SetIsRollingPerformed(InputAction.CallbackContext context)
        {
            isRolling = true;
        }
        private void SetIsRollingCanceled(InputAction.CallbackContext context)
        {
            isRolling = false;
        }
    }
}
