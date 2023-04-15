using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TMD
{
    public class LandedState : ActionState
    {
        private string fallingToRollAnimationName = "Falling To Roll";
        private int fallingToRollAnimation;
        private string landingAnimationName = "Landing";
        private int landingAnimation;

        public LandedState(ActionStateMachine actionStateMachine) : base(actionStateMachine)
        {
            fallingToRollAnimation = actionStateMachine.animatorManager.HashString(fallingToRollAnimationName);
            landingAnimation = actionStateMachine.animatorManager.HashString(landingAnimationName);
        }

        public override void Enter()
        {
            base.Enter();
            actionStateMachine.animatorManager.EnableRootMotion();
            Debug.Log("LandedState actionStateMachine.falledTime " + actionStateMachine.falledTime);

            if (actionStateMachine.falledTime > 1)
            {
                actionStateMachine.PlayTargetAnimation(fallingToRollAnimation);
            }
            else
            {
                actionStateMachine.PlayTargetAnimation(landingAnimation);
            }
            actionStateMachine.falledTime = 0f;
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
                actionStateMachine.SwitchState(ActionStateMachine.ACTION_STATE_ENUMS.Idle);
                return;
            }
        }
    }
}
