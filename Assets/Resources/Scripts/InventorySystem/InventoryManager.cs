using Resources.Scripts.Entities;
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
    }
}
