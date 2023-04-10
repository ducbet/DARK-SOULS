using System.Collections;
using UnityEngine;

namespace TMD
{
    public class ActionState : State
    {
        protected ActionStateMachine actionStateMachine;
        public ActionState(ActionStateMachine actionStateMachine)
        {
            this.actionStateMachine = actionStateMachine;
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
        protected void HandleRootMotionMovements(float rollingVelocityScale = 1f, bool isIgnoreYAxisRootMotion = true)
        {
            actionStateMachine.rgBody.drag = 0;
            Vector3 velocity = actionStateMachine.animatorManager.deltaPosition * rollingVelocityScale;
            if (isIgnoreYAxisRootMotion)
            {
                velocity.y = actionStateMachine.rgBody.velocity.y;
            }
            actionStateMachine.rgBody.velocity = velocity;
        }

        public virtual bool isMovementBlocked()
        {
            return true;
        }
        public virtual bool isRotationBlocked()
        {
            return true;
        }
    }
}
