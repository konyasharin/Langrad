using Resources.Scripts.Entities;
using Resources.Scripts.ServiceLocatorSystem;
using Resources.Scripts.UI.InventoryDisplays;
using UnityEngine;

namespace Resources.Scripts.InventorySystem
{
    public class InventoryManager : MonoBehaviour, IService
    {
        [SerializeField, Range(4, 30)] private int countInventorySlots;
        
        [field: SerializeField] public InventoryDisplay InventoryDisplay { get; private set; }
        [field: SerializeField] public QuickAccessInventoryDisplay QuickAccessInventoryDisplay { get; private set; }
        
        public QuickAccessInventory QuickAccessInventory { get; private set; }
        public Inventory Inventory { get; private set; }
        
        public void Initialize()
        {
            QuickAccessInventory = new QuickAccessInventory();
            Inventory = new Inventory(countInventorySlots);
            
            QuickAccessInventory.Initialize();
            InventoryDisplay.Initialize();
            QuickAccessInventoryDisplay.Initialize();
        }
        
        public void TryTakeItem(PickUpItem pickUpItem)
        {
            if (!QuickAccessInventory.IsFull())
            {
                QuickAccessInventory.TryTakeItem(pickUpItem);
                Destroy(pickUpItem.gameObject);
            } 
            else if (!Inventory.IsFull())
            {
                Inventory.TryTakeItem(pickUpItem);
                Destroy(pickUpItem.gameObject);
            }
        }

        public void MoveToQuickAccessSlot(InventorySlot from, InventorySlot to)
        {
            foreach (var slot in Inventory.Slots)
            {
                if (slot == from)
                {
                    foreach (var quickAccessSlot in QuickAccessInventory.Slots)
                    {
                        if (quickAccessSlot == to)
                        {
                            (slot.Item, quickAccessSlot.Item) = (quickAccessSlot.Item, slot.Item);
                            Inventory.OnChangeInventory.Invoke();
                            QuickAccessInventory.OnChangeInventory.Invoke();
                            return;
                        }
                    }
                }
            }
        }
    }
}
