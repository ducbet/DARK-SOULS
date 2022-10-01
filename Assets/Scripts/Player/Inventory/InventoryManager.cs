using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TMD
{
    [RequireComponent(typeof(SlotManager))]
    public class InventoryManager : MonoBehaviour
    {
        public ItemObject leftHandItem;
        public ItemObject rightHandItem;

        private SlotManager slotManager;
        public enum HOLDING_ITEM_STATE { EMPTY, LEFT_HAND, RIGHT_HAND, BOTH_HAND };
        public enum ITEM_TYPE { PUNCH, SWORD, UNKNOWN }
        private void Awake()
        {
            slotManager = GetComponent<SlotManager>();
        }

        // Start is called before the first frame update
        void Start()
        {
            if (rightHandItem)
            {
                slotManager.LoadItemOnSlot(rightHandItem);
            }
            if (leftHandItem)
            {
                slotManager.LoadItemOnSlot(leftHandItem, isRightHand: false);
            }
        }

        public HOLDING_ITEM_STATE GetHoldingItemState()
        {
            if (leftHandItem && rightHandItem)
            {
                return HOLDING_ITEM_STATE.BOTH_HAND;
            }
            else if (leftHandItem)
            {
                return HOLDING_ITEM_STATE.LEFT_HAND;
            }
            else if (rightHandItem)
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
