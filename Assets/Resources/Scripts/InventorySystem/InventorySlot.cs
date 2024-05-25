using Resources.Scripts.Entities;
using Resources.Scripts.Items;
using Resources.Scripts.ItemsData;
using UnityEngine;

namespace Resources.Scripts.InventorySystem
{
    public class InventorySlot
    {
        public Item Item;

        public bool IsBusy()
        {
            if (Item == null)
            {
                return false;
            }

            return true;
        }
    }
}
