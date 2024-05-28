namespace Resources.Scripts.InventorySystem
{
    public class TemporaryDragSlot
    {
        private static TemporaryDragSlot _instance;
        public InventorySlot Slot;

        public static TemporaryDragSlot GetInstance()
        {
            if (_instance == null)
            {
                _instance = new TemporaryDragSlot();
            }

            return _instance;
        }
    }
}