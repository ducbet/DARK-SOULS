using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TMD
{
    public abstract class ItemObject : ScriptableObject
    {
        public GameObject itemPrefab;
        [Header("Item Attributes")]
        public string itemName;
        public Sprite itemIcon;

        [HideInInspector] private string leftArmEmptyAnimation = "Left Arm Empty";
        [HideInInspector] private string righArmEmptAnimation = "Right Arm Empty";

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
