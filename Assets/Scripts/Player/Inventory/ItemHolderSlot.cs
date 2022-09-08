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
                Destroy(currentItem);
            }
        }

        public void LoadItemModel(Item item)
        {
            if (currentItem != null)
            {
                DestroyCurrentWeapon();
                return;
            }
            if (item == null)
            {
                return;
            }
            currentItem = (GameObject) GameObject.Instantiate(item.itemPrefab, holderTransform);
            currentItem.transform.localPosition = Vector3.zero;
            currentItem.transform.localRotation = Quaternion.identity;
            currentItem.transform.localScale = Vector3.one;
        }
    }
}
