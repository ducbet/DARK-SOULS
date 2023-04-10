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
            Idle,
            Rolling,
            Landing,
            PickingUp,
            Attacking,
            Dying,
            //Jumping,
            DodgingBack,
        };


        [HideInInspector] public Rigidbody rgBody { get; private set; }
        [HideInInspector] public AnimatorManager animatorManager { get; private set; }
        [HideInInspector] public MovementStateMachine movementStateMachine { get; private set; }
        [HideInInspector] public InventoryManager inventoryManager;
        [Header("Roll Attributes")]
        public float rollingVelocityScale = 1f;
        public bool isPlayingAnimation = false;
        public event EventHandler<ActionEventParams> ActionPerformed;
        public Coroutine checkForInteractableObject;
        public bool isMoving { get; set; } = false;
        public bool isRolling { get; protected set; } = false;
        public bool isInteractingObject { get; protected set; } = false;

        [Header("Check For Interactable Object Attr")]
        public float checkObjectInterval = 0.2f;
        public float checkObjectRayThickness = 1f;
        public float checkObjectRayLength = 2f;

        public InteractablePopup interactablePopup;
        public LayerMask interactableLayers;
        public GameObject interactableItem;

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

            InitStates();
        }
        protected override void Start()
        {
            base.Start();
            SwitchState(ACTION_STATE_ENUMS.Idle);
            checkForInteractableObject = StartCoroutine(CheckForInteractableObject());
        }

        protected virtual void OnDestroy()
        {
            StopCoroutine(checkForInteractableObject);  // OnDestroy is enough? also when player dead?
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
            states[(int)ACTION_STATE_ENUMS.Idle] = new IdleActionState(this);
            states[(int)ACTION_STATE_ENUMS.Rolling] = new RollingState(this);
            states[(int)ACTION_STATE_ENUMS.PickingUp] = new PickingUpState(this);
            states[(int)ACTION_STATE_ENUMS.DodgingBack] = new DodgingBackState(this);
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
        public void OnMovingHandler(object sender, MovementStateMachine.MOVEMENT_STATE_ENUMS newMovementState)
        {
            if (newMovementState == MovementStateMachine.MOVEMENT_STATE_ENUMS.Idle)
            {
                isMoving = false;
                return;
            }
            isMoving = true;
        }
    }
}
