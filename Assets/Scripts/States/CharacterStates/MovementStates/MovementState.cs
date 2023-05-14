using System.Collections;
using UnityEngine;

namespace TMD
{
    public class MovementState : State
    {
        protected MovementStateMachine movementStateMachine;

        protected string moveForwardStateParamName = "MoveForwardState";
        protected string moveHorizontalStateParamName = "MoveHorizontalState";
        protected int moveForwardStateParam;
        protected int moveHorizontalStateParam;
        protected MovementState(MovementStateMachine moveStateMachine, int stateIndex) : base(stateIndex)
        {
            this.movementStateMachine = moveStateMachine; 
            moveForwardStateParam = moveStateMachine.animatorManager.HashString(moveForwardStateParamName);
            moveHorizontalStateParam = moveStateMachine.animatorManager.HashString(moveHorizontalStateParamName);

        }
        public override void Enter()
        {
            base.Enter();
        }
        public override void Exit()
        {
            base.Exit();
        }
        public override void Update()
        {
            if (IsStateChanged())
            {
                return;
            }
            if (IsStopMovement())
            {
                StopMoving();
                return;
            }
            movementStateMachine.CalculateMoveDirection();
            movementStateMachine.CalculateMoveMagnitude();
        }

        public override void FixedUpdate()
        {
            if (IsStateChanged())
            {
                return;
            }
            if (movementStateMachine.isRotationBlocked == false)
            {
                HandleRotation();
            }
        }

        public override void LateUpdate()
        {
        }
        protected bool IsStopMovement()
        {
            return movementStateMachine.isMovementBlocked;
        }

        protected void StopMoving()
        {
            if (movementStateMachine.currentState.stateIndex != (int)MovementStateMachine.MOVEMENT_STATE_ENUMS.Empty)
            {
                movementStateMachine.SwitchState(MovementStateMachine.MOVEMENT_STATE_ENUMS.Empty);
            }
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
    }
}