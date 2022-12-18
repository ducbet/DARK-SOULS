using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TMD
{
    public class RollingState : GroundedState
    {
        private string rollAnimationName = "Roll";
        private int rollAnimation;

        public RollingState(MovementStateMachine movementStateMachine) : base(movementStateMachine)
        {
            rollAnimation = base.movementStateMachine.animatorManager.HashString(rollAnimationName);
        }
        public override void Enter()
        {
            base.Enter();
            movementStateMachine.animatorManager.EnableRootMotion();
            movementStateMachine.PlayTargetAnimation(rollAnimation);
        }

        public override void Exit()
        {
            base.Exit();
            movementStateMachine.animatorManager.DisableRootMotion();
        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();
        }

        public override void LateUpdate()
        {
            base.LateUpdate();
        }

        public override void Update()
        {
            base.Update();
            if (IsStateChanged())
            {
                return;
            }
            if (movementStateMachine.animatorManager.isUsingRootMotion)
            {
                // falling to roll animation uses root motion
                HandleRootMotionMovements(movementStateMachine.rollingVelocityScale);
            }
            if (!movementStateMachine.isPlayingAnimation)
            {
                movementStateMachine.SwitchState(MovementStateMachine.MOVEMENT_STATE_ENUMS.Idle);
                return;
            }
        }
    }
}
