using System;
using Resources.Scripts.InventorySystem;
using Resources.Scripts.ServiceLocatorSystem;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Resources.Scripts.UI.Inventory.Slots
{
    public class QuickAccessSlotDisplay : SlotDisplayBase, IDropHandler
    {
        private InventoryManager _inventoryManager;
        
        private void Start()
        {
            _inventoryManager = ServiceLocator.Instance.Get<InventoryManager>();
        }

        public void OnDrop(PointerEventData eventData)
        {
            GameObject drag = eventData.pointerDrag;
            if (drag != null)
            {
                _inventoryManager.MoveToQuickAccessSlot(drag.GetComponent<SlotDisplay>().Slot, Slot);
            }
        }
    }
}