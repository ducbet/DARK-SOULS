using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TMD
{
    public class RunningStrafeLeftState : RunningState
    {
        public RunningStrafeLeftState(MovementStateMachine moveStateMachine, int stateIndex) : base(moveStateMachine, stateIndex) { }
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
            if (isWalking())
            {
                movementStateMachine.SwitchState(MovementStateMachine.MOVEMENT_STATE_ENUMS.WalkingStrafeLeft);
                return;
            }
            if (movementStateMachine.GetPlayerMovementHorizontal() >= 0)
            {
                movementStateMachine.SwitchState(MovementStateMachine.MOVEMENT_STATE_ENUMS.RunningForward);
                return;
            }

            movementStateMachine.animatorManager.SetFloat(moveForwardStateParam, getRunForwardAnimValue());
            movementStateMachine.animatorManager.SetFloat(moveHorizontalStateParam, -(float)MovementStateMachine.MOVEMENT_STATE_ENUMS.RunningForward);
        }
    }
}
