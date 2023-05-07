using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TMD
{
    public class PlaneMoveState : GroundedState
    {
        

        public PlaneMoveState(MovementStateMachine movementStateMachine, int stateIndex) : base(movementStateMachine, stateIndex)
        {
                    }
        public override void Enter()
        {
            base.Enter();
            //movementStateMachine.animatorManager.PlayTargetAnimation("Empty");
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
            //if (IsStopMovement() && IsAssignableFromState<IdleState>(movementStateMachine.currentState) == false)
            //{
            //    StopMoving();
            //    return;
            //}
            //if (movementStateMachine.isLeftClick)
            //{
            //    movementStateMachine.SwitchState(MovementStateMachine.MOVEMENT_STATE_ENUMS.Attacking);
            //    return;
            //}
            //movementStateMachine.CalculateMoveMagnitude();
        }
    }
}
