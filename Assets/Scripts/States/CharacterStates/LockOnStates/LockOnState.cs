using System.Collections;
using UnityEngine;

namespace TMD
{
    public class LockOnState : State
    {
        protected LockOnStateMachine lockOnStateMachine;
        public LockOnState(LockOnStateMachine lockOnStateMachine, int stateIndex) : base(stateIndex)
        {
            this.lockOnStateMachine = lockOnStateMachine;
        }

        public override void Enter()
        {
            base.Enter();
        }

        public override void Exit()
        {
            base.Exit();
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
