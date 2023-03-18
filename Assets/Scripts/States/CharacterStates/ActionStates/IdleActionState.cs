using UnityEngine;

namespace TMD
{
    public class IdleActionState : ActionState
    {
        public IdleActionState(ActionStateMachine actionStateMachine) : base(actionStateMachine)
        {

        }

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
            if (actionStateMachine.isRolling)
            {
                // If Idle -> DodgingBack, else Rolling
                //actionStateMachine.SwitchState(ActionStateMachine.ACTION_STATE_ENUMS.DodgingBack);
                actionStateMachine.SwitchState(ActionStateMachine.ACTION_STATE_ENUMS.Rolling);
                return;
            }
        }
    }
}
