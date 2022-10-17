using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TMD
{
    public class DieState : State
    {
        private MovementStateMachine movementStateMachine;
        private string dieAnimationName = "Die";
        private int dieAnimation;

        public DieState(MovementStateMachine movementStateMachine)
        {
            this.movementStateMachine = movementStateMachine;
            dieAnimation = this.movementStateMachine.animatorManager.HashString(dieAnimationName);
        }
        public override void Enter()
        {
            base.Enter();
            StopMovingXZ();
            movementStateMachine.PlayTargetAnimation(dieAnimation);
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
            movementStateMachine.rgBody.velocity = new Vector3(0, movementStateMachine.rgBody.velocity.y, 0);
        }
    }
}
