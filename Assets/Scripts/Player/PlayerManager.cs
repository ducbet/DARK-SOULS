using UnityEngine;

namespace TMD
{
    [RequireComponent(typeof(PlayerLocomotion))]
    [RequireComponent(typeof(PlayerActionManager))]
    public class PlayerManager : MonoBehaviour
    {
        private PlayerLocomotion playerLocomotion;
        private PlayerActionManager playerActionManager;
        private AnimatorManager animatorManager;
        private CameraManager cameraManager;

        private void Awake()
        {
            playerLocomotion = GetComponent<PlayerLocomotion>();
            playerActionManager = GetComponent<PlayerActionManager>();
            animatorManager = GetComponent<AnimatorManager>();
            cameraManager = FindObjectOfType<CameraManager>();
        }

        // Update is called once per frame
        void Update()
        {
            animatorManager.UpdateAnimatorUsingState();
            playerLocomotion.HandleMovementAnimations();
            playerActionManager.HandleAllActions();
        }

        private void FixedUpdate()
        {
            animatorManager.UpdateAnimatorUsingState();
            playerLocomotion.HandleAllMovements();
        }

        private void LateUpdate()
        {
            cameraManager.HandleCameraMovement();
        }
    }
}
