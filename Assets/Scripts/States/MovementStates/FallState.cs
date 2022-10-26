using UnityEngine;

namespace TMD
{
    public class FallState : State
    {
        private MovementStateMachine movementStateMachine;
        private float fallingTime;
        
        private string fallingAnimationName = "Falling";
        private int fallingAnimation;
        private string moveForwardStateParamName = "MoveForwardState";
        private int moveForwardStateParam;

        public FallState(MovementStateMachine movementStateMachine)
        {
            this.movementStateMachine = movementStateMachine;
            fallingAnimation = this.movementStateMachine.animatorManager.HashString(fallingAnimationName);
            moveForwardStateParam = this.movementStateMachine.animatorManager.HashString(moveForwardStateParamName);
        }

        public override void Enter()
        {
            base.Enter();
            ResetFallingTime();
            movementStateMachine.animatorManager.SetFloatNoSmooth(moveForwardStateParam, 0f);
            if (IsAssignableFromState<RollingState>(movementStateMachine.preState))
            {
                // pre state is RollingState -> fadeLength longer
                movementStateMachine.PlayTargetAnimation(fallingAnimation, fadeLength: 0.4f);
            }
            else
            {
                movementStateMachine.PlayTargetAnimation(fallingAnimationName);
            }
        }
        public override void Exit()
        {
            base.Exit();
            // ResetFallingTime() in LandedState
        }

        public override void Update()
        {
            if (IsStateChanged())
            {
                return;
            }
            if (movementStateMachine.isGrounded)
            {
                movementStateMachine.SwitchState(MovementStateMachine.MOVEMENT_STATE_ENUMS.Laned);
                return;
            }
            UpdateFallingTime();
        }

        public override void FixedUpdate()
        {
            if (IsStateChanged())
            {
                return;
            }
            HandleFallingForces();
        }

        public override void LateUpdate()
        {
            if (IsStateChanged())
            {
                return;
            }
        }

        public void ResetFallingTime()
        {
            fallingTime = 0;
        }

        public void UpdateFallingTime()
        {
            fallingTime += Time.deltaTime;
        }

        public float GetFallingTime()
        {
            return fallingTime;
        }

        private void HandleFallingForces()
        {
            SlowDownXZ();
            movementStateMachine.rgBody.AddForce(Vector3.down * movementStateMachine.fallingVelocity * fallingTime);
        }

        private void SlowDownXZ()
        {
            float velocityY = movementStateMachine.rgBody.velocity.y;
            Vector3 velocityXZ = Vector3.SmoothDamp(movementStateMachine.rgBody.velocity, Vector3.zero, ref movementStateMachine.leapingVelocity, movementStateMachine.leapingVelocitySmoothTime);
            velocityXZ.y = velocityY;
            movementStateMachine.rgBody.velocity = velocityXZ;
        }
    }
}
