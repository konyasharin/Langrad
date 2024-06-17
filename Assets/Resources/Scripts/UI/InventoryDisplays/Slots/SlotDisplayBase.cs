using Resources.Scripts.InventorySystem;
using UnityEngine;
using UnityEngine.UI;

namespace Resources.Scripts.UI.InventoryDisplays.Slots
{
    public abstract class SlotDisplayBase : MonoBehaviour
    {
        [field: SerializeField]
        public Image ItemPlace { get; private set; }
        public InventorySlot Slot;

        public void Clear()
        {
            ItemPlace.color = Color.clear;
            ItemPlace.sprite = null;
        }

        public void Fill()
        {
            ItemPlace.color = Color.white;
            ItemPlace.sprite = Slot.Item.Sprite;
        }
    }
}