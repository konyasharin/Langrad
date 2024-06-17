using Resources.Scripts.InventorySystem;
using Resources.Scripts.UI.Inventory.Slots;
using UnityEngine;
using UnityEngine.UI;

namespace Resources.Scripts.UI.Inventory
{
    public class QuickAccessInventoryDisplay : InventoryDisplayBase<QuickAccessSlotDisplay>
    {
        [field: SerializeField]
        protected override QuickAccessSlotDisplay[] SlotsDisplays { get; set; }
        protected override InventoryBase Inventory { get; set; }

        public override void Initialize()
        {
            Inventory = QuickAccessInventory.Instance;
            InitializeHandle();
        }
    }
}
