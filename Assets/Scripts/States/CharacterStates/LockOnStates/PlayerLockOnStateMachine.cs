using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;



namespace TMD
{
    public class PlayerLockOnStateMachine : LockOnStateMachine
    {
        private InputManager inputManager;
        [HideInInspector] public CameraManager cameraManager;

        protected override void Awake()
        {
            base.Awake();
            inputManager = GetComponent<InputManager>();
            cameraManager = FindObjectOfType<CameraManager>();
        }

        protected override void InitStates()
        {
            //base.InitStates(); // Use PlayerLockOnStateMachine instead of LockOnStateMachine because Player's LockingOffState need cameraManager
            states = new State[Enum.GetNames(typeof(LOCK_ON_STATE_ENUMS)).Length];
            states[(int)LOCK_ON_STATE_ENUMS.LockingOn] = new LockingOnState(this, cameraTranform: Camera.main.transform);
            states[(int)LOCK_ON_STATE_ENUMS.LockingOff] = new LockingOffState(this);
        }

        protected override void Start()
        {
            base.Start();
            inputManager.playerControls.PlayerAction.LockOn.performed += SetIsLockingOnPerformed;
            inputManager.playerControls.PlayerAction.LockOnLeftTarget.performed += LockOnLeftTargetPerformed;
            inputManager.playerControls.PlayerAction.LockOnRightTarget.performed += LockOnRightTargetPerformed;
        }
        protected override void OnDestroy()
        {
            base.OnDestroy();
            inputManager.playerControls.PlayerAction.LockOn.performed -= SetIsLockingOnPerformed;
            inputManager.playerControls.PlayerAction.LockOnLeftTarget.performed -= LockOnLeftTargetPerformed;
            inputManager.playerControls.PlayerAction.LockOnRightTarget.performed -= LockOnRightTargetPerformed;
        }

        void OnDrawGizmos()
        {
            // Draw a yellow sphere at the transform's position
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(transform.position, Camera.main.transform.forward * 5);
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position + Camera.main.transform.forward * 5, 2);
        }

        private void SetIsLockingOnPerformed(InputAction.CallbackContext context)
        {
            if (isLockingOn)
            {
                SwitchState(LockOnStateMachine.LOCK_ON_STATE_ENUMS.LockingOff);
            }
            else
            {
                SwitchState(LockOnStateMachine.LOCK_ON_STATE_ENUMS.LockingOn);
            }
        }

        private void LockOnLeftTargetPerformed(InputAction.CallbackContext context)
        {
            if (isLockOnLeftTarget || isLockOnRightTarget)
            {
                return;
            }
            if (State.IsAssignableFromState<LockingOnState>(currentState))
            {
                isLockOnLeftTarget = true;
            }
        }
        private void LockOnRightTargetPerformed(InputAction.CallbackContext context)
        {
            if (isLockOnLeftTarget || isLockOnRightTarget)
            {
                return;
            }
            if (State.IsAssignableFromState<LockingOnState>(currentState))
            {
                isLockOnRightTarget = true;
            }
        }
    }
}
