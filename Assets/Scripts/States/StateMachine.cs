using System;
using UnityEngine;

namespace TMD
{
    public abstract class StateMachine : MonoBehaviour
    {
        public State preState;
        public State currentState;
        public State[] states;

        public void SwitchState(State newState)
        {
            preState = currentState;
            currentState?.Exit();
            currentState = newState;
            currentState?.Enter();
        }

        public void SwitchState(Enum stateEnum)
        {
            SwitchState(GetState(stateEnum));
        }

        protected virtual void Update()
        {
            currentState?.Update();
        }

        private void FixedUpdate()
        {
            currentState?.FixedUpdate();
        }

        private void LateUpdate()
        {
            currentState?.LateUpdate();
        }
        public State GetState(Enum stateEnum)
        {
            return states[Convert.ToInt32(stateEnum)];
        }
    }
}
