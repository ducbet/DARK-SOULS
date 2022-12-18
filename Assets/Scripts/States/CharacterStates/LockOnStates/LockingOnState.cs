using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TMD
{
    public class LockingOnState : State
    {
        private LockOnStateMachine lockOnStateMachine;

        private float spherecastThickness = 1f;
        private float lockOnMaxDistance = 5f;
        public LayerMask lockableOnLayers;

        public LockingOnState(LockOnStateMachine lockOnStateMachine)
        {
            this.lockOnStateMachine = lockOnStateMachine;
            if (lockableOnLayers == 0)
            {
                lockableOnLayers = (int)(CameraManager.LayerMasks.Enemy);
            }
        }
        public override void Enter()
        {
            base.Enter();
            Debug.Log("LockingOnState: Enter");
            RaycastHit[] hits = Physics.SphereCastAll(lockOnStateMachine.transform.position, spherecastThickness, lockOnStateMachine.transform.forward, lockOnMaxDistance, lockableOnLayers);
            float nearestDistance = Mathf.Infinity;
            GameObject nearestObject = null;
            foreach (RaycastHit hit in hits)
            {
                if (hit.distance < nearestDistance)
                {
                    nearestDistance = hit.distance;
                    nearestObject = hit.collider.gameObject;
                }
            }
            //Debug.Log("shortestDistance: " + nearestDistance);
            if (nearestObject == null)
            {
                lockOnStateMachine.SwitchState(LockOnStateMachine.LOCK_ON_STATE_ENUMS.LockingOff);
                return;
            }
            if (lockOnStateMachine.GetType() == typeof(PlayerLockOnStateMachine))
            {
                ((PlayerLockOnStateMachine)lockOnStateMachine).cameraManager.lockOnTarget = GetLockOnPointTransform(nearestObject);
            }
        }

        private Transform GetLockOnPointTransform(GameObject nearestObject)
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
        }
    }
}
