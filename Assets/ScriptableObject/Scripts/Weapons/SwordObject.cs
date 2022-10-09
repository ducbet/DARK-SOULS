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

        private int defaultStaminaCost = 10;
        private Dictionary<string, int> staminaCosts = new Dictionary<string, int>{
            {"straight_sword_oh_light_attack_01", 10},
            {"straight_sword_oh_light_attack_02", 10},
            {"straight_sword_oh_heavy_attack_01", 15},
            {"straight_sword_oh_heavy_attack_02", 15},
        };

        public override int GetDamage()
        {
            return damage;
        }

        public override int GetStaminaCost(string animationName)
        {
            if (staminaCosts.ContainsKey(animationName))
            {
                return staminaCosts[animationName];
            }
            return defaultStaminaCost;
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
