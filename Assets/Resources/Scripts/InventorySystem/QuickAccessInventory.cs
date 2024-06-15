using System;
using UnityEngine;

namespace Resources.Scripts.InventorySystem
{
    public class QuickAccessInventory : InventoryBase
    {
        public static QuickAccessInventory Instance;
        public override int CountSlots { get; protected set; } = 4;
        
        public KeyCode[] KeysToUse { get; private set; } = { KeyCode.Alpha1, KeyCode.Alpha2, KeyCode.Alpha3, KeyCode.Alpha4 };

        protected override void Awake()
        {
            base.Awake();
            Instance = this;
        }

        public void HandleKeyDown(int i)
        {
            UseItem(i);
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
                    CleanSlot(slotIndex);   
                }
            }
        }
    }
}
