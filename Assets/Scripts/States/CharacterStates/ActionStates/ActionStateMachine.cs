using System;
using System.Collections;
using UnityEngine;

namespace TMD
{
    public class ActionEventParams : EventArgs
    {
        public ActionStateMachine.ACTION_STATE_ENUMS newActionState;
        public bool isMovementBlocked;
        public bool isRotationBlocked;
    }

    public class ActionStateMachine : StateMachine
    {
        public enum ACTION_STATE_ENUMS
        {
            Empty,
            Rolling,
            PickingUp,
            DodgingBack,
            Die,
            Laned,
            Jumping,
            Fall,
            Attacking,
        };


        [HideInInspector] public Rigidbody rgBody { get; private set; }
        [HideInInspector] public AnimatorManager animatorManager { get; private set; }
        [HideInInspector] public MovementStateMachine movementStateMachine { get; private set; }
        [HideInInspector] public InventoryManager inventoryManager;
        public event EventHandler<ActionEventParams> ActionPerformed;

        [Header("Roll Attributes")]
        public float rollingVelocityScale = 1f;
        public bool isPlayingAnimation = false;

        public bool isMoving { get; set; } = false;
        public bool isRolling { get; protected set; } = false;
        public bool isInteractingObject { get; protected set; } = false;
        public Coroutine checkForInteractableObject;
        public bool isLeftClick { get; protected set; } = false;
        public float rootMotionSpeed = 1f;
        public bool canStartComboAttack = false;

        [Header("Check For Interactable Object Attr")]
        public float checkObjectInterval = 0.2f;
        public float checkObjectRayThickness = 1f;
        public float checkObjectRayLength = 2f;

        [Header("Check grounded Attributes")]
        private Vector3 groundCheckOriginOffset = new Vector3(0f, 1f, 0f);
        private float startLandingHeight = 1.2f;
        public LayerMask groundCheckLayers;
        public bool isGrounded = true;

        [Header("Falling Attributes")]
        public float fallingVelocity = 33f;
        public Vector3 leapingVelocity;
        public float leapingVelocitySmoothTime = 2f;

        public InteractablePopup interactablePopup;
        public LayerMask interactableLayers;
        public GameObject interactableItem;

        public float falledTime = 0f;
        public bool canStartFalling = false;
        public bool isJumping { get; protected set; } = false;
        public float jumpUpVelocityScale = 1.5f;

        protected virtual void Awake()
        {
            rgBody = GetComponent<Rigidbody>();
            animatorManager = GetComponent<AnimatorManager>();
            inventoryManager = GetComponent<InventoryManager>();
            movementStateMachine = GetComponent<MovementStateMachine>();
            if (movementStateMachine)
            {
                movementStateMachine.OnMoving += OnMovingHandler;
            }

            interactablePopup = FindObjectOfType<InteractablePopup>();
            if (interactableLayers == 0)
            {
                interactableLayers = (int)~(CameraManager.LayerMasks.TransparentFX | CameraManager.LayerMasks.IgnoreRaycast |
                    CameraManager.LayerMasks.UI | CameraManager.LayerMasks.Controller | CameraManager.LayerMasks.Ground |
                    CameraManager.LayerMasks.Water | CameraManager.LayerMasks.Environment | CameraManager.LayerMasks.Character);
            }
            if (groundCheckLayers == 0)
            {
                groundCheckLayers = (int)CameraManager.LayerMasks.Ground;
            }

            InitStates();
        }
        protected override void Start()
        {
            base.Start();
            SwitchState(ACTION_STATE_ENUMS.Empty);
            checkForInteractableObject = StartCoroutine(CheckForInteractableObject());

        }

        protected virtual void OnDestroy()
        {
            StopCoroutine(checkForInteractableObject);  // OnDestroy is enough? also when player dead?
        }
        protected override void Update()
        {
            base.Update();
            CheckGrounded();
        }

        public override void SwitchState(Enum stateEnum)
        {
            base.SwitchState(stateEnum);
            ActionPerformed?.Invoke(this,
                new ActionEventParams
                {
                    newActionState = (ACTION_STATE_ENUMS)stateEnum,
                    isMovementBlocked = ((ActionState) currentState).isMovementBlocked(),
                    isRotationBlocked = ((ActionState) currentState).isRotationBlocked(),
                });
        }

        protected virtual void InitStates()
        {
            states = new State[Enum.GetNames(typeof(ACTION_STATE_ENUMS)).Length];
            states[(int)ACTION_STATE_ENUMS.Empty] = new ActionEmptyState(this, (int)ACTION_STATE_ENUMS.Empty);
            states[(int)ACTION_STATE_ENUMS.Rolling] = new RollingState(this, (int)ACTION_STATE_ENUMS.Rolling);
            states[(int)ACTION_STATE_ENUMS.PickingUp] = new PickingUpState(this, (int)ACTION_STATE_ENUMS.PickingUp);
            states[(int)ACTION_STATE_ENUMS.DodgingBack] = new DodgingBackState(this, (int)ACTION_STATE_ENUMS.DodgingBack);
            states[(int)ACTION_STATE_ENUMS.Die] = new DieState(this, (int)ACTION_STATE_ENUMS.Die);
            states[(int)ACTION_STATE_ENUMS.Laned] = new LandedState(this, (int)ACTION_STATE_ENUMS.Laned);
            states[(int)ACTION_STATE_ENUMS.Jumping] = new JumpState(this, (int)ACTION_STATE_ENUMS.Jumping);
            states[(int)ACTION_STATE_ENUMS.Fall] = new FallState(this, (int)ACTION_STATE_ENUMS.Fall);
            states[(int)ACTION_STATE_ENUMS.Attacking] = new AttackingState(this, (int)ACTION_STATE_ENUMS.Attacking);
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
            // called from animation
            isPlayingAnimation = false;
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

        public int GetCurrentMovementStateIndex()
        {
            if (movementStateMachine == null)
            {
                return -1;
            }
            return movementStateMachine.currentState.stateIndex;
        }

        public State GetCurrentMovementState()
        {
            if (movementStateMachine == null)
            {
                return null;
            }
            return movementStateMachine.currentState;
        }

        public void OnMovingHandler(object sender, MovementStateMachine.MOVEMENT_STATE_ENUMS newMovementState)
        {
            if (newMovementState == MovementStateMachine.MOVEMENT_STATE_ENUMS.Idle)
            {
                isMoving = false;
                return;
            }
            isMoving = true;
        }
        public void CheckCanStartFalling()
        {
            // Use for JumpState
            canStartFalling = true;
        }
        private void CheckGrounded()
        {
            RaycastHit hit;
            Debug.DrawLine(transform.position + groundCheckOriginOffset, transform.position + groundCheckOriginOffset + (Vector3.down * startLandingHeight), Color.magenta);
            if (Physics.SphereCast(transform.position + groundCheckOriginOffset, 0.2f, Vector3.down, out hit, startLandingHeight, groundCheckLayers))
            {
                isGrounded = true;
            }
            else
            {
                isGrounded = false;
            }
        }
        public void HandleComboAttack()
        {
            if (isLeftClick)
            {
                canStartComboAttack = true;
            }
        }
        public bool IsEmptyAction()
        {
            return this.currentState.stateIndex == (int)ACTION_STATE_ENUMS.Empty;
        }
    }
}
