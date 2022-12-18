using UnityEngine;

namespace TMD
{
    public class PickingUpState : GroundedState
    {
        private string pickUpAnimationName = "Pick Up";
        private int pickUpAnimation;

        public PickingUpState(MovementStateMachine movementStateMachine) : base(movementStateMachine)
        {
            pickUpAnimation = base.movementStateMachine.animatorManager.HashString(pickUpAnimationName);
        }
        public override void Enter()
        {
            base.Enter();
            movementStateMachine.animatorManager.EnableRootMotion();
            movementStateMachine.PlayTargetAnimation(pickUpAnimation);

            StartPickUpCommand();
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
                HandleRootMotionMovements(movementStateMachine.rollingVelocityScale);
            }
            if (!movementStateMachine.isPlayingAnimation)
            {
                movementStateMachine.SwitchState(MovementStateMachine.MOVEMENT_STATE_ENUMS.Idle);
                return;
            }
        }

        private void StartPickUpCommand()
        {
            if (movementStateMachine.interactableItem == null)
            {
                return;
            }
            Interactable interactableComponent = movementStateMachine.interactableItem.GetComponent<Interactable>();
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
            movementStateMachine.inventoryManager.AddItemToInventory(item);
            movementStateMachine.interactablePopup.Hide();
        }

        public void StopMovingXZ()
        {
            movementStateMachine.rgBody.velocity = new Vector3(0, movementStateMachine.rgBody.velocity.y, 0);
        }
    }
}
