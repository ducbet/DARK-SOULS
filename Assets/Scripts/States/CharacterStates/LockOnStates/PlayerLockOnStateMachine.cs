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
            states[(int)LOCK_ON_STATE_ENUMS.LockingOn] = new LockingOnState(this);
            states[(int)LOCK_ON_STATE_ENUMS.LockingOff] = new LockingOffState(this);
        }

        protected override void Start()
        {
            base.Start();
            inputManager.playerControls.PlayerAction.LockOn.performed += SetIsLockingOnPerformed;
        }
        protected override void OnDestroy()
        {
            base.OnDestroy();
            inputManager.playerControls.PlayerAction.LockOn.performed -= SetIsLockingOnPerformed;
        }

        private void SetIsLockingOnPerformed(InputAction.CallbackContext context)
        {
            if (State.IsAssignableFromState<LockingOnState>(currentState))
            {
                SwitchState(LockOnStateMachine.LOCK_ON_STATE_ENUMS.LockingOff);
            }
            else
            {
                SwitchState(LockOnStateMachine.LOCK_ON_STATE_ENUMS.LockingOn);
            }
        }
    }
}
