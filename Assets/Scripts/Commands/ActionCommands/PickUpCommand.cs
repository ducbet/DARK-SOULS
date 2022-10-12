using UnityEngine;

namespace TMD
{
    public class PickUpCommand : ActionCommand
    {
        private MonoBehaviour actionManager;  // component that contain pick up function

        public PickUpCommand(MonoBehaviour _actionManager)
        {
            actionManager = _actionManager;
        }

        public void Execute(ItemObject item)
        {
            if (actionManager is PlayerActionManager)
            {
                ((PlayerActionManager)actionManager).PickUpItem(item);
            }
        }
    }
}
