using UnityEngine;

namespace TMD
{
    [RequireComponent(typeof(PlayerLocomotion))]
    [RequireComponent(typeof(PlayerActionManager))]
    public class PlayerManager : MonoBehaviour
    {
        private PlayerLocomotion playerLocomotion;
        private PlayerActionManager playerActionManager;
        private CameraManager cameraManager;

        private void Awake()
        {
            playerLocomotion = GetComponent<PlayerLocomotion>();
            playerActionManager = GetComponent<PlayerActionManager>();
            cameraManager = FindObjectOfType<CameraManager>();
        }

        // Update is called once per frame
        void Update()
        {
            playerLocomotion.HandleMovementAnimations();
            playerActionManager.HandleAllActions();
        }

        private void FixedUpdate()
        {
            playerLocomotion.HandleAllMovements();
        }

        private void LateUpdate()
        {
            cameraManager.HandleCameraMovement();
        }
    }
}
