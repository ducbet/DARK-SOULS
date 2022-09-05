using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TMD
{
    public class WeaponHolderSlot : MonoBehaviour
    {
        public Transform holderTransform;
        public bool isLeftHand;
        public bool isRightHand;

        public GameObject currentWeapon;
        private void Awake()
        {
            if (holderTransform == null)
            {
                holderTransform = transform;
            }
        }

        private void DestroyCurrentWeapon()
        {
            if (currentWeapon)
            {
                Destroy(currentWeapon);
            }
        }

        public void LoadWeaponModel(WeaponItem weaponItem)
        {
            if (currentWeapon != null)
            {
                DestroyCurrentWeapon();
                return;
            }
            currentWeapon = (GameObject) GameObject.Instantiate(weaponItem.weaponPrefab, holderTransform);
            currentWeapon.transform.localPosition = Vector3.zero;
            currentWeapon.transform.localRotation = Quaternion.identity;
            currentWeapon.transform.localScale = Vector3.one;
        }
    }
}
