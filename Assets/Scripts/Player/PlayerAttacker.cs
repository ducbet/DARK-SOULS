using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TMD
{
    [RequireComponent(typeof(InventoryManager))]
    [RequireComponent(typeof(AnimatorManager))]
    public class PlayerAttacker : MonoBehaviour
    {
        [Header("Attack Attributes")]
        public float heavyAttackDetectTime = 0.2f;  // the amount of time after left click. To decide whether light attack or heavy attack

        // Can merge PlayerAttacker to PlayerActionManager or not???
        [HideInInspector] public InventoryManager inventoryManager;
        [HideInInspector] public AnimatorManager animatorManager;

        public string lastAttackName = "";


        private void Awake()
        {
            inventoryManager = GetComponent<InventoryManager>();
            animatorManager = GetComponent<AnimatorManager>();
        }

        /*
        Return: true if attack is triggered. false if cancaled 
         */
        public bool Attack()
        {
            string attackAnimationName = GetAttackAnimation();
            lastAttackName = attackAnimationName;
            if (attackAnimationName != "")
            {
                animatorManager.PlayTargetAttackAnimation(attackAnimationName);
                return true;
            }
            return false;
        }

        public void ClearComboAttack()
        {
            lastAttackName = "";
        }
        public string GetAttackAnimation()
        {
            ItemObject leftHandItemObject = inventoryManager.GetCurrentItemObject(isRightHand: false);
            ItemObject rightHandItemObject = inventoryManager.GetCurrentItemObject();
            if (leftHandItemObject && rightHandItemObject)
            {
                if (rightHandItemObject is WeaponObject)
                {
                    return ((WeaponObject)rightHandItemObject).GetAttackAnimation(lastAttackName);
                }
            }
            else if (leftHandItemObject)
            {
                return "";
            }
            else if (rightHandItemObject)
            {
                if (rightHandItemObject is WeaponObject)
                {
                    return ((WeaponObject)rightHandItemObject).GetAttackAnimation(lastAttackName);
                }
            }
            return "";
        }

        public int GetAttackStaminaCost(string animationAttackName)
        {
            ItemObject leftHandItemObject = inventoryManager.GetCurrentItemObject(isRightHand: false);
            ItemObject rightHandItemObject = inventoryManager.GetCurrentItemObject();
            if (leftHandItemObject && rightHandItemObject)
            {
                if (rightHandItemObject is WeaponObject)
                {
                    return ((WeaponObject)rightHandItemObject).GetStaminaCost(animationAttackName);
                }
            }
            else if (leftHandItemObject)
            {
                return 0;
            }
            else if (rightHandItemObject)
            {
                if (rightHandItemObject is WeaponObject)
                {
                    return ((WeaponObject)rightHandItemObject).GetStaminaCost(animationAttackName);
                }
            }
            return 0;
        }
    }
}
