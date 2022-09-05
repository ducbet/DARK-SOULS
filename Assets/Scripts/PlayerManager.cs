using UnityEngine;

[RequireComponent(typeof(PlayerLocomotion))]
public class PlayerManager : MonoBehaviour
{
    private PlayerLocomotion playerLocomotion;
    private CameraManager cameraManager;

    private void Awake()
    {
        playerLocomotion = GetComponent<PlayerLocomotion>();
        cameraManager = FindObjectOfType<CameraManager>();
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

    private void LateUpdate()
    {
        cameraManager.HandleCameraMovement();
    }
}
