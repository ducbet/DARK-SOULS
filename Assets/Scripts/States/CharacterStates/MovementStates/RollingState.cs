using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TMD
{
    public class RollingState : ActionState
    {
        private string rollAnimationName = "Roll";
        private int rollAnimation;

        public RollingState(ActionStateMachine actionStateMachine) : base(actionStateMachine)
        {
            rollAnimation = base.actionStateMachine.animatorManager.HashString(rollAnimationName);
        }
        public override void Enter()
        {
            base.Enter();
            actionStateMachine.animatorManager.EnableRootMotion();
            actionStateMachine.PlayTargetAnimation(rollAnimation);
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
                // falling to roll animation uses root motion
                HandleRootMotionMovements(actionStateMachine.rollingVelocityScale);
            }
            if (!actionStateMachine.isPlayingAnimation)
            {
                actionStateMachine.SwitchState(ActionStateMachine.ACTION_STATE_ENUMS.Idle);
                return;
            }
        }
    }
}
