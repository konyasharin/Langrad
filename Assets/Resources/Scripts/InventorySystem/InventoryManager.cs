using Resources.Scripts.Entities;
using Resources.Scripts.ServiceLocatorSystem;
using UnityEngine;

namespace Resources.Scripts.InventorySystem
{
    public class InventoryManager : MonoBehaviour, IService
    {
        public void TryTakeItem(PickUpItem pickUpItem)
        {
            if (!QuickAccessInventory.Instance.IsFull())
            {
                QuickAccessInventory.Instance.TryTakeItem(pickUpItem);
                Destroy(pickUpItem.gameObject);
            } 
            else if (!Inventory.Instance.IsFull())
            {
                Inventory.Instance.TryTakeItem(pickUpItem);
                Destroy(pickUpItem.gameObject);
            }
        }

        public void MoveToQuickAccessSlot(InventorySlot from, InventorySlot to)
        {
            foreach (var slot in Inventory.Instance.Slots)
            {
                if (slot == from)
                {
                    foreach (var quickAccessSlot in QuickAccessInventory.Instance.Slots)
                    {
                        if (quickAccessSlot == to)
                        {
                            (slot.Item, quickAccessSlot.Item) = (quickAccessSlot.Item, slot.Item);
                            Inventory.Instance.OnChangeInventory.Invoke();
                            QuickAccessInventory.Instance.OnChangeInventory.Invoke();
                            return;
                        }
                    }
                }
            }
        }
    }
}
