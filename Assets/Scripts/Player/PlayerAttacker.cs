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

        private string lastAttackName = "";
        private Dictionary<string, string> comboAttacks = new Dictionary<string, string>{
            {"straight_sword_oh_light_attack_01", "straight_sword_oh_light_attack_02"},
            {"straight_sword_oh_light_attack_02", "straight_sword_oh_heavy_attack_01"},
            {"straight_sword_oh_heavy_attack_01", "straight_sword_oh_heavy_attack_02"},
            {"straight_sword_oh_heavy_attack_02", "straight_sword_oh_light_attack_01"},
        };

        private void Awake()
        {
            inventoryManager = GetComponent<InventoryManager>();
            animatorManager = GetComponent<AnimatorManager>();
            attackAnimationSelector = GetComponent<ActionAnimationSelector>();
        }

        public void Attack(bool isHeavyAttack = false)
        {
            string attackAnimationName = GetAttackAnimation(isHeavyAttack);
            lastAttackName = attackAnimationName;
            if (attackAnimationName != "")
            {
                animatorManager.PlayTargetAttackAnimation(attackAnimationName);
            }
        }

        public void ClearComboAttack()
        {
            lastAttackName = "";
        }

        private string GetAttackAnimation(bool isHeavyAttack)
        {
            if (lastAttackName != "")
            {
                return GetComboAttackAnimation();
            }
            InventoryManager.HOLDING_ITEM_STATE holdingState = inventoryManager.GetHoldingItemState();
            InventoryManager.ITEM_TYPE rightHandItemType = inventoryManager.GetItemType(inventoryManager.rightHandItem);
            if (isHeavyAttack)
            {
                return attackAnimationSelector.GetAttackAnimation(holdingState, rightHandItemType, "_heavy_attack");
            }
            return attackAnimationSelector.GetAttackAnimation(holdingState, rightHandItemType);
        }
        private string GetComboAttackAnimation()
        {
            if (comboAttacks.ContainsKey(lastAttackName))
            {
                return comboAttacks[lastAttackName];
            }
            return "";
        }
    }
}
