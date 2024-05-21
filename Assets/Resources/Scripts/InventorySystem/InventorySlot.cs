using Resources.Scripts.Entities.Items;

namespace Resources.Scripts.InventorySystem
{
    public class InventorySlot
    {
        public Item item;

        public bool IsBusy()
        {
            if (item == null)
            {
                return false;
            }

            return true;
        }
    
    
    }
}
