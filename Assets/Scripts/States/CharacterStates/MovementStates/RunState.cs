using UnityEngine;

namespace TMD
{
    public class RunState : PlaneMoveState
    {
        public RunState(MovementStateMachine moveStateMachine) : base(moveStateMachine) { }
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
            movementStateMachine.rgBody.velocity = movementStateMachine.moveDirection * movementStateMachine.runningSpeed;
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
            if (movementStateMachine.isWalking || movementStateMachine.moveMagnitude < movementStateMachine.runThreshold)
            {
                movementStateMachine.SwitchState(MovementStateMachine.MOVEMENT_STATE_ENUMS.Walking);
                return;
            }
            if (movementStateMachine.isSprinting)
            {
                movementStateMachine.SwitchState(MovementStateMachine.MOVEMENT_STATE_ENUMS.Sprinting);
                return;
            }
            if (movementStateMachine.isRolling)
            {
                // If Idle -> DodgingBack, else Rolling
                movementStateMachine.SwitchState(MovementStateMachine.MOVEMENT_STATE_ENUMS.Rolling);
                return;
            }
            movementStateMachine.animatorManager.SetFloat(moveForwardStateParam, (float)MovementStateMachine.MOVEMENT_STATE_ENUMS.Running);
        }
    }
}
