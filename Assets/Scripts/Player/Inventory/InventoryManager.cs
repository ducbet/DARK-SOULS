using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TMD
{
    [RequireComponent(typeof(WeaponSlotManager))]
    public class InventoryManager : MonoBehaviour
    {
        public WeaponItem leftHandWeapon;
        public WeaponItem rightHandWeapon;

        private WeaponSlotManager weaponSlotManager;

        private void Awake()
        {
            weaponSlotManager = GetComponent<WeaponSlotManager>();
        }

        // Start is called before the first frame update
        void Start()
        {
            weaponSlotManager.LoadItemOnSlot(rightHandWeapon);
            weaponSlotManager.LoadItemOnSlot(leftHandWeapon, isRightHand: false);
        }
    }
}
