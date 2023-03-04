using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TMD
{
    public class MovementStateMachine : StateMachine
    {
        public enum MOVEMENT_STATE_ENUMS {
            // Idle~Sprinting enums used to pass to animation blend tree. Must be put it in the beggin
            Idle, 
            WalkingForward, 
            RunningForward, 
            Sprinting,
            WalkingStrafeLeft,
            RunningStrafeLeft,
            WalkingStrafeRight,
            RunningStrafeRight,
            Laned,
            Rolling,
            DodgingBack,
            PickingUp,
            Jumping,
            Fall,
            Die,
            Attacking, // TODO: will be moved to PlayerActionStateMachine in the future
        };

        [HideInInspector] public AnimatorManager animatorManager { get; private set; }
        [HideInInspector] public Rigidbody rgBody { get; private set; }
        [HideInInspector] public LockOnStateMachine lockOnStateMachine { get; private set; }

        [Header("Check grounded Attributes")]
        private Vector3 groundCheckOriginOffset = new Vector3(0f, 1f, 0f);
        public float startLandingHeight = 1.5f;
        public LayerMask groundCheckLayers;
        public bool isGrounded = true;

        public Vector3 moveDirection { get; protected set; } = Vector3.zero;
        public Vector3 lockingOnDirection { get; protected set; } = Vector3.zero;  // only player has camera. The rest NPC must use this to facing target
        public float moveMagnitude { get; protected set; } = 0f;

        [Header("Translation Attributes")]
        public float runThreshold = 0.55f;
        public float walkingSpeed = 1f;
        public float runningSpeed = 4f;
        public float sprintingSpeed = 7f;

        // Rotation Attributes
        public float rotationSpeed { get; private set; } = 6f;

        public bool isSprinting { get; protected set; } = false;
        public bool isWalking { get; protected set; } = false;
        public bool isRolling { get; protected set; } = false;
        public bool isInteractingObject { get; protected set; } = false;
        public bool isJumping { get; protected set; } = false;

        // TODO: will be moved to PlayerActionStateMachine in the future
        public bool isLeftClick { get; protected set; } = false;
        [HideInInspector] public InventoryManager inventoryManager;
        public float rootMotionSpeed = 1f;
        public bool canStartComboAttack = false;
        [Header("Check For Interactable Object Attr")]
        public float checkObjectInterval = 0.2f;
        public float checkObjectRayThickness = 1f;
        public float checkObjectRayLength = 2f;
        public InteractablePopup interactablePopup;
        public LayerMask interactableLayers;
        public Coroutine checkForInteractableObject;
        public GameObject interactableItem;


        [Header("Roll Attributes")]
        public float rollingVelocityScale = 1f;

        [Header("Falling Attributes")]
        public float fallingVelocity = 33f;
        public Vector3 leapingVelocity;
        public float leapingVelocitySmoothTime = 2f;

        public bool isPlayingAnimation = false;  // animations except Idle, Walking, Running, Sprinting
        public bool canStartFalling = false;

        protected virtual void Awake()
        {
            animatorManager = GetComponent<AnimatorManager>();
            rgBody = GetComponent<Rigidbody>();
            lockOnStateMachine = GetComponent<LockOnStateMachine>();
            inventoryManager = GetComponent<InventoryManager>(); // TODO: will be moved to PlayerActionStateMachine in the future
            if (groundCheckLayers == 0)
            {
                groundCheckLayers = (int)CameraManager.LayerMasks.Ground;
            }


            // TODO: will be moved to PlayerActionStateMachine in the future
            interactablePopup = FindObjectOfType<InteractablePopup>();
            if (interactableLayers == 0)
            {
                interactableLayers = (int)~(CameraManager.LayerMasks.TransparentFX | CameraManager.LayerMasks.IgnoreRaycast |
                    CameraManager.LayerMasks.UI | CameraManager.LayerMasks.Controller | CameraManager.LayerMasks.Ground |
                    CameraManager.LayerMasks.Water | CameraManager.LayerMasks.Environment | CameraManager.LayerMasks.Character);
            }

            InitStates();
        }

        protected override void Start()
        {
            base.Start();
            SwitchState(states[(int)MOVEMENT_STATE_ENUMS.Idle]);

            checkForInteractableObject = StartCoroutine(CheckForInteractableObject());
        }

        protected virtual void OnDestroy()
        {
            // TODO: will be moved to PlayerActionStateMachine in the future
            StopCoroutine(checkForInteractableObject);  // OnDestroy is enough? also when player dead?
        }

        protected override void Update()
        {
            base.Update();
            CheckGrounded();
        }

        private void InitStates()
        {
            states = new State[Enum.GetNames(typeof(MOVEMENT_STATE_ENUMS)).Length];
            states[(int)MOVEMENT_STATE_ENUMS.Idle] = new IdleState(this);
            states[(int)MOVEMENT_STATE_ENUMS.WalkingForward] = new WalkingForwardState(this);
            states[(int)MOVEMENT_STATE_ENUMS.RunningForward] = new RunForwardState(this);
            states[(int)MOVEMENT_STATE_ENUMS.Sprinting] = new SprintState(this);
            states[(int)MOVEMENT_STATE_ENUMS.WalkingStrafeLeft] = new WalkingStrafeLeftState(this);
            states[(int)MOVEMENT_STATE_ENUMS.RunningStrafeLeft] = new RunningStrafeLeftState(this);
            states[(int)MOVEMENT_STATE_ENUMS.WalkingStrafeRight] = new WalkingStrafeRightState(this);
            states[(int)MOVEMENT_STATE_ENUMS.RunningStrafeRight] = new RunningStrafeRightState(this);
            states[(int)MOVEMENT_STATE_ENUMS.Laned] = new LandedState(this);
            states[(int)MOVEMENT_STATE_ENUMS.Fall] = new FallState(this);
            states[(int)MOVEMENT_STATE_ENUMS.Rolling] = new RollingState(this);
            states[(int)MOVEMENT_STATE_ENUMS.DodgingBack] = new DodgingBackState(this);
            states[(int)MOVEMENT_STATE_ENUMS.PickingUp] = new PickingUpState(this);
            states[(int)MOVEMENT_STATE_ENUMS.Jumping] = new JumpState(this);
            states[(int)MOVEMENT_STATE_ENUMS.Die] = new DieState(this);

            // TODO: will be moved to PlayerActionStateMachine in the future
            states[(int)MOVEMENT_STATE_ENUMS.Attacking] = new AttackingState(this);
        }

        private void CheckGrounded()
        {
            RaycastHit hit;
            if (Physics.SphereCast(transform.position + groundCheckOriginOffset, 0.2f, Vector3.down, out hit, startLandingHeight, groundCheckLayers))
            {
                isGrounded = true;
            }
            else
            {
                isGrounded = false;
            }
        }

        public void PlayTargetAnimation(string animationName, float fadeLength = 0.2f)
        {
            isPlayingAnimation = true;
            animatorManager.PlayTargetAnimation(animationName, fadeLength);
        }

        public void PlayTargetAnimation(int animationId, float fadeLength = 0.2f)
        {
            isPlayingAnimation = true;
            animatorManager.PlayTargetAnimation(animationId, fadeLength);
        }

        public void HandleAnimationEndedEvent()
        {
            isPlayingAnimation = false;
        }

        public void CheckCanStartFalling()
        {
            // Use for JumpState
            canStartFalling = true;
        }

        public virtual void CalculateMoveDirection()
        {
        }

        public virtual void CalculateMoveMagnitude()
        {
        }
        public virtual float GetPlayerMovementHorizontal()
        {
            return 0;
        }

        public virtual float GetPlayerMovementVertical()
        {
            return 0;
        }

        public bool IsLockingOn()
        {
            return lockOnStateMachine && lockOnStateMachine.isLockingOn;
        }

        public Vector3 GetLockOnDirection()
        {
            if (lockOnStateMachine == null)
            {
                return Vector3.zero;
            }
            return lockOnStateMachine.GetLockOnDirection();
        }

        #region TODO: will be moved to PlayerActionStateMachine in the future
        public void HandleComboAttack()
        {
            if (isLeftClick)
            {
                canStartComboAttack = true;
            }
        }

        IEnumerator CheckForInteractableObject()
        {
            RaycastHit hit;
            while (true)
            {
                // why interactableLayers while the param is ignore layers???
                if (Physics.SphereCast(transform.position, checkObjectRayThickness,
                    transform.forward, out hit, checkObjectRayLength, interactableLayers))
                {
                    Interactable interactableScript = hit.collider.gameObject.GetComponent<Interactable>();
                    if (interactableScript != null)
                    {
                        interactablePopup.Show(interactableScript.GetPopupMessage());
                        interactableItem = hit.collider.gameObject;
                    }
                }
                else
                {
                    interactableItem = null;
                    interactablePopup.Hide();
                }
                yield return new WaitForSeconds(checkObjectInterval);
            }
        }
        #endregion
    }
}
