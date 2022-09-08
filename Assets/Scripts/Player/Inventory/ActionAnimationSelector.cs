using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TMD
{
    public class ActionAnimationSelector : MonoBehaviour
    {
        public string GetAttackAnimation(InventoryManager.HOLDING_ITEM_STATE holdingState,
            InventoryManager.ITEM_TYPE rightHandItemType = InventoryManager.ITEM_TYPE.UNKNOWN,
            string attackType = "_light_attack",
            string animationNum = "_01")
        {
            if (rightHandItemType == InventoryManager.ITEM_TYPE.UNKNOWN)
            {
                return "";  // punch or something?
            }
            string animationName = "";
            if (rightHandItemType == InventoryManager.ITEM_TYPE.SWORD)
            {
                animationName += "straight_sword";
            }
            // else if (rightHandItemType == InventoryManager.ITEM_TYPE.SHIELD) ,...

            if (holdingState == InventoryManager.HOLDING_ITEM_STATE.BOTH_HAND)
            {
                animationName += "_th";
            }
            else if (holdingState == InventoryManager.HOLDING_ITEM_STATE.LEFT_HAND || holdingState == InventoryManager.HOLDING_ITEM_STATE.RIGHT_HAND)
            {
                animationName += "_oh";
            }
            animationName += attackType;
            animationName += animationNum;
            return animationName;
        }
    }
}
