using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace TMD
{
    public class Interactable : MonoBehaviour
    {
        public float gizmosRadius = 0.5f;
        public GameObject instanceRoot;
        public ItemObject itemObject;

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, gizmosRadius);
        }
        public ItemObject GetItem()
        {
            return itemObject;
        }

        public virtual void Interact(Command command)
        {
            Debug.Log("Interact " + itemObject.GetItemName() + " item");
        }

        public virtual string GetPopupMessage()
        {
            return "";
        }
    }
}
