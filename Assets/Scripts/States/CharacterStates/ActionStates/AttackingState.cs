using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TMD
{
    public class AttackingState : ActionState
    {
        private string lastAttackName = "";

        public AttackingState(ActionStateMachine actionStateMachine, int stateIndex) : base(actionStateMachine, stateIndex)
        {
        }
        public override void Enter()
        {
            base.Enter();
            actionStateMachine.animatorManager.EnableRootMotion();
            actionStateMachine.canStartComboAttack = false;

            if (actionStateMachine.preState is AttackingState)
            {
                lastAttackName = ((AttackingState)actionStateMachine.preState).GetLastAttackName();
            }
            lastAttackName = GetAttackAnimation(lastAttackName);
            if (lastAttackName != "")
            {
                actionStateMachine.PlayTargetAnimation(lastAttackName);
                return;
            }
        }

        public override void Exit()
        {
            base.Exit();
            actionStateMachine.animatorManager.DisableRootMotion();
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
            if (actionStateMachine.animatorManager.isUsingRootMotion)
            {
                HandleRootMotionMovements(actionStateMachine.rootMotionSpeed);
            }
            if (actionStateMachine.isPlayingAnimation)
            {
                if (actionStateMachine.isLeftClick && actionStateMachine.canStartComboAttack)
                {
                    actionStateMachine.SwitchState(ActionStateMachine.ACTION_STATE_ENUMS.Attacking);
                    return;
                }
            }
            else
            {
                ClearComboAttack();
                if (actionStateMachine.isLeftClick)
                {
                    actionStateMachine.SwitchState(ActionStateMachine.ACTION_STATE_ENUMS.Attacking);
                    return;
                }
                else
                {
                    actionStateMachine.SwitchState(ActionStateMachine.ACTION_STATE_ENUMS.Empty);
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

        public string GetAttackAnimation(string lastAttackName)
        {
            ItemObject leftHandItemObject = actionStateMachine.inventoryManager.GetCurrentItemObject(isRightHand: false);
            ItemObject rightHandItemObject = actionStateMachine.inventoryManager.GetCurrentItemObject();
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
    }
}
