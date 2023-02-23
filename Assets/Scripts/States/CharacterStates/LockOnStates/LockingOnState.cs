using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TMD
{
    public class LockingOnState : State
    {
        private LockOnStateMachine lockOnStateMachine;

        private Transform objTranform = null;  // camera tranform if current object is player. else are object transform
        private GameObject nearestObject = null;  // use to ignore locked on object when switch left and right
        private float spherecastThickness = 2f;
        private float lockOnMaxDistance = 5f;
        public LayerMask lockableOnLayers;

        public LockingOnState(LockOnStateMachine lockOnStateMachine, Transform cameraTranform = null)
        {
            this.lockOnStateMachine = lockOnStateMachine;
            if (lockableOnLayers == 0)
            {
                lockableOnLayers = (int)(CameraManager.LayerMasks.Enemy);
            }

            if (cameraTranform != null)
            {
                this.objTranform = cameraTranform;
            }
            else
            {
                this.objTranform = lockOnStateMachine.transform;
            }
        }
        public override void Enter()
        {
            base.Enter();
            Debug.Log("LockingOnState: Enter");
            GetNearestObject();
            //Debug.Log("shortestDistance: " + nearestDistance);
            if (nearestObject == null)
            {
                lockOnStateMachine.SwitchState(LockOnStateMachine.LOCK_ON_STATE_ENUMS.LockingOff);
                return;
            }
            PlayerLockOnTarget();
            
        }

        private void PlayerLockOnTarget()
        {
            if (nearestObject == null)
            {
                return;
            }
            if (lockOnStateMachine.GetType() == typeof(PlayerLockOnStateMachine))
            {
                ((PlayerLockOnStateMachine)lockOnStateMachine).cameraManager.lockOnTarget = GetLockOnPointTransform();
            }
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
            RaycastHit[] hits = Physics.SphereCastAll(lockOnStateMachine.transform.position, spherecastThickness, objTranform.forward, lockOnMaxDistance, lockableOnLayers);
            float nearestDistance = Mathf.Infinity;
            GameObject _nearestObject = null;
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
                if (left && IsVectorLeftSide(hit.collider.transform.position - lockOnStateMachine.transform.position, objTranform))
                {
                    nearestDistance = hit.distance;
                    _nearestObject = hit.collider.gameObject;
                }
                else if (right && IsVectorRightSide(hit.collider.transform.position - lockOnStateMachine.transform.position, objTranform))
                {
                    nearestDistance = hit.distance;
                    _nearestObject = hit.collider.gameObject;
                }
                else if (!left && !right)
                {
                    nearestDistance = hit.distance;
                    _nearestObject = hit.collider.gameObject;
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
                PlayerLockOnTarget();
                lockOnStateMachine.isLockOnLeftTarget = false;
            }
            if (lockOnStateMachine.isLockOnRightTarget)
            {
                GetNearestObject(right: true);
                PlayerLockOnTarget();
                lockOnStateMachine.isLockOnRightTarget = false;
            }
        }
    }
}
