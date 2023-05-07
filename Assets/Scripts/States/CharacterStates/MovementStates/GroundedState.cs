using UnityEngine;

namespace TMD
{
    public class GroundedState : MovementState
    {
        public GroundedState(MovementStateMachine movementStateMachine, int stateIndex) : base(movementStateMachine, stateIndex)
        {
        }

        public override void Enter()
        {
            base.Enter();
        }
        public override void Update()
        {
            base.Update();
            //if (IsStateChanged())
            //{
            //    return;
            //}
            //if (IsStopMovement())
            //{
            //    StopMoving();
            //    return;
            //}
            //movementStateMachine.CalculateMoveDirection();
        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();
            if (IsStateChanged())
            {
                return;
            }
            //if (movementStateMachine.isRotationBlocked == false)
            //{
            //    HandleRotation();
            //}
        }
        
        public override void LateUpdate()
        {
            base.LateUpdate();
        }

        public override void Exit()
        {
            base.Exit();
        }
    }
}
