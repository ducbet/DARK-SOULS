using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TMD
{
    [RequireComponent(typeof(InventoryManager))]
    [RequireComponent(typeof(AnimatorManager))]
    [RequireComponent(typeof(ActionAnimationSelector))]
    public class PlayerAttacker : MonoBehaviour
    {
        [Header("Attack Attributes")]
        public float heavyAttackDetectTime = 0.2f;  // the amount of time after left click. To decide whether light attack or heavy attack

        // Can merge PlayerAttacker to PlayerActionManager or not???
        [HideInInspector] public InventoryManager inventoryManager;
        [HideInInspector] public AnimatorManager animatorManager;
        [HideInInspector] public ActionAnimationSelector attackAnimationSelector;


        private void Awake()
        {
            inventoryManager = GetComponent<InventoryManager>();
            animatorManager = GetComponent<AnimatorManager>();
            attackAnimationSelector = GetComponent<ActionAnimationSelector>();
        }

        public void Attack(bool isHeavyAttack = false)
        {
            string attackAnimationName = GetAttackAnimation(isHeavyAttack);
            if (attackAnimationName != "")
            {
                animatorManager.PlayTargetAttackAnimation(attackAnimationName);
            }
        }

        private string GetAttackAnimation(bool isHeavyAttack)
        {
            InventoryManager.HOLDING_ITEM_STATE holdingState = inventoryManager.GetHoldingItemState();
            InventoryManager.ITEM_TYPE rightHandItemType = inventoryManager.GetItemType(inventoryManager.rightHandItem);
            if (isHeavyAttack)
            {
                return attackAnimationSelector.GetAttackAnimation(holdingState, rightHandItemType, "_heavy_attack");
            }
            return attackAnimationSelector.GetAttackAnimation(holdingState, rightHandItemType);
        }
    }
}
