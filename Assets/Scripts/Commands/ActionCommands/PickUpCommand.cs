using UnityEngine;

namespace TMD
{
    public class PickUpCommand : ActionCommand
    {
        private MonoBehaviour actionManager;  // component that contain pick up function
        private ItemObject targetItem;

        public PickUpCommand(MonoBehaviour _actionManager)
        {
            actionManager = _actionManager;
        }

        public void SetTargetItem(ItemObject _targetItem)
        {
            targetItem = _targetItem;
        }

        public override void Execute()
        {
            if (targetItem == null)
            {
                Debug.Log("PickUpCommand: targetItem is null");
            }
            if (actionManager is PlayerActionManager)
            {
                ((PlayerActionManager)actionManager).PickUpItem(targetItem);
            }
        }
    }
}
