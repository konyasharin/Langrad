using Resources.Scripts.InventorySystem;
using Resources.Scripts.ServiceLocatorSystem;
using Resources.Scripts.UI.InventoryDisplays.Slots;
using UnityEngine;

namespace Resources.Scripts.UI.InventoryDisplays
{
    public class QuickAccessInventoryDisplay : InventoryDisplayBase<QuickAccessSlotDisplay, QuickAccessInventory>
    {
        [SerializeField] private QuickAccessSlotDisplay[] slotsDisplays; 
        
        public void Initialize()
        {
            SlotsDisplays = slotsDisplays;
            Inventory = ServiceLocator.Instance.Get<InventoryManager>().QuickAccessInventory;
            InitializeHandle();
        }
    }
}
