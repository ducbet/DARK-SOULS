using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TMD
{
    public class LockingOffState : LockOnState
    {
        public LockingOffState(LockOnStateMachine lockOnStateMachine, int stateIndex) : base(lockOnStateMachine, stateIndex)
        {
        }

        public override void Enter()
        {
            base.Enter();
            Debug.Log("LockingOffState: Enter");
            lockOnStateMachine.lockOnTarget = null;
            lockOnStateMachine.isLockingOn = false;
        }

        public override void FixedUpdate()
        {
        }

        public override void LateUpdate()
        {
        }

        public override void Update()
        {
        }
    }
}
