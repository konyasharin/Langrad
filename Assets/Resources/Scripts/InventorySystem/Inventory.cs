using Resources.Scripts.Entities.Items;
using UnityEngine;

namespace Resources.Scripts.InventorySystem
{
    public class Inventory : InventoryBase
    {
        public static Inventory Instance { get; private set; }
        [field: SerializeField, Min(4)] public override int CountSlots { get; protected set; }

        protected override void Awake()
        {
            base.Awake();
            Instance = this;
        }
    }
}
