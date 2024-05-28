using System;
using Resources.Scripts.InventorySystem;
using UnityEngine;
using UnityEngine.UI;

namespace Resources.Scripts.UI.Inventory
{
    public abstract class InventoryDisplayBase : MonoBehaviour
    {
        protected abstract SlotDisplay[] SlotsDisplays { get; set; }
        protected abstract InventoryBase Inventory { get; set; }

        protected void InitializeHandle()
        {
            if (SlotsDisplays.Length != Inventory.CountSlots)
            {
                Debug.LogWarning("Count of elements in itemPlaces array not equal" +
                                 " count slots in quick access inventory");
            }
            Inventory.OnChangeInventory.AddListener(UpdateInventoryDisplay);
        }

        private void UpdateInventoryDisplay()
        {
            for (int i = 0; i < SlotsDisplays.Length; i++)
            {
                if (Inventory.Slots[i].IsBusy())
                {
                    SlotsDisplays[i].Fill(Inventory.Slots[i].Item.Sprite);
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