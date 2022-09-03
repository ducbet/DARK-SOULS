using UnityEngine;


namespace TMD
{
    public class InputManager : MonoBehaviour
    {
        [HideInInspector] public Vector2 playerMovement;
        [HideInInspector] public float playerMovementX;
        [HideInInspector] public float playerMovementY;

        [HideInInspector] public Vector2 cameraRotation;
        [HideInInspector] public float cameraRotationX;
        [HideInInspector] public float cameraRotationY;

        [HideInInspector] public bool isWalking;
        [HideInInspector] public bool isSprinting;

        private PlayerControls playerControls;

        private void OnEnable()
        {
            if (playerControls == null)
            {
                playerControls = new PlayerControls();
            }
            playerControls.PlayerMovement.Movement.performed += context =>
            {
                playerMovement = context.ReadValue<Vector2>();
                playerMovementX = playerMovement.x;
                playerMovementY = playerMovement.y;
            };
            playerControls.PlayerMovement.Sprint.performed += context => isSprinting = true;
            playerControls.PlayerMovement.Sprint.canceled += context => isSprinting = false;
            playerControls.PlayerMovement.Walk.performed += context => isWalking = true;
            playerControls.PlayerMovement.Walk.canceled += context => isWalking = false;

            playerControls.CameraMovement.Rotation.performed += context =>
            {
                cameraRotation = context.ReadValue<Vector2>();
                cameraRotationX = cameraRotation.y;
                cameraRotationY = cameraRotation.x;
            };

            playerControls.Enable();
        }


        private void OnDisable()
        {
            playerControls.Disable();
        }
    }
}

