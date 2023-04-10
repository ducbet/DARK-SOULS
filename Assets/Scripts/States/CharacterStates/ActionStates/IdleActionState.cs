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
            actionStateMachine.animatorManager.PlayTargetAnimation("Empty");
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
                if (actionStateMachine.isMoving)
                {
                    actionStateMachine.SwitchState(ActionStateMachine.ACTION_STATE_ENUMS.Rolling);
                }
                else
                {
                    actionStateMachine.SwitchState(ActionStateMachine.ACTION_STATE_ENUMS.DodgingBack);
                }
                return;
            }
            if (actionStateMachine.isInteractingObject && actionStateMachine.interactableItem != null)
            {
                actionStateMachine.SwitchState(ActionStateMachine.ACTION_STATE_ENUMS.PickingUp);
                return;
            }
        }

        public override bool isMovementBlocked()
        {
            return false;
        }

        public override bool isRotationBlocked()
        {
            return false;
        }
    }
}
