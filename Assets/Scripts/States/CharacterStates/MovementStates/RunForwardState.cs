using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TMD
{
    public class RunForwardState : RunningState
    {
        public RunForwardState(MovementStateMachine moveStateMachine) : base(moveStateMachine) { }

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
                movementStateMachine.SwitchState(MovementStateMachine.MOVEMENT_STATE_ENUMS.WalkingForward);
                return;
            }
            if (movementStateMachine.isLockingOn)
            {
                if (movementStateMachine.GetPlayerMovementHorizontal() > 0)
                {
                    movementStateMachine.SwitchState(MovementStateMachine.MOVEMENT_STATE_ENUMS.RunningStrafeRight);
                    return;
                }
                else if (movementStateMachine.GetPlayerMovementHorizontal() < 0)
                {
                    movementStateMachine.SwitchState(MovementStateMachine.MOVEMENT_STATE_ENUMS.RunningStrafeLeft);
                    return;
                }
            }
            
            movementStateMachine.animatorManager.SetFloat(moveForwardStateParam, getRunForwardAnimValue());
            movementStateMachine.animatorManager.SetFloat(moveHorizontalStateParam, 0);
        }
    }
}
