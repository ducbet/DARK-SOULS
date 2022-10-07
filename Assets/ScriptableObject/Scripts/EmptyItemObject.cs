using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TMD
{
    [CreateAssetMenu(fileName = "Empty Item", menuName = "ScriptableObjects/Items/Empty Item")]
    public class EmptyItemObject : ItemObject
    {
        public override string GetLeftArmIdleAnimation()
        {
            return "";
        }

        public override string GetRightArmIdleAnimation()
        {
            return "";
        }
    }
}
