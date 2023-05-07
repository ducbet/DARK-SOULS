using UnityEngine;

namespace TMD
{
    public class MovementEmptyState : MovementState
    {
        private int nextState;
        public MovementEmptyState(MovementStateMachine movementStateMachine, int stateIndex) : base(movementStateMachine, stateIndex)
        {
            nextState = (int)MovementStateMachine.MOVEMENT_STATE_ENUMS.Idle;
        }

        public override void Enter()
        {
            base.Enter();
            movementStateMachine.animatorManager.SetFloat(moveForwardStateParam, 0f);
            movementStateMachine.animatorManager.SetFloat(moveHorizontalStateParam, 0f);

            nextState = movementStateMachine.preState.stateIndex;
        }

        public override void Exit()
        {
            base.Exit();
        }

        public override void FixedUpdate()
        {
        }

        public override void LateUpdate()
        {
        }

        public override void Update()
        {
            base.Update();
            if (IsStateChanged())
            {
                return;
            }
            if (IsStopMovement() == false)
            {
                movementStateMachine.SwitchState((MovementStateMachine.MOVEMENT_STATE_ENUMS)nextState);
            }
        }
    }
}
