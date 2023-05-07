using System;
using UnityEngine;
using UnityEngine.UI;

namespace TMD
{
    public abstract class StateMachine : MonoBehaviour
    {
        public State preState;
        public State currentState;
        public State[] states;

        // debug
        public Text currentStateUI;

        protected virtual void Start()
        {
            currentStateUI = GameObject.Find("DebugCurrentState").GetComponent<Text>();
        }
        private void SwitchState(State newState)
        {
            if (currentState == newState)
            {
                return;
            }
            preState = currentState;
            currentState?.Exit();
            currentState = newState;
            Debug.Log("SwitchState " + newState);
            currentState?.Enter();
            currentStateUI.text = currentState.GetType().ToString(); // Debug
        }

        public virtual void SwitchState(Enum stateEnum)
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
