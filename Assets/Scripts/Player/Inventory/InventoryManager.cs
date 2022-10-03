using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TMD
{
    [RequireComponent(typeof(SlotManager))]
    public class InventoryManager : MonoBehaviour
    {
        public ItemObject leftHandItemObject;
        public ItemObject rightHandItemObject;

        [HideInInspector] public GameObject leftHandItem;
        [HideInInspector] public GameObject rightHandItem;

        [HideInInspector] public AnimatorManager animatorManager;

        private SlotManager slotManager;
        public enum HOLDING_ITEM_STATE { EMPTY, LEFT_HAND, RIGHT_HAND, BOTH_HAND };
        public enum ITEM_TYPE { PUNCH, SWORD, UNKNOWN }
        private void Awake()
        {
            slotManager = GetComponent<SlotManager>();
            animatorManager = GetComponent<AnimatorManager>();
        }

        // Start is called before the first frame update
        public void EquipItems()
        {
            if (rightHandItemObject)
            {
                rightHandItem = slotManager.LoadItemOnSlot(rightHandItemObject);
                if (rightHandItem)
                {
                    animatorManager.CrossFade(rightHandItemObject.GetRightArmIdleAnimation());
                }
                else
                {
                    animatorManager.CrossFade(rightHandItemObject.GetRightArmIdleAnimation());
                }
            }
            if (leftHandItemObject)
            {
                leftHandItem = slotManager.LoadItemOnSlot(leftHandItemObject, isRightHand: false);
                if (leftHandItem)
                {
                    animatorManager.CrossFade(leftHandItemObject.GetLeftArmIdleAnimation());
                }
                else
                {
                    animatorManager.CrossFade(leftHandItemObject.GetLeftArmEmptyAnimation());
                }
            }
        }

        public HOLDING_ITEM_STATE GetHoldingItemState()
        {
            if (leftHandItemObject && rightHandItemObject)
            {
                return HOLDING_ITEM_STATE.BOTH_HAND;
            }
            else if (leftHandItemObject)
            {
                return HOLDING_ITEM_STATE.LEFT_HAND;
            }
            else if (rightHandItemObject)
            {
                return HOLDING_ITEM_STATE.RIGHT_HAND;
            }
            return HOLDING_ITEM_STATE.EMPTY;
        }

        public ITEM_TYPE GetItemType(ItemObject item)
        {
            if (item is SwordObject)
            {
                return ITEM_TYPE.SWORD;
            }
            return ITEM_TYPE.UNKNOWN;
        }
    }
}
