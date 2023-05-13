using UnityEngine;

namespace TMD
{
    public class FallState : ActionState
    {
        private float fallingTime;
        
        private string fallingAnimationName = "Falling";
        private int fallingAnimation;
        private string moveForwardStateParamName = "MoveForwardState";
        private int moveForwardStateParam;

        public FallState(ActionStateMachine actionStateMachine, int stateIndex) : base(actionStateMachine, stateIndex)
        {
            fallingAnimation = this.actionStateMachine.animatorManager.HashString(fallingAnimationName);
            moveForwardStateParam = this.actionStateMachine.animatorManager.HashString(moveForwardStateParamName);
        }

        public override void Enter()
        {
            base.Enter();
            ResetFallingTime();
            actionStateMachine.animatorManager.SetFloatNoSmooth(moveForwardStateParam, 0f);
            if (IsAssignableFromState<RollingState>(actionStateMachine.preState))
            {
                // pre state is RollingState -> fadeLength longer
                actionStateMachine.PlayTargetAnimation(fallingAnimation, fadeLength: 0.4f);
            }
            else
            {
                actionStateMachine.PlayTargetAnimation(fallingAnimationName);
            }
        }
        public override void Exit()
        {
            base.Exit();
            // ResetFallingTime() in LandedState
        }

        public override void Update()
        {
            if (IsStateChanged())
            {
                return;
            }
            UpdateFallingTime();
            if (actionStateMachine.isGrounded)
            {
                actionStateMachine.SwitchState(ActionStateMachine.ACTION_STATE_ENUMS.Laned);
            }
        }

        public override void FixedUpdate()
        {
            if (IsStateChanged())
            {
                return;
            }
            HandleFallingForces();
        }

        public override void LateUpdate()
        {
        }

        public void ResetFallingTime()
        {
            fallingTime = 0;
        }

        public void UpdateFallingTime()
        {
            fallingTime += Time.deltaTime;
        }

        public float GetFallingTime()
        {
            return fallingTime;
        }

        private void HandleFallingForces()
        {
            SlowDownXZ();
            actionStateMachine.rgBody.AddForce(Vector3.down * actionStateMachine.fallingVelocity * fallingTime);
        }

        private void SlowDownXZ()
        {
            float velocityY = actionStateMachine.rgBody.velocity.y;
            Vector3 velocityXZ = Vector3.SmoothDamp(actionStateMachine.rgBody.velocity, Vector3.zero, ref actionStateMachine.leapingVelocity, actionStateMachine.leapingVelocitySmoothTime);
            velocityXZ.y = velocityY;
            actionStateMachine.rgBody.velocity = velocityXZ;
        }
    }
}
