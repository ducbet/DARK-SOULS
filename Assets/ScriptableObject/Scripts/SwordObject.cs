using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TMD
{
    [CreateAssetMenu(fileName = "Weapon Item", menuName = "ScriptableObjects/Items/Weapon Items/Sword")]
    public class SwordObject : WeaponObject
    {
        public int damage = 10;
        [Header("Attack Animations")]
        public string straight_sword_oh_light_attack_01 = "straight_sword_oh_light_attack_01";
        public string straight_sword_oh_light_attack_02 = "straight_sword_oh_light_attack_02";
        public string straight_sword_oh_heavy_attack_01 = "straight_sword_oh_heavy_attack_01";
        public string straight_sword_oh_heavy_attack_02 = "straight_sword_oh_heavy_attack_02";

        [Header("Idle Animations")]
        public string leftArmIdleAnimation = "Left Arm Idle";
        public string rightArmIdleAnimation = "Right Arm Idle";

        private Dictionary<string, string> comboAttacks = new Dictionary<string, string>{
            {"straight_sword_oh_light_attack_01", "straight_sword_oh_light_attack_02"},
            {"straight_sword_oh_light_attack_02", "straight_sword_oh_heavy_attack_01"},
            {"straight_sword_oh_heavy_attack_01", "straight_sword_oh_heavy_attack_02"},
            {"straight_sword_oh_heavy_attack_02", "straight_sword_oh_light_attack_01"},
        };

        public override int GetDamage()
        {
            return damage;
        }

        public override Dictionary<string, string> GetComboAttacks()
        {
            return comboAttacks;
        }

        public override string GetAttackAnimation(string lastAttackName = "")
        {
            if (comboAttacks.ContainsKey(lastAttackName))
            {
                return comboAttacks[lastAttackName];
            }
            return straight_sword_oh_light_attack_01;
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
