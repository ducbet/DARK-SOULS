using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TMD
{
    public class IdleState : MovementState
    {
        public IdleState(MovementStateMachine movementStateMachine, int stateIndex) : base(movementStateMachine, stateIndex) { }

        public override void Enter()
        {
            base.Enter();
            movementStateMachine.rgBody.velocity = Vector3.zero;
            //SlowDownXZ();
        }

        public override void Exit()
        {
            base.Exit();
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
            if (IsStopMovement())
            {
                StopMoving();
                return;
            }
            if (movementStateMachine.moveMagnitude > 0)
            {
                movementStateMachine.SwitchState(MovementStateMachine.MOVEMENT_STATE_ENUMS.WalkingForward);
                return;
            }
            movementStateMachine.animatorManager.SetFloat(moveForwardStateParam, 0f);
            movementStateMachine.animatorManager.SetFloat(moveHorizontalStateParam, 0f);
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
