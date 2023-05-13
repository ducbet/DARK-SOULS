using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TMD
{
    public class FoundCharacterState : DetectCharacterState
    {
        public FoundCharacterState(DetectCharacterStateMachine detectCharacterStateMachine, int stateIndex) : base(detectCharacterStateMachine, stateIndex)
        {
        }
        public override void Enter()
        {
            base.Enter();
            detectCharacterStateMachine.StartValidatingFoundTarget();
        }

        public override void Exit()
        {
            base.Exit();
            detectCharacterStateMachine.FoundTarget = null;
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
