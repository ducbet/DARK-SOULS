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

        [Header("Roll Attributes")]
        public float rollingVelocityScale = 1f;

        private bool isAnimatorInteracting = false;
        private bool isUsingRootMotion = false;

        private void Awake()
        {
            inputManager = GetComponent<InputManager>();
            animatorManager = GetComponent<AnimatorManager>();
            cameraTransform = Camera.main.transform;
            playerRigidbody = GetComponent<Rigidbody>();
        }

        public void HandleAllMovements()
        {
            isAnimatorInteracting = IsAnimatorInteracting();
            isUsingRootMotion = IsUsingRootMotion();
            if (isUsingRootMotion)
            {
                HandleRootMotionMovements();
            }
            else
            {
                HandleRollingOrDodgeBack();
                HandleMovements();
            }
        }

        public void HandleMovementAnimations()
        {
            animatorManager.SetFloat(animatorManager.movementYParam, (float) GetMovementState());
        }

        private void HandleMovements()
        {
            if (isAnimatorInteracting)
            {
                return;
            }

            if (playerRigidbody.velocity.magnitude == 0 && inputManager.playerMovement.magnitude == 0)
            {
                // do not need to handle movement if player velocity and input is zero (completely indle)
                return;
            }
            Vector3 direction = cameraTransform.forward * inputManager.playerMovementY + cameraTransform.right * inputManager.playerMovementX;
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

        private void HandleRootMotionMovements()
        {
            playerRigidbody.drag = 0;  // drag default is 0 already
            Vector3 velocity = animatorManager.deltaPosition * rollingVelocityScale;
            velocity.y = 0;
            playerRigidbody.velocity = velocity;
        }

        private void HandleRollingOrDodgeBack()
        {
            if (isAnimatorInteracting)
            {
                return;
            }
            if (!inputManager.isRolling)
            {
                return;
            }
            if (GetMovementState() == MOVEMENT_STATE.Idle)
            {
                animatorManager.PlayTargetAnimation(animatorManager.dodgeBackAnimation, true, true);
            }
            else
            {
                animatorManager.PlayTargetAnimation(animatorManager.rollAnimation, true, true);
            }
        }


        private bool IsAnimatorInteracting()
        {
            return animatorManager.GetBool(animatorManager.isInteractingParam);
        }
        private bool IsUsingRootMotion()
        {
            return animatorManager.GetBool(animatorManager.usingRootMotionParam);
        }
        private MOVEMENT_STATE GetMovementState()
        {
            float inputManitude = inputManager.playerMovement.magnitude;
            if (inputManitude == 0)
            {
                // Put Idle first because player is idle in most of the time -> better performance
                return MOVEMENT_STATE.Idle;
            }
            if (inputManitude > runThreshold)
            {
                if (inputManager.isSprinting)
                {
                    // Nothing to do. Make some changes to commit
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
