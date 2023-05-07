using UnityEngine;

namespace TMD
{
    public class SprintState : MovementState
    {
        public SprintState(MovementStateMachine moveStateMachine, int stateIndex) : base(moveStateMachine, stateIndex) { }

        public override void Enter()
        {
            base.Enter();
            movementStateMachine.animatorManager.SetFloat(moveForwardStateParam, (float)MovementStateMachine.MOVEMENT_STATE_ENUMS.Sprinting);
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
            movementStateMachine.rgBody.velocity = movementStateMachine.moveDirection * movementStateMachine.sprintingSpeed;
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
            //movementStateMachine.animatorManager.SetFloat(moveForwardStateParam, (float)MovementStateMachine.MOVEMENT_STATE_ENUMS.Sprinting);
        }
    }
}
