using UnityEngine;

namespace TMD
{
    [RequireComponent(typeof(PlayerActionManager))]
    [RequireComponent(typeof(AnimatorManager))]
    [RequireComponent(typeof(PlayerMovementStateMachine))]
    [RequireComponent(typeof(PlayerLockOnStateMachine))]
    public class PlayerManager : CharacterManager
    {
        public static string PLAYER_TAG = "Player";

        private CameraManager cameraManager;

        private void Awake()
        {
            cameraManager = FindObjectOfType<CameraManager>();
        }

        // Update is called once per frame
        void Update()
        {
        }

        private void FixedUpdate()
        {
        }

        private void LateUpdate()
        {
            cameraManager.HandleCameraMovement();
        }
    }
}
