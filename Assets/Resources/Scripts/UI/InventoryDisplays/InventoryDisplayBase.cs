using Resources.Scripts.InventorySystem;
using Resources.Scripts.ServiceLocatorSystem;
using Resources.Scripts.UI.InventoryDisplays.Slots;
using UnityEngine;

namespace Resources.Scripts.UI.InventoryDisplays
{
    public abstract class InventoryDisplayBase<TS, TI> : MonoBehaviour 
        where TS : SlotDisplayBase
        where TI : Inventory
    {
        protected TS[] SlotsDisplays { get; set; }
        protected TI Inventory { get; set; }

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
    }
}