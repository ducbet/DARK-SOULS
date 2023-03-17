using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace TMD
{
    [RequireComponent(typeof(NavMeshAgent))]
    [RequireComponent(typeof(DetectCharacterStateMachine))]
    [RequireComponent(typeof(MovementStateMachine))]
    public class EnemyMovementStateMachine : MovementStateMachine
    {
        private DetectCharacterStateMachine detectCharacterStateMachine;
        private MovementStateMachine movementStateMachine;
        public Transform foundTarget;
        private NavMeshAgent navMeshAgent;
        private IEnumerator followingTargetCoroutine = null;

        protected override void Awake()
        {
            base.Awake();
            navMeshAgent = GetComponent<NavMeshAgent>();
            navMeshAgent.updatePosition = false;
            navMeshAgent.updateRotation = false;
            //navMeshAgent.updateUpAxis = false;

            detectCharacterStateMachine = GetComponent<DetectCharacterStateMachine>();
            movementStateMachine = GetComponent<MovementStateMachine>();
            movementStateMachine.rgBody.isKinematic = false;
            //detectCharacterStateMachine.TargetFound += StartFollowingTarget;
            //detectCharacterStateMachine.TargetNotFound += StopFollowingTarget;

            movementStateMachine.CharacterLanded += HandleCharacterLanded;

            // test
            GameObject player = GameObject.Find("Player");
            StartFollowingTarget(null, player.transform);
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            detectCharacterStateMachine.TargetFound -= StartFollowingTarget;
            detectCharacterStateMachine.TargetNotFound -= StopFollowingTarget;
            movementStateMachine.CharacterLanded -= HandleCharacterLanded;
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
            moveDirection = Vector3.Normalize(navMeshAgent.desiredVelocity);
            navMeshAgent.nextPosition = transform.position;  // reset navmesh position self position (because the speed is different)
            if (navMeshAgent.isOnOffMeshLink)
            {
                StartCoroutine(StartJumpingCoroutine());
            }
        }

        public IEnumerator StartJumpingCoroutine()
        {
            isJumping = true;
            yield return null;
            isJumping = false;
        }

        public void HandleCharacterLanded(object sender, EventArgs e)
        {
            navMeshAgent.CompleteOffMeshLink();
            navMeshAgent.nextPosition = transform.position;
        }

        public override void CalculateMoveMagnitude()
        {
            base.CalculateMoveMagnitude();
            if (foundTarget == null)
            {
                moveMagnitude = 0;
                return;
            }
            // any number > 0 is fine
            moveMagnitude = (foundTarget.position - transform.position).magnitude > navMeshAgent.stoppingDistance ? 1 : 0;
        }

        public void StartFollowingTarget(object sender, Transform foundTarget)
        {
            this.foundTarget = foundTarget;
            if (foundTarget == null)
            {
                return;
                
            }
            navMeshAgent.SetDestination(foundTarget.position);
            followingTargetCoroutine = FollowTarget();
            StartCoroutine(followingTargetCoroutine);
        }
        public void StopFollowingTarget(object sender, EventArgs e)
        {
            this.foundTarget = null;
            if (followingTargetCoroutine == null)
            {
                return;
            }
            StopCoroutine(followingTargetCoroutine);
        }

        public IEnumerator FollowTarget()
        {
            while (foundTarget != null)
            {
                navMeshAgent.SetDestination(foundTarget.position);
                yield return new WaitForSeconds(0.5f);
            }
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
