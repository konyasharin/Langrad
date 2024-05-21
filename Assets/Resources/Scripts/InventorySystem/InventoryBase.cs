using Resources.Scripts.Entities.Items;
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
                if (slot.item == null)
                {
                    return false;
                }
            }

            return true;
        }
        
        public void TryTakeItem(Item item)
        {
            foreach (var slot in Slots)
            {
                if (!slot.IsBusy())
                {
                    slot.item = item;
                    OnChangeInventory.Invoke();
                    return;
                }
            }
        }
    }
}
