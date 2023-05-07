using UnityEngine;

namespace TMD
{
    public class WalkingStrafeRightState : WalkingState
    {
        public WalkingStrafeRightState(MovementStateMachine moveStateMachine, int stateIndex) : base(moveStateMachine, stateIndex) { }

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
            if (isRunning())
            {
                movementStateMachine.SwitchState(MovementStateMachine.MOVEMENT_STATE_ENUMS.RunningStrafeRight);
                return;
            }
            if (movementStateMachine.GetPlayerMovementHorizontal() <= 0)
            {
                movementStateMachine.SwitchState(MovementStateMachine.MOVEMENT_STATE_ENUMS.WalkingForward);
                return;
            }

            movementStateMachine.animatorManager.SetFloat(moveForwardStateParam, getWalkForwardAnimValue());
            movementStateMachine.animatorManager.SetFloat(moveHorizontalStateParam, (float)MovementStateMachine.MOVEMENT_STATE_ENUMS.WalkingForward);
        }
    }
}
