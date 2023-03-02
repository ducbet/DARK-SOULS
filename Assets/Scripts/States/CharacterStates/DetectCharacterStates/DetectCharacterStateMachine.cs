using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TMD
{
    public class DetectCharacterStateMachine : StateMachine
    {
        public enum DETECT_CHARACTER_STATE_ENUMS
        {
            DetectingState,
            FoundState,
            StopDetecting  // resting state. Have to change DetectingState by trigger,...
        };

        [HideInInspector] public Transform foundTarget { get; set; }
        public bool isStopDetecting = false;
        public LayerMask characterLayer;

        private IEnumerator validateTargetCoroutine = null; // check target is obstacled, too far away,... constantly after found
        private IEnumerator detectingTargetCoroutine = null; // find target

        protected virtual void Awake()
        {
            InitStates();
        }
        protected override void Start()
        {
            base.Start();
            SwitchState(states[(int)DETECT_CHARACTER_STATE_ENUMS.DetectingState]);

            characterLayer = (int)CameraManager.LayerMasks.Character;
        }

        protected virtual void InitStates()
        {
            states = new State[Enum.GetNames(typeof(DETECT_CHARACTER_STATE_ENUMS)).Length];
            states[(int)DETECT_CHARACTER_STATE_ENUMS.DetectingState] = new DetectingCharacterState(this);
            states[(int)DETECT_CHARACTER_STATE_ENUMS.FoundState] = new FoundCharacterState(this);
            states[(int)DETECT_CHARACTER_STATE_ENUMS.StopDetecting] = new StopDetectingCharacterState(this);
        }


        public void StartValidatingFoundTarget()
        {
            validateTargetCoroutine = ValidateFoundTarget();
            StartCoroutine(validateTargetCoroutine);
        }
        public void StopValidatingFoundTarget()
        {
            if (validateTargetCoroutine == null)
            {
                return;
            }
            StopCoroutine(validateTargetCoroutine);
        }

        public IEnumerator ValidateFoundTarget()
        {
            // used after detecting and start validating found target
            while (foundTarget != null)
            {
                if (isStopDetecting)
                {
                    StopDetecting();
                    yield break;
                }
                if (((DetectingCharacterState)states[(int)DETECT_CHARACTER_STATE_ENUMS.DetectingState]).IsTargetValid(foundTarget) == false)
                {
                    SwitchState(DETECT_CHARACTER_STATE_ENUMS.DetectingState);
                    yield break;
                }
                yield return new WaitForSeconds(0.5f);
            }

            //  shouldn't be executed
            if (isStopDetecting)
            {
                StopDetecting();
            }
            else
            {
                SwitchState(DETECT_CHARACTER_STATE_ENUMS.DetectingState);
            }
        }
        public void StartDetectingAgain()
        {
            if (State.IsAssignableFromState<StopDetectingCharacterState>(currentState))
            {
                SwitchState(DETECT_CHARACTER_STATE_ENUMS.DetectingState);
            }
        }

        public void StopDetecting()
        {
            if (State.IsAssignableFromState<StopDetectingCharacterState>(currentState))
            {
                return;
            }
            SwitchState(DETECT_CHARACTER_STATE_ENUMS.StopDetecting);
        }

        public void StartDetectingTarget()
        {
            detectingTargetCoroutine = DetectTarget();
            StartCoroutine(detectingTargetCoroutine);
        }
        public void StopDetectingTarget()
        {
            if (detectingTargetCoroutine == null)
            {
                return;
            }
            StopCoroutine(detectingTargetCoroutine);
        }

        public IEnumerator DetectTarget()
        {
            while (foundTarget == null)
            {
                if (isStopDetecting)
                {
                    SwitchState(DETECT_CHARACTER_STATE_ENUMS.StopDetecting);
                    yield break;
                }
                if (((DetectingCharacterState)states[(int)DETECT_CHARACTER_STATE_ENUMS.DetectingState]).DetectTarget() != null)
                {
                    SwitchState(DETECT_CHARACTER_STATE_ENUMS.FoundState);
                    yield break;
                }
                yield return new WaitForSeconds(1f);
            }
            //  shouldn't be executed
            if (isStopDetecting)
            {
                StopDetecting();
            }
            else
            {
                SwitchState(DETECT_CHARACTER_STATE_ENUMS.FoundState);
            }
        }
    }
}
