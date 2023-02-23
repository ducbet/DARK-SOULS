using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TMD
{
    public class LockingOffState : State
    {
        private LockOnStateMachine lockOnStateMachine;
        public LockingOffState(LockOnStateMachine lockOnStateMachine)
        {
            this.lockOnStateMachine = lockOnStateMachine;
        }

        public override void Enter()
        {
            base.Enter();
            Debug.Log("LockingOffState: Enter");
            if (lockOnStateMachine.GetType() == typeof(PlayerLockOnStateMachine))
            {
                ((PlayerLockOnStateMachine)lockOnStateMachine).cameraManager.lockOnTarget = null;
            }
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
