using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TMD
{
    public class LockingOnState : State
    {
        private LockOnStateMachine lockOnStateMachine;

        private Transform objTranform = null;  // camera tranform if current object is player. else are object selfLockOnPoint
        private GameObject nearestObject = null;  // use to ignore locked on object when switch left and right
        private float spherecastThickness = 2f;
        private float lockOnMaxDistance = 10f;
        public LayerMask lockableOnLayers;
        public LayerMask groundCheckLayers;


        public LockingOnState(LockOnStateMachine lockOnStateMachine, Transform cameraTranform = null)
        {
            this.lockOnStateMachine = lockOnStateMachine;
            if (lockableOnLayers == 0)
            {
                lockableOnLayers = (int)(CameraManager.LayerMasks.Character);
            }
            if (groundCheckLayers == 0)
            {
                groundCheckLayers = (int)CameraManager.LayerMasks.Ground;
            }

            if (cameraTranform != null)
            {
                this.objTranform = cameraTranform;
            }
            else
            {
                this.objTranform = lockOnStateMachine.selfLockOnPoint;
            }
        }
        public override void Enter()
        {
            base.Enter();
            lockOnStateMachine.isLockingOn = true;
            GetNearestObject();
            if (nearestObject == null)
            {
                lockOnStateMachine.SwitchState(LockOnStateMachine.LOCK_ON_STATE_ENUMS.LockingOff);
                return;
            }
            LockOnTarget();
            lockOnStateMachine.StartValidatingTarget();
        }
        public override void Exit()
        {
            base.Exit();
            lockOnStateMachine.lockOnTarget = null;
            lockOnStateMachine.isLockingOn = false;
            lockOnStateMachine.StopValidatingTarget();
        }

        private void LockOnTarget()
        {
            if (nearestObject == null)
            {
                return;
            }
            lockOnStateMachine.lockOnTarget = GetLockOnPointTransform();
        }

        private bool IsTargetBlocked(Transform target)
        {
            // check whether there is any obstacle blocking the target
            Transform targetLockOnPoint = target.Find("LockOnPoint");
            Vector3 targetPosition = targetLockOnPoint != null ? targetLockOnPoint.position : target.position;

            RaycastHit hit;
            if (Physics.Linecast(lockOnStateMachine.selfLockOnPoint.position, targetPosition, out hit, groundCheckLayers))
            {
                return true;
            }
            return false;
        }

        private bool IsTargetTooFar(Transform target)
        {
            // check whether the target is too far from self (when player locked and ran away,...)
            return Vector3.Distance(target.position, lockOnStateMachine.selfLockOnPoint.position) > lockOnMaxDistance;
        }
        private bool IsSelf(Transform target)
        {
            // target can be LockOnPoint or the parent target. check LockOnPoint first
            if (target.gameObject == lockOnStateMachine.gameObject)
            {
                return true;
            }
            return target.parent.gameObject == lockOnStateMachine.gameObject;
        }

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
            return true;
        }

        private int GetVectorLeftRightSide(Vector3 targetVector, Vector3 rightVector)
        {
            float dot_result = Vector3.Dot(targetVector, rightVector);
            if (dot_result > 0f)
            {
                return 1;  // right
            }
            else if (dot_result < 0f)
            {
                return -1;  // left
            }
            else
            {
                return 0;  // forward or backward
            }
        }

        private bool IsVectorRightSide(Vector3 targetVector, Transform objTransform)
        {
            if (GetVectorLeftRightSide(targetVector, objTransform.right) == 1) return true;
            return false;
        }

        private bool IsVectorLeftSide(Vector3 targetVector, Transform objTransform)
        {
            if (GetVectorLeftRightSide(targetVector, objTransform.right) == -1) return true;
            return false;
        }

        private GameObject GetNearestObject(bool left = false, bool right = false)
        {
            RaycastHit[] hits = Physics.SphereCastAll(lockOnStateMachine.selfLockOnPoint.position, spherecastThickness, objTranform.forward, lockOnMaxDistance, lockableOnLayers);
            float nearestDistance = Mathf.Infinity;
            GameObject _nearestObject = null;

            void SelectNearestDistance(RaycastHit hit)
            {
                if (IsTargetBlocked(hit.collider.transform))
                {
                    return;
                }
                nearestDistance = hit.distance;
                _nearestObject = hit.collider.gameObject;
            }

            foreach (RaycastHit hit in hits)
            {
                if (hit.collider.gameObject == this.nearestObject)
                {
                    continue;
                }
                if (hit.distance >= nearestDistance)
                {
                    continue;
                }
                if (left && IsVectorLeftSide(hit.collider.transform.position - lockOnStateMachine.selfLockOnPoint.position, objTranform))
                {
                    SelectNearestDistance(hit);
                }
                else if (right && IsVectorRightSide(hit.collider.transform.position - lockOnStateMachine.selfLockOnPoint.position, objTranform))
                {
                    SelectNearestDistance(hit);
                }
                else if (!left && !right)
                {
                    SelectNearestDistance(hit);
                }
            }
            this.nearestObject = _nearestObject;
            return _nearestObject;
        }

        private Transform GetLockOnPointTransform()
        {
            // Get LockOn point in transform
            LockableOn lockOnComponent = nearestObject.GetComponentInChildren<LockableOn>();
            if (lockOnComponent != null)
            {
                return lockOnComponent.transform;
            }
            else
            {
                return nearestObject.transform;
            }
        }

        public override void FixedUpdate()
        {
        }

        public override void LateUpdate()
        {
        }

        public override void Update()
        {
            if (lockOnStateMachine.isLockOnLeftTarget)
            {
                GetNearestObject(left: true);
                LockOnTarget();
                lockOnStateMachine.isLockOnLeftTarget = false;
            }
            if (lockOnStateMachine.isLockOnRightTarget)
            {
                GetNearestObject(right: true);
                LockOnTarget();
                lockOnStateMachine.isLockOnRightTarget = false;
            }
        }
    }
}
