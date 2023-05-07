using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TMD
{
    public class MovementStateMachine : StateMachine
    {
        public enum MOVEMENT_STATE_ENUMS
        {
            // Idle~Sprinting enums used to pass to animation blend tree. Must be put it in the beggin
            Idle,
            WalkingForward,
            RunningForward,
            Sprinting,
            WalkingStrafeLeft,
            RunningStrafeLeft,
            WalkingStrafeRight,
            RunningStrafeRight,
            Empty
        };

        [HideInInspector] public AnimatorManager animatorManager { get; private set; }
        [HideInInspector] public Rigidbody rgBody { get; private set; }
        [HideInInspector] public LockOnStateMachine lockOnStateMachine { get; private set; }
        [HideInInspector] public ActionStateMachine actionStateMachine { get; private set; }

        public Vector3 moveDirection { get; protected set; } = Vector3.zero;
        public Vector3 lockingOnDirection { get; protected set; } = Vector3.zero;  // only player has camera. The rest NPC must use this to facing target
        public float moveMagnitude { get; set; } = 0f;

        [Header("Translation Attributes")]
        public float runThreshold = 0.55f;
        public float walkingSpeed = 1f;
        public float runningSpeed = 4f;
        public float sprintingSpeed = 7f;

        public Vector3 leapingVelocity;
        public float leapingVelocitySmoothTime = 2f;

        // Rotation Attributes
        public float rotationSpeed { get; private set; } = 6f;

        public bool isSprinting { get; protected set; } = false;
        public bool isWalking { get; protected set; } = false;
        public bool isJumping { get; protected set; } = false;  // move to action state machine

        [HideInInspector] public InventoryManager inventoryManager;

        [Header("Roll Attributes")]
        public float rollingVelocityScale = 1f;

        public bool isMovementBlocked = true;
        public bool isRotationBlocked = true;
        public bool isPlayingAnimation = false;  // animations except Idle, Walking, Running, Sprinting
        public event EventHandler<MovementStateMachine.MOVEMENT_STATE_ENUMS> OnMoving;

        protected virtual void Awake()
        {
            animatorManager = GetComponent<AnimatorManager>();
            rgBody = GetComponent<Rigidbody>();
            lockOnStateMachine = GetComponent<LockOnStateMachine>();
            actionStateMachine = GetComponent<ActionStateMachine>();
            if (actionStateMachine)
            {
                actionStateMachine.ActionPerformed += ActionPerformed;
            }
            inventoryManager = GetComponent<InventoryManager>(); // TODO: will be moved to PlayerActionStateMachine in the future

            InitStates();
        }

        protected override void Start()
        {
            base.Start();
            SwitchState(MOVEMENT_STATE_ENUMS.Idle);
        }

        protected virtual void OnDestroy()
        {
        }

        protected override void Update()
        {
            base.Update();
        }
        public override void SwitchState(Enum stateEnum)
        {
            base.SwitchState(stateEnum);
            OnMoving?.Invoke(this, (MOVEMENT_STATE_ENUMS)stateEnum);
        }

        private void InitStates()
        {
            states = new State[Enum.GetNames(typeof(MOVEMENT_STATE_ENUMS)).Length];
            states[(int)MOVEMENT_STATE_ENUMS.Idle] = new IdleState(this, (int)MOVEMENT_STATE_ENUMS.Idle);
            states[(int)MOVEMENT_STATE_ENUMS.WalkingForward] = new WalkingForwardState(this, (int)MOVEMENT_STATE_ENUMS.WalkingForward);
            states[(int)MOVEMENT_STATE_ENUMS.RunningForward] = new RunForwardState(this, (int)MOVEMENT_STATE_ENUMS.RunningForward);
            states[(int)MOVEMENT_STATE_ENUMS.Sprinting] = new SprintState(this, (int)MOVEMENT_STATE_ENUMS.Sprinting);
            states[(int)MOVEMENT_STATE_ENUMS.WalkingStrafeLeft] = new WalkingStrafeLeftState(this, (int)MOVEMENT_STATE_ENUMS.WalkingStrafeLeft);
            states[(int)MOVEMENT_STATE_ENUMS.RunningStrafeLeft] = new RunningStrafeLeftState(this, (int)MOVEMENT_STATE_ENUMS.RunningStrafeLeft);
            states[(int)MOVEMENT_STATE_ENUMS.WalkingStrafeRight] = new WalkingStrafeRightState(this, (int)MOVEMENT_STATE_ENUMS.WalkingStrafeRight);
            states[(int)MOVEMENT_STATE_ENUMS.RunningStrafeRight] = new RunningStrafeRightState(this, (int)MOVEMENT_STATE_ENUMS.RunningStrafeRight);
            states[(int)MOVEMENT_STATE_ENUMS.Empty] = new MovementEmptyState(this, (int)MOVEMENT_STATE_ENUMS.Empty);
        }

        public void ActionPerformed(object sender, ActionEventParams actionEventParams)
        {
            isMovementBlocked = actionEventParams.isMovementBlocked;
            isRotationBlocked = actionEventParams.isRotationBlocked;
            //Debug.Log("isMovingBlocked " + isMovementBlocked);
            //Debug.Log("isRotationBlocked " + isRotationBlocked);
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

    }
}
