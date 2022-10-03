using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TMD
{
    public class SlotManager : MonoBehaviour
    {
        ItemHolderSlot leftHandSlot;
        ItemHolderSlot rightHandSlot;

        private void Awake()
        {
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
                return rightHandSlot.LoadItemModel(item);
            }
            else
            {
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
