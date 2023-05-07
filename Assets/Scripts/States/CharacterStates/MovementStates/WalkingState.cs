using UnityEngine;

namespace TMD
{
    public class WalkingState : MovementState
    {
        public WalkingState(MovementStateMachine moveStateMachine, int stateIndex) : base(moveStateMachine, stateIndex) { }

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
        }
        protected bool isRunning()
        {
            return !movementStateMachine.isWalking && movementStateMachine.moveMagnitude >= movementStateMachine.runThreshold;
        }
        protected float getWalkForwardAnimValue()
        {
            if (movementStateMachine.GetPlayerMovementVertical() > 0 || !movementStateMachine.IsLockingOn())
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
