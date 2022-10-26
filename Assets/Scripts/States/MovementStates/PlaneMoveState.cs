using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TMD
{
    public class PlaneMoveState : GroundedState
    {
        protected string moveForwardStateParamName = "MoveForwardState";
        protected int moveForwardStateParam;

        public PlaneMoveState(MovementStateMachine movementStateMachine) : base(movementStateMachine) {
            moveForwardStateParam = base.movementStateMachine.animatorManager.HashString(moveForwardStateParamName);
        }
        public override void Enter()
        {
            base.Enter();
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
            if (movementStateMachine.isLeftClick)
            {
                movementStateMachine.SwitchState(MovementStateMachine.MOVEMENT_STATE_ENUMS.Attacking);
                return;
            }
            movementStateMachine.CalculateMoveMagnitude();
        }
    }
}
