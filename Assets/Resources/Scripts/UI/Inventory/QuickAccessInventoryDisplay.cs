using Resources.Scripts.InventorySystem;
using Resources.Scripts.UI.Inventory.Slots;
using UnityEngine;
using UnityEngine.UI;

namespace Resources.Scripts.UI.Inventory
{
    public class QuickAccessInventoryDisplay : InventoryDisplayBase<QuickAccessSlotDisplay>
    {
        public static QuickAccessInventoryDisplay Instance;
        [field: SerializeField]
        protected override QuickAccessSlotDisplay[] SlotsDisplays { get; set; }
        protected override InventoryBase Inventory { get; set; }

        private void Awake()
        {
            Instance = this;
        }

        public override void Initialize()
        {
            Inventory = QuickAccessInventory.Instance;
            InitializeHandle();
        }
    }
}
