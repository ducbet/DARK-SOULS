using System;
using System.Collections;

namespace TMD
{
    public class EnemyActionStateMachine : ActionStateMachine
    {
        protected override void Awake()
        {
            base.Awake();
        }

        protected override void Start()
        {
            base.Start();
        }
        protected override void OnDestroy()
        {
            base.OnDestroy();
        }
        public override void SwitchState(Enum stateEnum)
        {
            base.SwitchState(stateEnum);
            if ((ACTION_STATE_ENUMS)stateEnum == ACTION_STATE_ENUMS.Laned)
            {
                HandleEnemyLanded();
            }
        }

        public void HandleEnemyLanded()
        {
            ((EnemyMovementStateMachine)movementStateMachine).navMeshAgent.CompleteOffMeshLink();
            ((EnemyMovementStateMachine)movementStateMachine).navMeshAgent.nextPosition = transform.position;
        }

        public void SetIsJumpingPerformed()
        {
            StartCoroutine(StartJumpingCoroutine());
        }

        public IEnumerator StartJumpingCoroutine()
        {
            isJumping = true;
            yield return null;
            isJumping = false;
        }
    }
}
