using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TMD
{
    [RequireComponent(typeof(PlayerLocomotion))]
    public class PlayerManager : MonoBehaviour
    {
        private PlayerLocomotion playerLocomotion;


        private void Awake()
        {
            playerLocomotion = GetComponent<PlayerLocomotion>();
        }

        // Update is called once per frame
        void Update()
        {
            playerLocomotion.HandleMovementAnimations();
        }

        private void FixedUpdate()
        {
            playerLocomotion.HandleAllMovements();
        }
    }
}
