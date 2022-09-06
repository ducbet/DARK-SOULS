using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TMD
{
    public class ResetBoolsOnStateExit : StateMachineBehaviour
    {
        public string[] defaultTrueBoolNames;
        public string[] defaultFalseBoolNames;

        // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
        override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            foreach (var boolName in defaultTrueBoolNames)
            {
                animator.SetBool(boolName, true);
            }

            foreach (var boolName in defaultFalseBoolNames)
            {
                animator.SetBool(boolName, false);
            }
        }
    }
}