using Resources.Scripts.Data.ItemsData;
using Resources.Scripts.InventorySystem;
using Resources.Scripts.Items;
using UnityEngine;

namespace Resources.Scripts.Entities
{
    public class PickUpItem : Entity
    {
        public SpriteRenderer SpriteRenderer { get; private set; }
        [field: SerializeField]
        public ItemType Type { get; private set; }
        [field: SerializeField]
        public ItemData Data { get; private set; }

        private void Awake()
        {
            SpriteRenderer = GetComponent<SpriteRenderer>();
        }

        public override void Interact()
        {
            InventoryManager.Instance.TryTakeItem(this);
        }
    }
}
