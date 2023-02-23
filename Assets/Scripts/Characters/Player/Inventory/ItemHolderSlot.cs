using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TMD
{
    public class ItemHolderSlot : MonoBehaviour
    {
        public Transform holderTransform;
        public bool isLeftHand;
        public bool isRightHand;

        public GameObject currentItem;
        public DamageFactor damageFactor;

        private void Awake()
        {
            if (holderTransform == null)
            {
                holderTransform = transform;
            }
        }

        private void DestroyCurrentWeapon()
        {
            if (currentItem)
            {
                damageFactor = null;
                Destroy(currentItem);
            }
        }

        public GameObject LoadItemModel(ItemObject item)
        {
            if (currentItem != null)
            {
                DestroyCurrentWeapon();
                //return null;
            }
            if (item == null)
            {
                return null;
            }
            currentItem = (GameObject) GameObject.Instantiate(item.itemPrefab, holderTransform);
            currentItem.transform.localPosition = Vector3.zero;
            currentItem.transform.localRotation = Quaternion.identity;
            currentItem.transform.localScale = Vector3.one;

            damageFactor = currentItem.GetComponentInChildren<DamageFactor>();
            if (damageFactor)
            {
                damageFactor.SetDamage(((WeaponObject) item).GetDamage());
            }
            return currentItem;
        }
    }
}