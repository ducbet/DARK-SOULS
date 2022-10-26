using UnityEngine;

namespace TMD
{
    public class PickUpCommand : ActionCommand
    {
        private PickingUpState pickingUpState;
        private ItemObject targetItem;

        public PickUpCommand(PickingUpState pickingUpState)
        {
            this.pickingUpState = pickingUpState;
        }

        public void SetTargetItem(ItemObject targetItem)
        {
            this.targetItem = targetItem;
        }

        public override void Execute()
        {
            if (targetItem == null)
            {
                Debug.Log("PickUpCommand: targetItem is null");
            }
            pickingUpState.PickUpItem(targetItem);
        }
    }
}
