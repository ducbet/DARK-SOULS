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

        public void LoadItemOnSlot(Item item, bool isRightHand = true)
        {
            if (isRightHand)
            {
                rightHandSlot.LoadItemModel(item);
            }
            else
            {
                leftHandSlot.LoadItemModel(item);
            }
        }
    }
}
