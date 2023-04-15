using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TMD
{
    public class DieState : ActionState
    {
        private string dieAnimationName = "Die";
        private int dieAnimation;

        public DieState(ActionStateMachine actionStateMachine) : base(actionStateMachine)
        {
            dieAnimation = actionStateMachine.animatorManager.HashString(dieAnimationName);
        }
        public override void Enter()
        {
            base.Enter();
            StopMovingXZ();
            actionStateMachine.PlayTargetAnimation(dieAnimation);
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
        }
        public void StopMovingXZ()
        {
            actionStateMachine.rgBody.velocity = new Vector3(0, actionStateMachine.rgBody.velocity.y, 0);
        }
    }
}
