using UnityEngine;

namespace TMD
{
    public class DodgingBackState : GroundedState
    {
        private string dodgeBackAnimationName = "Dodge Back";
        private int dodgeBackAnimation;

        public DodgingBackState(MovementStateMachine movementStateMachine) : base(movementStateMachine)
        {
            dodgeBackAnimation = movementStateMachine.animatorManager.HashString(dodgeBackAnimationName);
        }
        public override void Enter()
        {
            base.Enter();
            movementStateMachine.animatorManager.EnableRootMotion();
            movementStateMachine.PlayTargetAnimation(dodgeBackAnimation);
        }

        public override void Exit()
        {
            base.Exit();
            movementStateMachine.animatorManager.DisableRootMotion();
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
            if (movementStateMachine.animatorManager.isUsingRootMotion)
            {
                HandleRootMotionMovements(movementStateMachine.rollingVelocityScale);
            }
            if (!movementStateMachine.isPlayingAnimation)
            {
                movementStateMachine.SwitchState(MovementStateMachine.MOVEMENT_STATE_ENUMS.Idle);
                return;
            }
        }
    }
}
