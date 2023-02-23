using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TMD
{
    public class CharacterManager : MonoBehaviour
    {
        public Transform lockOnTransform;
        // Start is called before the first frame update
        void Start()
        {
            lockOnTransform = GetComponentInChildren<LockableOn>().transform;
        }
    }
}
