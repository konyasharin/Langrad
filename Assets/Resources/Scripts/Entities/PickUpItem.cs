using Resources.Scripts.Data.ItemsData;
using Resources.Scripts.InventorySystem;
using Resources.Scripts.Items;
using Resources.Scripts.ServiceLocatorSystem;
using UnityEngine;

namespace Resources.Scripts.Entities
{
    public class PickUpItem : Entity
    {
        [field: SerializeField] public ItemType Type { get; private set; }
        [field: SerializeField] public ItemData Data { get; private set; }
        
        public SpriteRenderer SpriteRenderer { get; private set; }

        private InventoryManager _inventoryManager;
        

        private void Awake()
        {
            SpriteRenderer = GetComponent<SpriteRenderer>();
        }

        protected override void Start()
        {
            base.Start();
            _inventoryManager = ServiceLocator.Instance.Get<InventoryManager>();
        }

        public override void Interact()
        {
            _inventoryManager.TryTakeItem(this);
        }
    }
}
