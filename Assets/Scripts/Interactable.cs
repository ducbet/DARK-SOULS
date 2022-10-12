using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TMD
{
    public class Interactable : MonoBehaviour
    {
        public float gizmosRadius = 0.5f;
        public ItemObject itemObject;

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, gizmosRadius);
        }

        public virtual ItemObject Interact(Command command)
        {
            Debug.Log("Interact " + itemObject.GetItemName() + " item");
            return itemObject;
        }
    }
}
