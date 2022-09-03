using UnityEngine;


namespace TMD
{
    public class InputManager : MonoBehaviour
    {
        [HideInInspector] public Vector2 movement;
        [HideInInspector] public float movementX;
        [HideInInspector] public float movementY;

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
                movement = context.ReadValue<Vector2>();
                movementX = movement.x;
                movementY = movement.y;
            };
            playerControls.PlayerMovement.Sprint.performed += context => isSprinting = true;
            playerControls.PlayerMovement.Sprint.canceled += context => isSprinting = false;
            playerControls.PlayerMovement.Walk.performed += context => isWalking = true;
            playerControls.PlayerMovement.Walk.canceled += context => isWalking = false;

            playerControls.Enable();
        }


        private void OnDisable()
        {
            playerControls.Disable();
        }
    }
}

