using Resources.Scripts.InventorySystem;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Resources.Scripts.UI.Inventory.Slots
{
    public class QuickAccessSlotDisplay : SlotDisplayBase, IDropHandler
    {
        public void OnDrop(PointerEventData eventData)
        {
            GameObject drag = eventData.pointerDrag;
            if (drag != null)
            {
                InventoryManager.Instance.MoveToQuickAccessSlot(drag.GetComponent<SlotDisplay>().Slot, Slot);
            }
        }
    }
}