using System.Collections;
using UnityEngine;

namespace TMD
{
    public class MovementState : State
    {
        protected MovementStateMachine movementStateMachine;
        protected MovementState(MovementStateMachine moveStateMachine)
        {
            movementStateMachine = moveStateMachine;
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
        }

        public override void FixedUpdate()
        {

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
            movementStateMachine.SwitchState(MovementStateMachine.MOVEMENT_STATE_ENUMS.Idle);
        }
    }
}