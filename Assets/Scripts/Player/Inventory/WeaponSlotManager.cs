using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TMD
{
    public class WeaponSlotManager : MonoBehaviour
    {
        WeaponHolderSlot leftHandSlot;
        WeaponHolderSlot rightHandSlot;

        private void Awake()
        {
            foreach (WeaponHolderSlot weaponHolderSlot in GetComponentsInChildren<WeaponHolderSlot>())
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

        public void LoadItemOnSlot(WeaponItem weaponItem, bool isRightHand = true)
        {
            if (isRightHand)
            {
                rightHandSlot.LoadWeaponModel(weaponItem);
            }
            else
            {
                leftHandSlot.LoadWeaponModel(weaponItem);
            }
        }
    }
}
