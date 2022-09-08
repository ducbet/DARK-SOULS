using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TMD
{
    [RequireComponent(typeof(InputManager))]
    public class PlayerActionManager : MonoBehaviour
    {
        /*
        Control all actions that are not related to locomotion 
        */

        [HideInInspector] public InputManager inputManager;
        [HideInInspector] public AnimatorManager animatorManager;
        [HideInInspector] public PlayerAttacker playerAttacker;

        private bool isAnimatorInteracting = false;

        private void Awake()
        {
            inputManager = GetComponent<InputManager>();
            animatorManager = GetComponent<AnimatorManager>();
            playerAttacker = GetComponent<PlayerAttacker>();
        }

        public void HandleAllActions()
        {
            isAnimatorInteracting = IsAnimatorInteracting();
            if (isAnimatorInteracting)
            {
                return;
            }
            HandleAttack();
        }

        private void HandleAttack()
        {
            if (inputManager.isLeftClick)
            {
                StartCoroutine(Attack());
            }
        }

        private IEnumerator Attack()
        {
            animatorManager.SetBool(animatorManager.isInteractingParam, true);
            yield return new WaitForSeconds(playerAttacker.heavyAttackDetectTime);
            if (inputManager.isLeftClick)
            {
                playerAttacker.Attack(isHeavyAttack: true);
            }
            else
            {
                playerAttacker.Attack();
            }
        }

        private bool IsAnimatorInteracting()
        {
            return animatorManager.GetBool(animatorManager.isInteractingParam);
        }
    }
}
