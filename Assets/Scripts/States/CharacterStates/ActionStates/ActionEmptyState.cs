using UnityEngine;

namespace TMD
{
    public class ActionEmptyState : ActionState
    {
        public ActionEmptyState(ActionStateMachine actionStateMachine, int stateIndex) : base(actionStateMachine, stateIndex)
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
            if (actionStateMachine.isJumping)
            {
                actionStateMachine.SwitchState(ActionStateMachine.ACTION_STATE_ENUMS.Jumping);
                return;
            }
            if (actionStateMachine.isLeftClick)
            {
                actionStateMachine.SwitchState(ActionStateMachine.ACTION_STATE_ENUMS.Attacking);
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
