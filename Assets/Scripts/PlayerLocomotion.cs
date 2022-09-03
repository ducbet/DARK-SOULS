using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TMD
{
    [RequireComponent(typeof(InputManager))]
    [RequireComponent(typeof(AnimatorManager))]
    public class PlayerLocomotion : MonoBehaviour
    {
        [HideInInspector] public InputManager inputManager;
        [HideInInspector] public AnimatorManager animatorManager;
        private Transform cameraTransform;
        private Rigidbody playerRigidbody;

        enum MOVEMENT_STATE { Idle, Walking, Running, Sprinting };
        [Header("Tralation Attributes")]
        public float runThreshold = 0.55f;
        public float walkingSpeed = 1f;
        public float runningSpeed = 4f;
        public float sprintingSpeed = 7f;

        [Header("Rotation Attributes")]
        public float rotationSpeed = 6f;

        private void Awake()
        {
            inputManager = GetComponent<InputManager>();
            animatorManager = GetComponent<AnimatorManager>();
            cameraTransform = Camera.main.transform;
            playerRigidbody = GetComponent<Rigidbody>();
        }

        public void HandleAllMovements()
        {
            HandleMovements();
        }

        public void HandleMovementAnimations()
        {
            animatorManager.SetFloat(animatorManager.movementYParam, (float)GetMovementState());
        }

        private void HandleMovements()
        {
            Vector3 direction = cameraTransform.forward * inputManager.movementY + cameraTransform.right * inputManager.movementX;
            direction.y = 0;
            direction.Normalize();

            // Handle translation
            playerRigidbody.velocity = direction * GetMovementSpeed();

            // Handle rotation
            if (direction == Vector3.zero)
            {
                return;
            }
            Quaternion lookDirection = Quaternion.LookRotation(direction);
            lookDirection = Quaternion.Slerp(transform.rotation, lookDirection, rotationSpeed * Time.deltaTime);
            transform.rotation = lookDirection;
        }


        private MOVEMENT_STATE GetMovementState()
        {
            float inputManitude = inputManager.movement.magnitude;
            if (inputManitude == 0)
            {
                // Put Idle first because player is idle in most of the time -> better performance
                return MOVEMENT_STATE.Idle;
            }
            if (inputManitude > runThreshold)
            {
                if (inputManager.isSprinting)
                {
                    return MOVEMENT_STATE.Sprinting;
                }
                if (inputManager.isWalking)
                {
                    return MOVEMENT_STATE.Walking;
                }
                return MOVEMENT_STATE.Running;
            }
            if (inputManitude > 0)
            {
                return MOVEMENT_STATE.Walking;
            }
            return MOVEMENT_STATE.Idle;
        }

        private float GetMovementSpeed()
        {
            MOVEMENT_STATE state = GetMovementState();
            if (state == MOVEMENT_STATE.Idle)
            {
                return 0;
            }
            if (state == MOVEMENT_STATE.Sprinting)
            {
                return sprintingSpeed;
            }
            if (state == MOVEMENT_STATE.Running)
            {
                return runningSpeed;
            }
            if (state == MOVEMENT_STATE.Walking)
            {
                return walkingSpeed;
            }
            return 0;
        }
    }

}
