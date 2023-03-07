using UnityEngine;

namespace TMD
{
    public class GroundedState : State
    {
        protected MovementStateMachine movementStateMachine;

        protected GroundedState(MovementStateMachine moveStateMachine)
        {
            movementStateMachine = moveStateMachine;
        }
        public override void Enter()
        {
            base.Enter();
        }
        public override void Update()
        {
            if (IsStateChanged())
            {
                return;
            }
            if (!movementStateMachine.isGrounded)
            {
                movementStateMachine.SwitchState(MovementStateMachine.MOVEMENT_STATE_ENUMS.Fall);
                return;
            }
            movementStateMachine.CalculateMoveDirection();
        }

        public override void FixedUpdate()
        {
            if (IsStateChanged())
            {
                return;
            }
            HandleRotation();  // it's better if player can rotate while rolling,...
        }
        
        public override void LateUpdate()
        {
        }

        public override void Exit()
        {
            base.Exit();
        }

        protected void HandleRootMotionMovements(float rollingVelocityScale = 1f, bool isIgnoreYAxisRootMotion = true)
        {
            movementStateMachine.rgBody.drag = 0;
            Vector3 velocity = movementStateMachine.animatorManager.deltaPosition * rollingVelocityScale;
            if (isIgnoreYAxisRootMotion)
            {
                velocity.y = movementStateMachine.rgBody.velocity.y;
            }
            movementStateMachine.rgBody.velocity = velocity;
        }
        private void HandleRotation()
        {
            if (movementStateMachine.moveDirection == Vector3.zero)
            {
                return;
            }

            Quaternion lookDirection = Quaternion.identity;
            if (movementStateMachine.IsLockingOn())
            {
                Vector3 lockOnDirection = movementStateMachine.GetLockOnDirection();
                lockOnDirection.y = 0;
                lockOnDirection.Normalize();
                lookDirection = Quaternion.LookRotation(lockOnDirection);
            }
            else
            {
                lookDirection = Quaternion.LookRotation(movementStateMachine.moveDirection);
            }
            
            lookDirection = Quaternion.Slerp(movementStateMachine.transform.rotation, lookDirection, movementStateMachine.rotationSpeed * Time.deltaTime);
            movementStateMachine.transform.rotation = lookDirection;
        }
    }
}
