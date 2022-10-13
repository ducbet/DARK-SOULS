using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TMD
{
    public class PickUpable : Interactable
    {
        public override void Interact(Command command)
        {
            base.Interact(command);
            command.Execute();
            Destroy(instanceRoot);
        }

        public override string GetPopupMessage()
        {
            return "Pick up " + itemObject.GetItemName();
        }
    }
}
