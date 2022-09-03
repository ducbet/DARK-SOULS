using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TMD
{
    [RequireComponent(typeof(Animator))]
    public class AnimatorManager : MonoBehaviour
    {
        [HideInInspector] public string movementYParam = "MovementY";
        [HideInInspector] public string isInteractingParam = "IsInteracting";
        [HideInInspector] public string usingRootMotionParam = "UsingRootMotion";

        [HideInInspector] public string rollAnimation = "Roll";
        [HideInInspector] public string dodgeBackAnimation = "Dodge Back";

        [HideInInspector] public Vector3 deltaPosition = Vector3.zero;

        private Animator animator;
        private Dictionary<string, int> animationHash = new Dictionary<string, int>();

        private float fadeLength = 0.2f;

        private void Awake()
        {
            animator = GetComponent<Animator>();
        }

        public void SetFloat(string animationName, float value)
        {
            animator.SetFloat(HashString(animationName), value, 0.1f, Time.deltaTime);
        }
        public void PlayTargetAnimation(string animationName, bool isInteracting, bool useRootMotion = false)
        {
            Debug.Log("PlayTargetAnimation animationName: " + animationName + ", isInteracting: " + isInteracting + ", useRootMotion: " + useRootMotion);
            deltaPosition = Vector3.zero;
            animator.SetBool(HashString(isInteractingParam), isInteracting);
            animator.SetBool(HashString(usingRootMotionParam), useRootMotion);
            animator.CrossFade(HashString(animationName), fadeLength);
        }

        private int HashString(string animationName)
        {
            if (animationHash.ContainsKey(animationName))
            {
                return animationHash[animationName];
            }
            animationHash[animationName] = Animator.StringToHash(animationName);
            return animationHash[animationName];
        }
        public bool GetBool(string boolName)
        {
            return animator.GetBool(HashString(boolName));
        }

        private void OnAnimatorMove()
        {
            if (GetBool(usingRootMotionParam))
            {
                deltaPosition = animator.deltaPosition / Time.deltaTime;
            }
        }
    }

}
