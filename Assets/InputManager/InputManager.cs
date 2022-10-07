using UnityEngine;

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
    [HideInInspector] public bool isRolling;

    [HideInInspector] public bool isLeftClick;
    [HideInInspector] public bool isUpArrow;
    [HideInInspector] public bool isDownArrow;
    [HideInInspector] public bool isLeftArrow;
    [HideInInspector] public bool isRightArrow;

    public PlayerControls playerControls;

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
        playerControls.PlayerMovement.Roll.performed += context => isRolling = true;
        playerControls.PlayerMovement.Roll.canceled += context => isRolling = false;


        playerControls.PlayerAction.LeftClick.performed += context => isLeftClick = true;
        playerControls.PlayerAction.LeftClick.canceled += context => isLeftClick = false;

        playerControls.PlayerAction.UpArrow.performed += context => isUpArrow = true;
        playerControls.PlayerAction.UpArrow.canceled += context => isUpArrow = false;
        playerControls.PlayerAction.DownArrow.performed += context => isDownArrow = true;
        playerControls.PlayerAction.DownArrow.canceled += context => isDownArrow = false;
        playerControls.PlayerAction.LeftArrow.performed += context => isLeftArrow = true;
        playerControls.PlayerAction.LeftArrow.canceled += context => isLeftArrow = false;
        playerControls.PlayerAction.RightArrow.performed += context => isRightArrow = true;
        playerControls.PlayerAction.RightArrow.canceled += context => isRightArrow = false;

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
