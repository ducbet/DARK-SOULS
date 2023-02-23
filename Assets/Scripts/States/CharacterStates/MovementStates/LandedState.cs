using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TMD
{
    public class LandedState : GroundedState
    {
        private string fallingToRollAnimationName = "Falling To Roll";
        private int fallingToRollAnimation;
        private string landingAnimationName = "Landing";
        private int landingAnimation;

        public LandedState(MovementStateMachine movementStateMachine) : base(movementStateMachine) {
            fallingToRollAnimation = base.movementStateMachine.animatorManager.HashString(fallingToRollAnimationName);
            landingAnimation = base.movementStateMachine.animatorManager.HashString(landingAnimationName);
        }

        public override void Enter()
        {
            base.Enter();
            movementStateMachine.animatorManager.EnableRootMotion();

            if (((FallState)movementStateMachine.preState).GetFallingTime() > 1)
            {
                movementStateMachine.PlayTargetAnimation(fallingToRollAnimation);
            }
            else
            {
                movementStateMachine.PlayTargetAnimation(landingAnimation);
            }
            ((FallState)movementStateMachine.preState).ResetFallingTime();
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
