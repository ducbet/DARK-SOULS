using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TMD
{
    public class StopDetectingCharacterState : DetectCharacterState
    {
        public StopDetectingCharacterState(DetectCharacterStateMachine detectCharacterStateMachine, int stateIndex) : base(detectCharacterStateMachine, stateIndex)
        {
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
