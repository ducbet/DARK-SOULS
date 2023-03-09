using UnityEngine;

namespace TMD
{
    public class RunningState : PlaneMoveState
    {
        public RunningState(MovementStateMachine moveStateMachine) : base(moveStateMachine) { }
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
            if (movementStateMachine.isSprinting && !movementStateMachine.IsLockingOn())
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
        }
        protected bool isWalking()
        {
            return movementStateMachine.isWalking || movementStateMachine.moveMagnitude < movementStateMachine.runThreshold;
        }

        protected float getRunForwardAnimValue()
        {
            if (movementStateMachine.GetPlayerMovementVertical() > 0 || !movementStateMachine.IsLockingOn())
            {
                return (float)MovementStateMachine.MOVEMENT_STATE_ENUMS.RunningForward;
            }
            else if (movementStateMachine.GetPlayerMovementVertical() < 0)
            {
                return -(float)MovementStateMachine.MOVEMENT_STATE_ENUMS.RunningForward;
            }
            return 0;
        }
    }
}
