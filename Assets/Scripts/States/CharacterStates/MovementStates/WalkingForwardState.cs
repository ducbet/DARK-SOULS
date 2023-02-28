using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TMD
{
    public class WalkingForwardState : WalkingState
    {
        public WalkingForwardState(MovementStateMachine moveStateMachine) : base(moveStateMachine) { }

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
                movementStateMachine.SwitchState(MovementStateMachine.MOVEMENT_STATE_ENUMS.RunningForward);
                return;
            }
            if (movementStateMachine.isLockingOn)
            {
                if (movementStateMachine.GetPlayerMovementHorizontal() > 0)
                {
                    movementStateMachine.SwitchState(MovementStateMachine.MOVEMENT_STATE_ENUMS.WalkingStrafeRight);
                    return;
                }
                else if (movementStateMachine.GetPlayerMovementHorizontal() < 0)
                {
                    movementStateMachine.SwitchState(MovementStateMachine.MOVEMENT_STATE_ENUMS.WalkingStrafeLeft);
                    return;
                }
            }

            movementStateMachine.animatorManager.SetFloat(moveForwardStateParam, getWalkForwardAnimValue());
            movementStateMachine.animatorManager.SetFloat(moveHorizontalStateParam, 0);
        }
    }
}
