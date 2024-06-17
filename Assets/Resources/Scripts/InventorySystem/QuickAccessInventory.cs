using System;
using Resources.Scripts.Items;
using Resources.Scripts.ServiceLocatorSystem;
using UnityEngine;

namespace Resources.Scripts.InventorySystem
{
    public class QuickAccessInventory : Inventory
    {
        public KeyCode[] KeysToUse { get; private set; } = { KeyCode.Alpha1, KeyCode.Alpha2, KeyCode.Alpha3, KeyCode.Alpha4 };
        private Inventory _inventory;

        public QuickAccessInventory() : base(4)
        {
        }

        public void Initialize()
        {
            _inventory = ServiceLocator.Instance.Get<InventoryManager>().Inventory;
        }
        
        public void HandleKeyDown(int i)
        {
            UseItem(i);
        }

        private void TryReplaceItem(int slotIndex)
        {
            InventorySlot[] slots = _inventory.Slots;
            
            for (int i = 0; i < slots.Length; i++)
            {
                if (slots[i].IsBusy() && slots[i].Item.Equals(Slots[slotIndex].Item))
                {
                    TryTakeItem(slots[i].Item);
                    _inventory.CleanSlot(i);
                    return;
                }
            }
            
            CleanSlot(slotIndex);
        }

        private void UseItem(int slotIndex)
        {
            if (slotIndex < 0 || slotIndex >= CountSlots)
            {
                Debug.LogWarning("Slot number doesn't exist");
                return;
            }
            if (Slots[slotIndex].IsBusy())
            {
                if (Slots[slotIndex].Item.IsActivationAvailable())
                {
                    Slots[slotIndex].Item.Use();
                    TryReplaceItem(slotIndex);
                }
            }
        }
    }
}
