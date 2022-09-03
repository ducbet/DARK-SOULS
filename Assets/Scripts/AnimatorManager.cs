using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TMD
{
    [RequireComponent(typeof(Animator))]
    public class AnimatorManager : MonoBehaviour
    {
        [HideInInspector] public string movementYParam = "MovementY";

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

        private int HashString(string animationName)
        {
            if (animationHash.ContainsKey(animationName))
            {
                return animationHash[animationName];
            }
            animationHash[animationName] = Animator.StringToHash(animationName);
            return animationHash[animationName];
        }
    }

}
