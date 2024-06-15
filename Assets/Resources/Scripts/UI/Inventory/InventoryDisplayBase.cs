using System;
using Resources.Scripts.InventorySystem;
using Resources.Scripts.UI.Inventory.Slots;
using UnityEngine;
using UnityEngine.UI;

namespace Resources.Scripts.UI.Inventory
{
    public abstract class InventoryDisplayBase<T> : MonoBehaviour where T : SlotDisplayBase
    {
        protected abstract T[] SlotsDisplays { get; set; }
        protected abstract InventoryBase Inventory { get; set; }

        protected void InitializeHandle()
        {
            if (SlotsDisplays.Length != Inventory.CountSlots)
            {
                Debug.LogWarning("Count of elements in itemPlaces array not equal" +
                                 " count slots in quick access inventory");
            }
            AttachSlots();
            UpdateInventoryDisplay();
            Inventory.OnChangeInventory.AddListener(UpdateInventoryDisplay);
        }

        private void AttachSlots()
        {
            for (int i = 0; i < SlotsDisplays.Length; i++)
            {
                SlotsDisplays[i].Slot = Inventory.Slots[i];
            }
        }

        private void UpdateInventoryDisplay()
        {
            for (int i = 0; i < SlotsDisplays.Length; i++)
            {
                if (Inventory.Slots[i].IsBusy())
                {
                    SlotsDisplays[i].Fill();
                }
                else
                {
                    SlotsDisplays[i].Clear();
                }
            }
        }

        public abstract void Initialize();
    }
}