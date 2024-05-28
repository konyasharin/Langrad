using Resources.Scripts.InventorySystem;
using UnityEngine;
using UnityEngine.UI;

namespace Resources.Scripts.UI.Inventory
{
    public class QuickAccessInventoryDisplay : InventoryDisplayBase
    {
        public static QuickAccessInventoryDisplay Instance;
        [field: SerializeField]
        protected override SlotDisplay[] SlotsDisplays { get; set; }
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
