using Resources.Scripts.Entities;
using Resources.Scripts.Items;
using UnityEngine;
using UnityEngine.Events;

namespace Resources.Scripts.InventorySystem
{
    public abstract class InventoryBase : MonoBehaviour
    {
        public InventorySlot[] Slots { get; private set; }
        public abstract int CountSlots { get; protected set; }
        public UnityEvent OnChangeInventory { get; private set; } = new UnityEvent();

        protected virtual void Awake()
        {
            Slots = new InventorySlot[CountSlots];
            for (int i = 0; i < Slots.Length; i++)
            {
                Slots[i] = new InventorySlot();
            }
        }
        
        public bool IsFull()
        {
            foreach (var slot in Slots)
            {
                if (slot.Item == null)
                {
                    return false;
                }
            }

            return true;
        }

        protected void CleanSlot(int slotIndex)
        {
            if (slotIndex < 0 || slotIndex >= CountSlots)
            {
                Debug.LogWarning("Slot number doesn't exist");
                return;
            }
            
            Slots[slotIndex].Item = null;
            OnChangeInventory.Invoke();
        }
        
        public void TryTakeItem(PickUpItem pickUpItem)
        {
            ItemsFactory factory = new ItemsFactory();
            foreach (var slot in Slots)
            {
                if (!slot.IsBusy())
                {
                    slot.Item = factory.CreateItem(pickUpItem);
                    OnChangeInventory.Invoke();
                    return;
                }
            }
        }
    }
}
