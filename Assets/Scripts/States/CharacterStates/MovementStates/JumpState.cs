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

            //Debug.Log("((int)MovementStateMachine.MOVEMENT_STATE_ENUMS.Idle) " + ((int)MovementStateMachine.MOVEMENT_STATE_ENUMS.Idle));
            //Debug.Log("actionStateMachine.GetCurrentMovementStateIndex() " + actionStateMachine.GetCurrentMovementStateIndex());
            //Debug.Log("");
            //Debug.Log("Enum.Equals " + Enum.Equals(MovementStateMachine.MOVEMENT_STATE_ENUMS.Idle, ((int)MovementStateMachine.MOVEMENT_STATE_ENUMS.Idle)));
            //Debug.Log("Enum.Equals " + Enum.Equals(MovementStateMachine.MOVEMENT_STATE_ENUMS.Idle, 0));
            //Debug.Log("Enum.Equals " + Enum.Equals(0, ((int)MovementStateMachine.MOVEMENT_STATE_ENUMS.Idle)));
            //Debug.Log("Enum.Equals " + (MovementStateMachine.MOVEMENT_STATE_ENUMS.Idle == 0));
            //Debug.Log(actionStateMachine.GetCurrentMovementState());
            
            if (Enum.Equals(actionStateMachine.GetCurrentMovementStateIndex(), (int)MovementStateMachine.MOVEMENT_STATE_ENUMS.Idle))
            {
                isIdleJump = true;
                actionStateMachine.PlayTargetAnimation(jumpFromIdleAnimation);
            }
            else if (IsAssignableFromState<PlaneMoveState>(actionStateMachine.GetCurrentMovementState()))
            {
                isIdleJump = false;
                actionStateMachine.PlayTargetAnimation(runningJumpAnimation);
            }
            //Debug.Break();
            //actionStateMachine.PlayTargetAnimation(runningJumpAnimation);
            //actionStateMachine.PlayTargetAnimation(jumpFromIdleAnimation);
        }

        public override void Exit()
        {
            base.Exit();
            actionStateMachine.animatorManager.DisableRootMotion();
        }

        public override void FixedUpdate()
        {
        }

        public override void LateUpdate()
        {
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
            Debug.Log("JumpState actionStateMachine.canStartFalling " + actionStateMachine.canStartFalling);
            if (actionStateMachine.canStartFalling)
            {
                if (!actionStateMachine.isGrounded || isIdleJump)
                {
                    Debug.Log("JumpState !actionStateMachine.isGrounded " + !actionStateMachine.isGrounded);
                    actionStateMachine.SwitchState(ActionStateMachine.ACTION_STATE_ENUMS.Fall);
                    return;
                }
            }
            if (!actionStateMachine.isPlayingAnimation)
            {
                //Debug.Break();
                actionStateMachine.SwitchState(ActionStateMachine.ACTION_STATE_ENUMS.Empty);
                return;
            }
            //Debug.Log("actionStateMachine.canStartFalling " + actionStateMachine.canStartFalling);
            //Debug.Log("!actionStateMachine.isGrounded " + !actionStateMachine.isGrounded);
            //if (actionStateMachine.canStartFalling && !actionStateMachine.isGrounded)
            //{
            //    actionStateMachine.SwitchState(ActionStateMachine.ACTION_STATE_ENUMS.Fall);
            //    return;
            //}
            //if (!actionStateMachine.isPlayingAnimation)
            //{
            //    actionStateMachine.SwitchState(ActionStateMachine.ACTION_STATE_ENUMS.Idle);
            //    return;
            //}
        }

        protected void HandleRootMotionMovements()
        {
            actionStateMachine.rgBody.drag = 0;
            Vector3 velocity = actionStateMachine.animatorManager.deltaPosition;
            //velocity.y *= jumpUpVelocityScale;  // velocity.y is always 0 because root transform Y
            actionStateMachine.rgBody.velocity = velocity;
        }
    }
}
