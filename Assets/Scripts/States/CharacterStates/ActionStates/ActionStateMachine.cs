using System;
using System.Collections;
using UnityEngine;

namespace TMD
{
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
            //DodgingBack
        };
        [HideInInspector] public Rigidbody rgBody { get; private set; }
        [HideInInspector] public AnimatorManager animatorManager { get; private set; }
        [Header("Roll Attributes")]
        public float rollingVelocityScale = 1f;
        public bool isPlayingAnimation = false;
        public event EventHandler<ActionStateMachine.ACTION_STATE_ENUMS> ActionPerformed;
        public bool isRolling { get; protected set; } = false;


        protected virtual void Awake()
        {
            rgBody = GetComponent<Rigidbody>();
            animatorManager = GetComponent<AnimatorManager>();
            InitStates();
        }
        protected override void Start()
        {
            base.Start();
            SwitchState(ACTION_STATE_ENUMS.Idle);
        }

        protected virtual void OnDestroy()
        {
        }

        public override void SwitchState(Enum stateEnum)
        {
            base.SwitchState(stateEnum);
            ActionPerformed?.Invoke(this, (ActionStateMachine.ACTION_STATE_ENUMS) stateEnum);
        }

        protected virtual void InitStates()
        {
            states = new State[Enum.GetNames(typeof(ACTION_STATE_ENUMS)).Length];
            states[(int)ACTION_STATE_ENUMS.Idle] = new IdleActionState(this);
            states[(int)ACTION_STATE_ENUMS.Rolling] = new RollingState(this);
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
    }
}
