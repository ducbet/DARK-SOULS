using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TMD
{
    public class LockOnStateMachine : StateMachine
    {
        public enum LOCK_ON_STATE_ENUMS
        {
            LockingOn,
            LockingOff
        };
        [HideInInspector] public Transform lockOnTarget { get; set; }
        public bool isLockingOn { get; set; } = false;
        public bool isLockOnLeftTarget { get; set; } = false;
        public bool isLockOnRightTarget { get; set; } = false;

        [HideInInspector] public Transform selfLockOnPoint = null;

        private IEnumerator validateTargetCoroutine = null; // check target is obstacled, too far away,...

        protected virtual void Awake()
        {
            InitStates();
        }

        protected override void Start()
        {
            base.Start();
            SwitchState(states[(int)LOCK_ON_STATE_ENUMS.LockingOff]);
            selfLockOnPoint = transform.Find("LockOnPoint");
        }

        protected override void Update()
        {
            base.Update();
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
        public Vector3 GetLockOnDirection()
        {
            if (lockOnTarget == null)
            {
                return Vector3.zero;
            }
            return lockOnTarget.position - transform.position;
        }

        public void StartValidatingTarget()
        {
            validateTargetCoroutine = ValidateTarget();
            StartCoroutine(validateTargetCoroutine);
        }
        public void StopValidatingTarget()
        {
            if (validateTargetCoroutine == null)
            {
                return;
            }
            StopCoroutine(validateTargetCoroutine);
        }

        public IEnumerator ValidateTarget()
        {
            while (lockOnTarget != null)
            {
                if (((LockingOnState)states[(int)LOCK_ON_STATE_ENUMS.LockingOn]).IsTargetValid(lockOnTarget) == false)
                {
                    break;
                }
                yield return new WaitForSeconds(0.5f);
            }
            SwitchState(LOCK_ON_STATE_ENUMS.LockingOff);
        }
    }
}
