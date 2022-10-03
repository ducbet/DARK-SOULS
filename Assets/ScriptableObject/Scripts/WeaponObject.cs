using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TMD
{
    public abstract class WeaponObject : ItemObject
    {
        public abstract int GetDamage();
        public abstract Dictionary<string, string> GetComboAttacks();
        public abstract string GetAttackAnimation(string lastAttackName = "");
    }
}
