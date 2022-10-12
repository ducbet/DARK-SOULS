using System.Collections.Generic;
using UnityEngine;

namespace TMD
{
    [RequireComponent(typeof(Animator))]
    public class AnimatorManager : MonoBehaviour
    {
        public bool isInteracting = false;
        public bool isUsingRootMotion = false;
        public bool isIgnoreYAxisRootMotion = false;

        [HideInInspector] private string movementYParamName = "MovementY";
        [HideInInspector] public int movementYParam;
        [HideInInspector] private string isInteractingParamName = "IsInteracting";
        [HideInInspector] public int isInteractingParam;
        [HideInInspector] private string isUsingRootMotionParamName = "IsUsingRootMotion";
        [HideInInspector] public int isUsingRootMotionParam;
        [HideInInspector] private string isIgnoreYAxisRootMotionParamName = "IsIgnoreYAxisRootMotion";
        [HideInInspector] public int isIgnoreYAxisRootMotionParam;
        [HideInInspector] private string isGroundParamName = "IsGround";
        [HideInInspector] public int isGroundParam;

        [HideInInspector] private string fallingAnimationName = "Falling";
        [HideInInspector] public int fallingAnimation;
        [HideInInspector] private string landingAnimationName = "Landing";
        [HideInInspector] public int landingAnimation;
        [HideInInspector] private string rollAnimationName = "Roll";
        [HideInInspector] public int rollAnimation;
        [HideInInspector] private string dodgeBackAnimationName = "Dodge Back";
        [HideInInspector] public int dodgeBackAnimation;
        [HideInInspector] private string pickUpAnimationName = "Pick Up";
        [HideInInspector] public int pickUpAnimation;
        [HideInInspector] private string dieAnimationName = "Die";
        [HideInInspector] public int dieAnimation;


        private float fadeLength = 0.2f;

        [HideInInspector] public Vector3 deltaPosition = Vector3.zero;

        private Animator animator;

        private void Awake()
        {
            animator = GetComponent<Animator>();
            HashStrings();
        }

        public void SetFloat(int animationId, float value)
        {
            animator.SetFloat(animationId, value, 0.1f, Time.deltaTime);
        }

        public void CrossFade(string animationName)
        {
            animator.CrossFade(animationName, fadeLength);
        }

        public void PlayTargetAnimation(string animationName, bool _isInteracting = true)
        {
            Debug.Log("PlayTargetAnimation animationName: " + animationName + ", isInteracting: " + isInteracting);
            deltaPosition = Vector3.zero;
            isInteracting = _isInteracting;
            animator.SetBool(isInteractingParam, _isInteracting);
            animator.CrossFade(animationName, fadeLength);
        }

        public void PlayTargetAnimation(int animationId, bool _isInteracting = true)
        {
            Debug.Log("PlayTargetAnimation animationId: " + animationId + ", isInteracting: " + isInteracting);
            deltaPosition = Vector3.zero;
            isInteracting = _isInteracting;
            animator.SetBool(isInteractingParam, _isInteracting);
            animator.CrossFade(animationId, fadeLength);
        }

        public void PlayTargetAttackAnimation(string animationName, bool isInteracting = true)
        {
            animator.SetBool(isUsingRootMotionParam, true);
            animator.SetBool(isIgnoreYAxisRootMotionParam, true);
            PlayTargetAnimation(animationName, isInteracting);
        }

        public void SetBool(int boolId, bool value)
        {
            animator.SetBool(boolId, value);
        }

        public bool GetBool(int boolId)
        {
            return animator.GetBool(boolId);
        }

        public void UpdateAnimatorUsingState()
        {
            isInteracting = GetBool(isInteractingParam);
            isUsingRootMotion = GetBool(isUsingRootMotionParam);
            isIgnoreYAxisRootMotion = GetBool(isIgnoreYAxisRootMotionParam);
        }

        private void OnAnimatorMove()
        {
            if (isUsingRootMotion)
            {
                deltaPosition = animator.deltaPosition / Time.deltaTime;
            }
        }
        private void HashStrings()
        {
            movementYParam = Animator.StringToHash(movementYParamName);
            isInteractingParam = Animator.StringToHash(isInteractingParamName);
            isUsingRootMotionParam = Animator.StringToHash(isUsingRootMotionParamName);
            isIgnoreYAxisRootMotionParam = Animator.StringToHash(isIgnoreYAxisRootMotionParamName);
            isGroundParam = Animator.StringToHash(isGroundParamName);

            fallingAnimation = Animator.StringToHash(fallingAnimationName);
            landingAnimation = Animator.StringToHash(landingAnimationName);
            rollAnimation = Animator.StringToHash(rollAnimationName);
            dodgeBackAnimation = Animator.StringToHash(dodgeBackAnimationName);
            pickUpAnimation = Animator.StringToHash(pickUpAnimationName);
            dieAnimation = Animator.StringToHash(dieAnimationName);
        }
    }
}
