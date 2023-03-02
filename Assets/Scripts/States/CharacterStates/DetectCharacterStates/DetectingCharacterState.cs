using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TMD
{
    public class DetectingCharacterState : State
    {
        private DetectCharacterStateMachine detectCharacterStateMachine;
        private int detectRange = 20;
        private int viewAngle = 50;
        public LayerMask characterLayer;
        public LayerMask groundCheckLayers;

        public DetectingCharacterState(DetectCharacterStateMachine detectCharacterStateMachine)
        {
            this.detectCharacterStateMachine = detectCharacterStateMachine;
            if (characterLayer == 0)
            {
                characterLayer = (int)CameraManager.LayerMasks.Character;
            }
            if (groundCheckLayers == 0)
            {
                groundCheckLayers = (int)CameraManager.LayerMasks.Ground;
            }
        }

        public override void Enter()
        {
            base.Enter();
            detectCharacterStateMachine.StartDetectingTarget();
        }

        public override void Exit()
        {
            base.Exit();
            detectCharacterStateMachine.StopDetectingTarget();
        }

        public GameObject DetectTarget()
        {
            Collider[] colliders = Physics.OverlapSphere(detectCharacterStateMachine.transform.position, detectRange, characterLayer);
            foreach (var collider in colliders)
            {
                GameObject character = collider.gameObject;
                if (IsTargetValid(character.transform))
                {
                    detectCharacterStateMachine.foundTarget = character.transform;
                    return character;
                }
            }
            return null;
        }

        // make shared funtion later
        public bool IsTargetValid(Transform target)
        {
            if (IsSelf(target))
            {
                return false;
            }
            if (IsTargetTooFar(target))
            {
                return false;
            }
            if (IsTargetBlocked(target))
            {
                return false;
            }
            if (IsTargetOutsidePOV(target))
            {
                return false;
            }
            return true;
        }

        private bool IsSelf(Transform target)
        {
            return target.gameObject == detectCharacterStateMachine.gameObject;
        }

        private bool IsTargetBlocked(Transform target)
        {
            // check whether there is any obstacle blocking the target
            Transform targetLockOnPoint = target.Find("LockOnPoint");
            Vector3 targetPosition = targetLockOnPoint != null ? targetLockOnPoint.position : target.position;

            RaycastHit hit;
            if (Physics.Linecast(detectCharacterStateMachine.transform.position, targetPosition, out hit, groundCheckLayers))
            {
                return true;
            }
            return false;
        }
        private bool IsTargetTooFar(Transform target)
        {
            // check whether the target is too far
            return Vector3.Distance(target.position, detectCharacterStateMachine.transform.position) > detectRange;
        }
        private bool IsTargetOutsidePOV(Transform target)
        {
            // check whether the target is in view angle
            Vector3 targetDirection = target.position - detectCharacterStateMachine.transform.position;
            float angle = Vector3.Angle(targetDirection, detectCharacterStateMachine.transform.forward);
            return angle < -viewAngle || viewAngle < angle;
        }
        
        public override void FixedUpdate()
        {
        }

        public override void LateUpdate()
        {
        }

        public override void Update()
        {
            
        }
    }
}
