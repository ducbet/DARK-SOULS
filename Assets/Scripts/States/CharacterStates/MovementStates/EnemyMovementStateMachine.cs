using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TMD
{
    [RequireComponent(typeof(DetectCharacterStateMachine))]
    public class EnemyMovementStateMachine : MovementStateMachine
    {
        private DetectCharacterStateMachine detectCharacterStateMachine;
        public Transform foundTarget;

        protected override void Awake()
        {
            base.Awake();
            detectCharacterStateMachine = GetComponent<DetectCharacterStateMachine>();
            detectCharacterStateMachine.TargetFound += moveToTarget;
            detectCharacterStateMachine.TargetNotFound += stopMovingToTarget;
        }

        public void moveToTarget(object sender, Transform foundTarget)
        {
            this.foundTarget = foundTarget;
        }

        public void stopMovingToTarget(object sender, EventArgs e)
        {
            this.foundTarget = null;
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            detectCharacterStateMachine.TargetFound -= moveToTarget;
            detectCharacterStateMachine.TargetNotFound -= stopMovingToTarget;
        }

        protected override void Start()
        {
            base.Start();
        }

        protected override void Update()
        {
            base.Update();
        }
        public override void CalculateMoveDirection()
        {
            base.CalculateMoveDirection();
            
            if (foundTarget == null)
            {
                moveDirection = Vector3.zero;
                return;
            }
            Vector3 _moveDirection = foundTarget.position - transform.position;
            _moveDirection.y = 0;
            moveDirection = Vector3.Normalize(_moveDirection);
        }

        public override void CalculateMoveMagnitude()
        {
            base.CalculateMoveMagnitude();
            if (foundTarget == null)
            {
                moveMagnitude = 0;
                return;
            }
            moveMagnitude = 1;  // any number > 0 is fine
        }

        public override float GetPlayerMovementHorizontal()
        {
            return base.GetPlayerMovementHorizontal();
        }

        public override float GetPlayerMovementVertical()
        {
            if (foundTarget == null)
            {
                return 0;
            }
            return 1; // any number > 0 is fine
        }
    }
}
