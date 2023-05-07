using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TMD
{
    public class AttackingState : GroundedState
    {
        private string lastAttackName = "";

        public AttackingState(MovementStateMachine movementStateMachine, int stateIndex) : base(movementStateMachine, stateIndex)
        {
        }
        public override void Enter()
        {
            base.Enter();
            movementStateMachine.animatorManager.EnableRootMotion();
            movementStateMachine.canStartComboAttack = false;

            if (movementStateMachine.preState is AttackingState)
            {
                lastAttackName = ((AttackingState)movementStateMachine.preState).GetLastAttackName();
            }
            lastAttackName = GetAttackAnimation(lastAttackName);
            if (lastAttackName != "")
            {
                movementStateMachine.PlayTargetAnimation(lastAttackName);
                return;
            }
        }

        public override void Exit()
        {
            base.Exit();
            movementStateMachine.animatorManager.DisableRootMotion();
        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();
        }

        public override void LateUpdate()
        {
            base.LateUpdate();
        }

        public override void Update()
        {
            base.Update();
            if (IsStateChanged())
            {
                return;
            }
            if (movementStateMachine.animatorManager.isUsingRootMotion)
            {
                HandleRootMotionMovements(movementStateMachine.rootMotionSpeed);
            }
            if (movementStateMachine.isPlayingAnimation)
            {
                if (movementStateMachine.isLeftClick && movementStateMachine.canStartComboAttack)
                {
                    movementStateMachine.SwitchState(MovementStateMachine.MOVEMENT_STATE_ENUMS.Attacking);
                    return;
                }
            }
            else
            {
                ClearComboAttack();
                if (movementStateMachine.isLeftClick)
                {
                    movementStateMachine.SwitchState(MovementStateMachine.MOVEMENT_STATE_ENUMS.Attacking);
                    return;
                }
                else
                {
                    movementStateMachine.SwitchState(MovementStateMachine.MOVEMENT_STATE_ENUMS.Idle);
                    return;
                }
            }
        }
        public void ClearComboAttack()
        {
            lastAttackName = "";
        }
        public string GetLastAttackName()
        {
            return lastAttackName;
        }

        #region TODO: will be moved to PlayerActionStateMachine in the future
        public string GetAttackAnimation(string lastAttackName)
        {
            ItemObject leftHandItemObject = movementStateMachine.inventoryManager.GetCurrentItemObject(isRightHand: false);
            ItemObject rightHandItemObject = movementStateMachine.inventoryManager.GetCurrentItemObject();
            if (leftHandItemObject && rightHandItemObject)
            {
                if (rightHandItemObject is WeaponObject)
                {
                    return ((WeaponObject)rightHandItemObject).GetAttackAnimation(lastAttackName);
                }
            }
            else if (leftHandItemObject)
            {
                return "";
            }
            else if (rightHandItemObject)
            {
                if (rightHandItemObject is WeaponObject)
                {
                    return ((WeaponObject)rightHandItemObject).GetAttackAnimation(lastAttackName);
                }
            }
            return "";
        }
        #endregion
    }
}
