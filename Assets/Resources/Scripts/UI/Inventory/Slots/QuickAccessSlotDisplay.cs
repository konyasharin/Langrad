using UnityEngine;
using UnityEngine.EventSystems;

namespace Resources.Scripts.UI.Inventory.Slots
{
    public class QuickAccessSlotDisplay : SlotDisplayBase, IDropHandler
    {
        public void OnDrop(PointerEventData eventData)
        {
            Debug.Log(eventData.pointerDrag);
        }
    }
}