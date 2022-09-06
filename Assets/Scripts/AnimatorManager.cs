using System.Collections.Generic;
using UnityEngine;

namespace TMD
{
    [RequireComponent(typeof(Animator))]
    public class AnimatorManager : MonoBehaviour
    {
        [HideInInspector] public string movementYParam = "MovementY";
        [HideInInspector] public string isInteractingParam = "IsInteracting";
        [HideInInspector] public string isUsingRootMotionParam = "IsUsingRootMotion";
        [HideInInspector] public string isIgnoreYAxisRootMotionParam = "IsIgnoreYAxisRootMotion";
        [HideInInspector] public string isGroundParam = "IsGround";

        [HideInInspector] public string fallingAnimation = "Falling";
        [HideInInspector] public string landingAnimation = "Landing";
        [HideInInspector] public string rollAnimation = "Roll";
        [HideInInspector] public string dodgeBackAnimation = "Dodge Back";

        private float fadeLength = 0.2f;

        [HideInInspector] public Vector3 deltaPosition = Vector3.zero;

        private Animator animator;
        private Dictionary<string, int> animationHash = new Dictionary<string, int>();

        private void Awake()
        {
            animator = GetComponent<Animator>();
        }

        public void SetFloat(string animationName, float value)
        {
            animator.SetFloat(HashString(animationName), value, 0.1f, Time.deltaTime);
        }

        public void PlayTargetAnimation(string animationName, bool isInteracting = true)
        {
            Debug.Log("PlayTargetAnimation animationName: " + animationName + ", isInteracting: " + isInteracting);
            deltaPosition = Vector3.zero;
            animator.SetBool(HashString(isInteractingParam), isInteracting);
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

        public void SetBool(string animationName, bool value)
        {
            animator.SetBool(animationName, value);
        }

        public bool GetBool(string boolName)
        {
            return animator.GetBool(HashString(boolName));
        }

        private void OnAnimatorMove()
        {
            if (GetBool(isUsingRootMotionParam))
            {
                deltaPosition = animator.deltaPosition / Time.deltaTime;
            }
        }
    }
}