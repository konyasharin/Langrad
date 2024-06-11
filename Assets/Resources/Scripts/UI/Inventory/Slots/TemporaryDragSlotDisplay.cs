using Resources.Scripts.InventorySystem;
using UnityEngine;
using UnityEngine.UI;

namespace Resources.Scripts.UI.Inventory.Slots
{
    public class TemporaryDragSlotDisplay : SlotDisplayBase
    {
        public static TemporaryDragSlotDisplay Instance;
        private RectTransform _rectTransform;

        private void Awake()
        {
            Instance = this;
            _rectTransform = GetComponent<RectTransform>();
            Hide();
        }

        public void Show(Vector2 position, InventorySlot slot)
        {
            gameObject.SetActive(true);
            _rectTransform.anchoredPosition = position;
            if (slot.Item == null)
            {
                Clear();
            }
            else
            {
                Slot = slot;
                Fill();
            }
        }

        public void ChangeRectTransform(Vector2 delta)
        {
            _rectTransform.anchoredPosition += delta;
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}