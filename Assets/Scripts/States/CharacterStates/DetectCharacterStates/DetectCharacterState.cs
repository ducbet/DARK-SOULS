using System.Collections;
using UnityEngine;

namespace TMD
{
    public class DetectCharacterState : State
    {
        protected DetectCharacterStateMachine detectCharacterStateMachine;
        public DetectCharacterState(DetectCharacterStateMachine detectCharacterStateMachine, int stateIndex) : base(stateIndex)
        {
            this.detectCharacterStateMachine = detectCharacterStateMachine;
        }

        public override void Enter()
        {
            base.Enter();
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
    }
}
