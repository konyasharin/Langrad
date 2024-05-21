using System;
using Resources.Scripts.Entities.Items;
using UnityEngine;

namespace Resources.Scripts.InventorySystem
{
    public class InventoryManager : MonoBehaviour
    {
        public static InventoryManager Instance;

        private void Awake()
        {
            Instance = this;
        }

        public void TryTakeItem(Item item)
        {
            if (!QuickAccessInventory.Instance.IsFull())
            {
                QuickAccessInventory.Instance.TryTakeItem(item);
                Destroy(item.gameObject);
            } 
            else if (!Inventory.Instance.IsFull())
            {
                Inventory.Instance.TryTakeItem(item);
                Destroy(item.gameObject);
            }
        }
    }
}
