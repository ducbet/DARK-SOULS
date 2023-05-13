using UnityEngine;

namespace TMD
{
    public class DodgingBackState : ActionState
    {
        private string dodgeBackAnimationName = "Dodge Back";
        private int dodgeBackAnimation;

        public DodgingBackState(ActionStateMachine actionStateMachine, int stateIndex) : base(actionStateMachine, stateIndex)
        {
            dodgeBackAnimation = actionStateMachine.animatorManager.HashString(dodgeBackAnimationName);
        }
        public override void Enter()
        {
            base.Enter();
            actionStateMachine.animatorManager.EnableRootMotion();
            actionStateMachine.PlayTargetAnimation(dodgeBackAnimation);
        }

        public override void Exit()
        {
            base.Exit();
            actionStateMachine.animatorManager.DisableRootMotion();
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
            if (actionStateMachine.animatorManager.isUsingRootMotion)
            {
                HandleRootMotionMovements(actionStateMachine.rollingVelocityScale);
            }
            if (!actionStateMachine.isPlayingAnimation)
            {
                actionStateMachine.SwitchState(ActionStateMachine.ACTION_STATE_ENUMS.Empty);
                return;
            }
        }
    }
}
