using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TMD
{
    [RequireComponent(typeof(SlotManager))]
    public class InventoryManager : MonoBehaviour
    {
        public Item leftHandWeapon;
        public Item rightHandWeapon;

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
            if (rightHandWeapon)
            {
                
            }
            if (leftHandWeapon)
            {
                slotManager.LoadItemOnSlot(leftHandWeapon, isRightHand: false);
            }
        }

        public HOLDING_ITEM_STATE GetHoldingItemState()
        {
            if (leftHandWeapon && rightHandWeapon)
            {
                return HOLDING_ITEM_STATE.BOTH_HAND;
            }
            else if (leftHandWeapon)
            {
                return HOLDING_ITEM_STATE.LEFT_HAND;
            }
            else
            {
                return HOLDING_ITEM_STATE.RIGHT_HAND;
            }
            return HOLDING_ITEM_STATE.EMPTY;
        }

        public ITEM_TYPE GetItemType(Item item)
        {
            if (item is Sword)
            {
                return ITEM_TYPE.SWORD;
            }
            return ITEM_TYPE.UNKNOWN;
        }
    }
}
