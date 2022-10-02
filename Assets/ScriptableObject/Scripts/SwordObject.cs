using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TMD
{
    [CreateAssetMenu(fileName = "Weapon Item", menuName = "ScriptableObjects/Items/Weapon Items/Sword")]
    public class SwordObject : WeaponObject
    {
        public int damage = 10;

        public override int GetDamage()
        {
            return damage;
        }
    }
}
