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
    }
}
