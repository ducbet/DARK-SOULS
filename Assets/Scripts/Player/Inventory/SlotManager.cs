using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TMD
{
    public class SlotManager : MonoBehaviour
    {
        ItemHolderSlot leftHandSlot;
        ItemHolderSlot rightHandSlot;
        private QuickSlotUI quickSlotUI;

        private void Awake()
        {
            quickSlotUI = FindObjectOfType<QuickSlotUI>();
            foreach (ItemHolderSlot weaponHolderSlot in GetComponentsInChildren<ItemHolderSlot>())
            {
                if (weaponHolderSlot.isLeftHand)
                {
                    leftHandSlot = weaponHolderSlot;
                }
                else
                {
                    rightHandSlot = weaponHolderSlot;
                }
            }
        }

        public GameObject LoadItemOnSlot(ItemObject item, bool isRightHand = true)
        {
            if (isRightHand)
            {
                quickSlotUI.UpdateQuickSlotIcon(item);
                return rightHandSlot.LoadItemModel(item);
            }
            else
            {
                quickSlotUI.UpdateQuickSlotIcon(item, isRightHand: false);
                return leftHandSlot.LoadItemModel(item);
            }
        }

        #region Handle Weapon Damage Factor
        public void EnableRightHandDamageFactor()
        {
            rightHandSlot.damageFactor.EnableDamageCollider();
        }

        public void DisableRightHandDamageFactor()
        {
            rightHandSlot.damageFactor.DisableDamageCollider();
        }

        public void EnableLeftHandDamageFactor()
        {
            leftHandSlot.damageFactor.EnableDamageCollider();
        }

        public void DisableLeftHandDamageFactor()
        {
            leftHandSlot.damageFactor.DisableDamageCollider();
        }
        #endregion
    }
}
