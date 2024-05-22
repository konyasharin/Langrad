using Resources.Scripts.InventorySystem;
using Resources.Scripts.Items;
using UnityEngine;

namespace Resources.Scripts.Entities
{
    public class PickUpItem : Entity
    {
        public SpriteRenderer SpriteRenderer { get; private set; }
        [field: SerializeField]
        public Item Item { get; private set; }

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
