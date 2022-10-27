using UnityEngine;

namespace TMD
{
    public class JumpState : State
    {
        private MovementStateMachine movementStateMachine;

        private string jumpFromIdleAnimationName = "Jumping Up From Idle";
        private int jumpFromIdleAnimation;
        private string runningJumpAnimationName = "Running Jump";
        private int runningJumpAnimation;
        private string moveForwardStateParamName = "MoveForwardState";
        private int moveForwardStateParam;
        private bool isIdleJump;

        public JumpState(MovementStateMachine movementStateMachine)
        {
            this.movementStateMachine = movementStateMachine;
            jumpFromIdleAnimation = this.movementStateMachine.animatorManager.HashString(jumpFromIdleAnimationName);
            runningJumpAnimation = this.movementStateMachine.animatorManager.HashString(runningJumpAnimationName); 
            moveForwardStateParam = this.movementStateMachine.animatorManager.HashString(moveForwardStateParamName);
        }

        public override void Enter()
        {
            base.Enter();
            movementStateMachine.animatorManager.EnableRootMotion();
            movementStateMachine.canStartFalling = false;
            movementStateMachine.animatorManager.SetFloatNoSmooth(moveForwardStateParam, 0f);

            if (movementStateMachine.preState is IdleState)
            {
                isIdleJump = true;
                movementStateMachine.PlayTargetAnimation(jumpFromIdleAnimation);
            }
            else if (IsAssignableFromState<PlaneMoveState>(movementStateMachine.preState))
            {
                isIdleJump = false;
                movementStateMachine.PlayTargetAnimation(runningJumpAnimation);
            }
        }

        public override void Exit()
        {
            base.Exit();
            movementStateMachine.animatorManager.DisableRootMotion();
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
            if (movementStateMachine.animatorManager.isUsingRootMotion)
            {
                HandleRootMotionMovements();
            }
            if (movementStateMachine.canStartFalling)
            {
                if (!movementStateMachine.isGrounded || isIdleJump)
                {
                    movementStateMachine.SwitchState(MovementStateMachine.MOVEMENT_STATE_ENUMS.Fall);
                    return;
                }
            }
            if (!movementStateMachine.isPlayingAnimation)
            {
                movementStateMachine.SwitchState(MovementStateMachine.MOVEMENT_STATE_ENUMS.Idle);
                return;
            }
        }

        protected void HandleRootMotionMovements()
        {
            movementStateMachine.rgBody.drag = 0;
            Vector3 velocity = movementStateMachine.animatorManager.deltaPosition;
            //velocity.y *= jumpUpVelocityScale;  // velocity.y is always 0 because root transform Y
            movementStateMachine.rgBody.velocity = velocity;
        }
    }
}
