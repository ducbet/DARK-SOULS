using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TMD
{
    [RequireComponent(typeof(SlotManager))]
    public class InventoryManager : MonoBehaviour
    {
        private static int QUICK_SLOT_CAPACITY = 3;

        private int currentLeftItemIndex = 0;
        private int currentRightItemIndex = 0;
        public ItemObject[] leftHandQuickSlotItemObjects = new ItemObject[QUICK_SLOT_CAPACITY];
        public ItemObject[] rightHandQuickSlotItemObjects = new ItemObject[QUICK_SLOT_CAPACITY];

        public List<ItemObject> inventory;

        [HideInInspector] public ItemObject leftHandItemObject;
        [HideInInspector] public ItemObject rightHandItemObject;

        [HideInInspector] public GameObject leftHandItem;
        [HideInInspector] public GameObject rightHandItem;

        [HideInInspector] public AnimatorManager animatorManager;

        private SlotManager slotManager;
        private void Awake()
        {
            slotManager = GetComponent<SlotManager>();
            animatorManager = GetComponent<AnimatorManager>();
        }

        private void Start()
        {
            EquipLeftHandItems();
            EquipRightHandItems();
        }

        public void EquipLeftHandItems(ItemObject item = null)
        {
            item = item != null ? item : GetCurrentItemObject(isRightHand: false);
            leftHandItem = slotManager.LoadItemOnSlot(item, isRightHand: false);
            animatorManager.CrossFade(item.GetLeftArmIdleAnimation());
            animatorManager.CrossFade(item.GetLeftArmEmptyAnimation());
        }

        public void EquipRightHandItems(ItemObject item = null)
        {
            item = item != null ? item : GetCurrentItemObject();
            rightHandItem = slotManager.LoadItemOnSlot(item);
            animatorManager.CrossFade(item.GetRightArmIdleAnimation());
            animatorManager.CrossFade(item.GetRightArmEmptyAnimation());
        }

        public void SwitchLeftHandItems()
        {
            EquipLeftHandItems(GetNextQuickSlotItem(isRightHand: false));
        }
        public void SwitchRightHandItems()
        {
            EquipRightHandItems(GetNextQuickSlotItem());
        }


        public void AddItemToInventory(ItemObject item)
        {
            inventory.Add(item);
        }

        public ItemObject GetNextQuickSlotItem(bool isRightHand = true)
        {
            if (isRightHand)
            {
                currentRightItemIndex = (currentRightItemIndex + 1) % QUICK_SLOT_CAPACITY;
                return rightHandQuickSlotItemObjects[currentRightItemIndex];
            }
            else
            {
                currentLeftItemIndex = (currentLeftItemIndex + 1) % QUICK_SLOT_CAPACITY;
                return leftHandQuickSlotItemObjects[currentLeftItemIndex];
            }
        }

        public ItemObject GetCurrentItemObject(bool isRightHand = true)
        {
            if (isRightHand)
            {
                return rightHandQuickSlotItemObjects[currentRightItemIndex];
            }
            else
            {
                return leftHandQuickSlotItemObjects[currentLeftItemIndex];
            }
        }
    }
}
