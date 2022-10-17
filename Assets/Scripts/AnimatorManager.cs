using UnityEngine;

namespace TMD
{
    [RequireComponent(typeof(Animator))]
    public class AnimatorManager : MonoBehaviour
    {
        public bool isUsingRootMotion = false;

        [HideInInspector] public Vector3 deltaPosition = Vector3.zero;

        private Animator animator;

        private void Awake()
        {
            animator = GetComponent<Animator>();
        }

        public void CrossFade(string animationName, float fadeLength = 0.2f)
        {
            animator.CrossFade(animationName, fadeLength);
        }

        public void PlayTargetAnimation(string animationName, float fadeLength = 0.2f)
        {
            animator.CrossFade(animationName, fadeLength);
        }

        public void PlayTargetAnimation(int animationId, float fadeLength = 0.2f)
        {
            animator.CrossFade(animationId, fadeLength);
        }

        public void EnableRootMotion()
        {
            isUsingRootMotion = true;
            deltaPosition = Vector3.zero;
        }

        public void DisableRootMotion()
        {
            isUsingRootMotion = false;
        }

        private void OnAnimatorMove()
        {
            if (isUsingRootMotion)
            {
                deltaPosition = animator.deltaPosition / Time.deltaTime;
            }
        }

        public int HashString(string stringName)
        {
            return Animator.StringToHash(stringName);
        }

        public void SetBool(int boolId, bool value)
        {
            animator.SetBool(boolId, value);
        }

        public void SetBool(string boolName, bool value)
        {
            animator.SetBool(boolName, value);
        }

        public bool GetBool(int boolId)
        {
            return animator.GetBool(boolId);
        }

        public float GetFloat(int floatId)
        {
            return animator.GetFloat(floatId);
        }

        public float GetFloat(string floatName)
        {
            return animator.GetFloat(floatName);
        }

        public void SetFloat(int floatId, float value)
        {
            animator.SetFloat(floatId, value, 0.1f, Time.deltaTime);
        }

        public void SetFloat(string floatName, float value)
        {
            animator.SetFloat(floatName, value, 0.1f, Time.deltaTime);
        }
        public void SetFloatNoSmooth(int floatId, float value)
        {
            animator.SetFloat(floatId, value);
        }
        public void SetFloatNoSmooth(string floatName, float value)
        {
            animator.SetFloat(floatName, value);
        }
    }
}
