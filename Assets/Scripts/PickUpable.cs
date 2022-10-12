using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TMD
{
    public class PickUpable : Interactable
    {
        public override ItemObject Interact(Command command)
        {
            base.Interact(command);
            ((PickUpCommand) command).Execute(itemObject);
            return itemObject;
        }
    }
}
