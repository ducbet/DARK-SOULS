using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TMD
{
    public class FoundCharacterState : State
    {
        private DetectCharacterStateMachine detectCharacterStateMachine;
        public FoundCharacterState(DetectCharacterStateMachine detectCharacterStateMachine)
        {
            this.detectCharacterStateMachine = detectCharacterStateMachine;
        }
        public override void Enter()
        {
            base.Enter();
            detectCharacterStateMachine.StartValidatingFoundTarget();
        }

        public override void Exit()
        {
            base.Exit();
            detectCharacterStateMachine.foundTarget = null;
            detectCharacterStateMachine.StopValidatingFoundTarget();
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
    }
}
