using System;
using UnityEngine;

namespace Resources.Scripts.InventorySystem
{
    public class QuickAccessInventory : InventoryBase
    {
        public static QuickAccessInventory Instance;
        public override int CountSlots { get; protected set; } = 4;

        protected override void Awake()
        {
            base.Awake();
            Instance = this;
        }
    }
}
