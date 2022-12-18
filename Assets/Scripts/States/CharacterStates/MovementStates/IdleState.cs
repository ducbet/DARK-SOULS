using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TMD
{
    public class IdleState : PlaneMoveState
    {
        public IdleState(MovementStateMachine movementStateMachine) : base(movementStateMachine) { }

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
            if (IsStateChanged())
            {
                return;
            }
            movementStateMachine.rgBody.velocity = Vector3.zero;
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
            if (movementStateMachine.moveMagnitude > 0)
            {
                movementStateMachine.SwitchState(MovementStateMachine.MOVEMENT_STATE_ENUMS.Walking);
                return;
            }
            if (movementStateMachine.isRolling)
            {
                // If Idle -> DodgingBack, else Rolling
                movementStateMachine.SwitchState(MovementStateMachine.MOVEMENT_STATE_ENUMS.DodgingBack);
                return;
            }
            if (movementStateMachine.isInteractingObject && movementStateMachine.interactableItem != null)
            {
                movementStateMachine.SwitchState(MovementStateMachine.MOVEMENT_STATE_ENUMS.PickingUp);
                return;
            }
            movementStateMachine.animatorManager.SetFloat(moveForwardStateParam, 0f);
        }
    }
}
