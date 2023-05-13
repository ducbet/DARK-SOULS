using System;
using UnityEngine;

namespace TMD
{
    public class JumpState : ActionState
    {
        private string jumpFromIdleAnimationName = "Jumping Up From Idle";
        private int jumpFromIdleAnimation;
        private string runningJumpAnimationName = "Running Jump";
        private int runningJumpAnimation;
        private string moveForwardStateParamName = "MoveForwardState";
        private int moveForwardStateParam;
        private bool isIdleJump;

        public JumpState(ActionStateMachine actionStateMachine, int stateIndex) : base(actionStateMachine, stateIndex)
        {
            jumpFromIdleAnimation = actionStateMachine.animatorManager.HashString(jumpFromIdleAnimationName);
            runningJumpAnimation = actionStateMachine.animatorManager.HashString(runningJumpAnimationName); 
            moveForwardStateParam = actionStateMachine.animatorManager.HashString(moveForwardStateParamName);
        }

        public override void Enter()
        {
            base.Enter();
            actionStateMachine.animatorManager.EnableRootMotion();
            actionStateMachine.canStartFalling = false;
            actionStateMachine.animatorManager.SetFloatNoSmooth(moveForwardStateParam, 0f);

            if (Enum.Equals(actionStateMachine.GetCurrentMovementStateIndex(), (int)MovementStateMachine.MOVEMENT_STATE_ENUMS.Idle))
            {
                isIdleJump = true;
                actionStateMachine.PlayTargetAnimation(jumpFromIdleAnimation);
            }
            //else if (IsAssignableFromState<PlaneMoveState>(actionStateMachine.GetCurrentMovementState()))
            else
            {
                isIdleJump = false;
                actionStateMachine.PlayTargetAnimation(runningJumpAnimation);
            }
        }

        public override void Exit()
        {
            base.Exit();
            actionStateMachine.animatorManager.DisableRootMotion();
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
            if (IsStateChanged())
            {
                return;
            }
            if (actionStateMachine.animatorManager.isUsingRootMotion)
            {
                HandleRootMotionMovements();
            }
            if (actionStateMachine.canStartFalling)
            {
                if (!actionStateMachine.isGrounded || isIdleJump)
                {
                    actionStateMachine.SwitchState(ActionStateMachine.ACTION_STATE_ENUMS.Fall);
                    return;
                }
            }
            if (!actionStateMachine.isPlayingAnimation)
            {
                actionStateMachine.SwitchState(ActionStateMachine.ACTION_STATE_ENUMS.Empty);
                return;
            }
        }

        protected void HandleRootMotionMovements()
        {
            actionStateMachine.rgBody.drag = 0;
            Vector3 velocity = actionStateMachine.animatorManager.deltaPosition;
            velocity *= actionStateMachine.jumpUpVelocityScale;
            actionStateMachine.rgBody.velocity = velocity;
        }
    }
}
