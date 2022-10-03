using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TMD
{
    [CreateAssetMenu(fileName = "Weapon Item", menuName = "ScriptableObjects/Items/Weapon Items/Sword")]
    public class SwordObject : WeaponObject
    {
        public int damage = 10;
        [Header("Idle Animations")]
        public string leftArmIdleAnimation = "Left Arm Idle";
        public string rightArmIdleAnimation = "Right Arm Idle";


        public override int GetDamage()
        {
            return damage;
        }

        public override string GetLeftArmIdleAnimation()
        {
            return leftArmIdleAnimation;
        }

        public override string GetRightArmIdleAnimation()
        {
            return rightArmIdleAnimation;
        }
    }
}
