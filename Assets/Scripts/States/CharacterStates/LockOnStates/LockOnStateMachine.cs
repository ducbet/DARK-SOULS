using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TMD
{
    public class LockOnStateMachine : StateMachine
    {
        // debug
        public Text currentStateUI;

        public enum LOCK_ON_STATE_ENUMS
        {
            LockingOn,
            LockingOff
        };

        public bool isLockOnLeftTarget { get; set; } = false;
        public bool isLockOnRightTarget { get; set; } = false;

        protected virtual void Awake()
        {
            InitStates();
        }

        protected virtual void Start()
        {
            SwitchState(states[(int)LOCK_ON_STATE_ENUMS.LockingOff]);
        }

        protected override void Update()
        {
            base.Update();
            currentStateUI.text = currentState.GetType().ToString(); // Debug
        }

        void OnDrawGizmos()
        {
            // Draw a yellow sphere at the transform's position
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, transform.position + transform.forward * 5);
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position + transform.forward * 5, 2);
        }

        protected virtual void OnDestroy()
        {
        }
        protected virtual void InitStates()
        {
            states = new State[Enum.GetNames(typeof(LOCK_ON_STATE_ENUMS)).Length];
            states[(int)LOCK_ON_STATE_ENUMS.LockingOn] = new LockingOnState(this);
            states[(int)LOCK_ON_STATE_ENUMS.LockingOff] = new LockingOffState(this);
        }
    }
}
