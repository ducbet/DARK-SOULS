using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TMD
{
    public class StopDetectingCharacterState : State
    {
        private DetectCharacterStateMachine detectCharacterStateMachine;
        public StopDetectingCharacterState(DetectCharacterStateMachine detectCharacterStateMachine)
        {
            this.detectCharacterStateMachine = detectCharacterStateMachine;
        }

        public override void Enter()
        {
            base.Enter();
            detectCharacterStateMachine.isStopDetecting = true;
            detectCharacterStateMachine.FoundTarget = null;
            detectCharacterStateMachine.StopValidatingFoundTarget();
        }

        public override void Exit()
        {
            base.Exit();
            detectCharacterStateMachine.isStopDetecting = false;
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
