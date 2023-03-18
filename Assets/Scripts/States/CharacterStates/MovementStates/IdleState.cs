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
            movementStateMachine.rgBody.velocity = Vector3.zero;
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
            if (movementStateMachine.isDoingAction)
            {
                return;
            }
            //movementStateMachine.rgBody.velocity = Vector3.zero;
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
                movementStateMachine.SwitchState(MovementStateMachine.MOVEMENT_STATE_ENUMS.WalkingForward);
                return;
            }
            if (movementStateMachine.isInteractingObject && movementStateMachine.interactableItem != null)
            {
                movementStateMachine.SwitchState(MovementStateMachine.MOVEMENT_STATE_ENUMS.PickingUp);
                return;
            }
            movementStateMachine.animatorManager.SetFloat(moveForwardStateParam, 0f);
            movementStateMachine.animatorManager.SetFloat(moveHorizontalStateParam, 0f);
        }
    }
}
