using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TMD
{
    [RequireComponent(typeof(SlotManager))]
    public class InventoryManager : MonoBehaviour
    {
        public ItemObject leftHandItemObject;
        public ItemObject rightHandItemObject;

        [HideInInspector] public GameObject leftHandItem;
        [HideInInspector] public GameObject rightHandItem;

        [HideInInspector] public AnimatorManager animatorManager;

        private SlotManager slotManager;
        private void Awake()
        {
            slotManager = GetComponent<SlotManager>();
            animatorManager = GetComponent<AnimatorManager>();
        }

        // Start is called before the first frame update
        public void EquipItems()
        {
            if (rightHandItemObject)
            {
                rightHandItem = slotManager.LoadItemOnSlot(rightHandItemObject);
                if (rightHandItem)
                {
                    animatorManager.CrossFade(rightHandItemObject.GetRightArmIdleAnimation());
                }
                else
                {
                    animatorManager.CrossFade(rightHandItemObject.GetRightArmIdleAnimation());
                }
            }
            if (leftHandItemObject)
            {
                leftHandItem = slotManager.LoadItemOnSlot(leftHandItemObject, isRightHand: false);
                if (leftHandItem)
                {
                    animatorManager.CrossFade(leftHandItemObject.GetLeftArmIdleAnimation());
                }
                else
                {
                    animatorManager.CrossFade(leftHandItemObject.GetLeftArmEmptyAnimation());
                }
            }
        }
    }
}
