using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TMD
{
    public abstract class ItemObject : ScriptableObject
    {
        public GameObject itemPrefab;
        [Header("Item Attributes")]
        public Sprite itemIcon;

        private string leftArmEmptyAnimation = "Left Arm Empty";
        private string righArmEmptAnimation = "Right Arm Empty";

        public string GetItemName()
        {
            return itemPrefab.name;
        }

        public string GetLeftArmEmptyAnimation()
        {
            return leftArmEmptyAnimation;
        }

        public string GetRightArmEmptyAnimation()
        {
            return righArmEmptAnimation;
        }
        public abstract string GetLeftArmIdleAnimation();
        public abstract string GetRightArmIdleAnimation();

    }
}
