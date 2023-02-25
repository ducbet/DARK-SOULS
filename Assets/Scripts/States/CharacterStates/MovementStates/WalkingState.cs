using UnityEngine;

namespace TMD
{
    public class WalkingState : PlaneMoveState
    {
        public WalkingState(MovementStateMachine moveStateMachine) : base(moveStateMachine) { }

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
            movementStateMachine.rgBody.velocity = movementStateMachine.moveDirection * movementStateMachine.walkingSpeed;
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
            if (movementStateMachine.moveMagnitude == 0)
            {
                movementStateMachine.SwitchState(MovementStateMachine.MOVEMENT_STATE_ENUMS.Idle);
                return;
            }
            if (movementStateMachine.isRolling)
            {
                // If Idle -> DodgingBack, else Rolling
                movementStateMachine.SwitchState(MovementStateMachine.MOVEMENT_STATE_ENUMS.Rolling);
                return;
            }
        }
        protected bool isRunning()
        {
            return !movementStateMachine.isWalking && movementStateMachine.moveMagnitude >= movementStateMachine.runThreshold;
        }
        protected float getWalkForwardAnimValue()
        {
            if (movementStateMachine.GetPlayerMovementVertical() > 0 || !movementStateMachine.isLockingOn)
            {
                return (float)MovementStateMachine.MOVEMENT_STATE_ENUMS.WalkingForward;
            }
            else if (movementStateMachine.GetPlayerMovementVertical() < 0)
            {
                return -(float)MovementStateMachine.MOVEMENT_STATE_ENUMS.WalkingForward;
            }
            return 0;
        }
    }
}
