using UnityEngine;

namespace TMD
{
    public class PickingUpState : ActionState
    {
        private string pickUpAnimationName = "Pick Up";
        private int pickUpAnimation;

        public PickingUpState(ActionStateMachine actionStateMachine, int stateIndex) : base(actionStateMachine, stateIndex)
        {
            pickUpAnimation = base.actionStateMachine.animatorManager.HashString(pickUpAnimationName);
        }
        public override void Enter()
        {
            base.Enter();
            actionStateMachine.animatorManager.EnableRootMotion();
            actionStateMachine.PlayTargetAnimation(pickUpAnimation);

            StartPickUpCommand();
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
                HandleRootMotionMovements(actionStateMachine.rollingVelocityScale);
            }
            if (!actionStateMachine.isPlayingAnimation)
            {
                actionStateMachine.SwitchState(ActionStateMachine.ACTION_STATE_ENUMS.Empty);
                return;
            }
        }

        private void StartPickUpCommand()
        {
            if (actionStateMachine.interactableItem == null)
            {
                return;
            }
            Interactable interactableComponent = actionStateMachine.interactableItem.GetComponent<Interactable>();
            if (interactableComponent == null)
            {
                return;
            }
            PickUpCommand pickUpCommand = new PickUpCommand(this);
            pickUpCommand.SetTargetItem(interactableComponent.GetItem());
            interactableComponent.Interact(pickUpCommand);
        }

        public void PickUpItem(ItemObject item)
        {
            StopMovingXZ();
            actionStateMachine.inventoryManager.AddItemToInventory(item);
            actionStateMachine.interactablePopup.Hide();
        }

        public void StopMovingXZ()
        {
            actionStateMachine.rgBody.velocity = new Vector3(0, actionStateMachine.rgBody.velocity.y, 0);
        }
    }
}
