using UnityEngine;

namespace TMD
{
    public class SprintState : PlaneMoveState
    {
        public SprintState(MovementStateMachine moveStateMachine) : base(moveStateMachine) { }

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
            if (movementStateMachine.is_AI_control)
            {
                movementStateMachine.rgBody.velocity = movementStateMachine.moveDirection * movementStateMachine.sprintingSpeed;
            }
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
            if (!movementStateMachine.isSprinting)
            {
                movementStateMachine.SwitchState(MovementStateMachine.MOVEMENT_STATE_ENUMS.RunningForward);
                return;
            }
            if (movementStateMachine.isRolling)
            {
                // If Idle -> DodgingBack, else Rolling
                movementStateMachine.SwitchState(MovementStateMachine.MOVEMENT_STATE_ENUMS.Rolling);
                return;
            }
            movementStateMachine.animatorManager.SetFloat(moveForwardStateParam, (float)MovementStateMachine.MOVEMENT_STATE_ENUMS.Sprinting);
        }
    }
}
