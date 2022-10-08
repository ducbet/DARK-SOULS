using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TMD
{
    public class QuickSlotUI : MonoBehaviour
    {
        public Image leftHandSlotIcon;
        public Image rightHandSlotIcon;

        public void UpdateQuickSlotIcon(ItemObject item, bool isRightHand = true)
        {
            if (isRightHand)
            {
                if (item.itemIcon != null)
                {
                    rightHandSlotIcon.sprite = item.itemIcon;
                    rightHandSlotIcon.enabled = true;
                }
                else
                {
                    rightHandSlotIcon.sprite = null;
                    rightHandSlotIcon.enabled = false;
                }
            }
            else
            {
                if(item.itemIcon != null)
                {
                    leftHandSlotIcon.sprite = item.itemIcon;
                    leftHandSlotIcon.enabled = true;
                }
                else
                {
                    leftHandSlotIcon.sprite = null;
                    leftHandSlotIcon.enabled = false;
                }
            }
        }
    }
}
