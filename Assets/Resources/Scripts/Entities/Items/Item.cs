using System;
using Resources.Scripts.InventorySystem;
using UnityEngine;

namespace Resources.Scripts.Entities.Items
{
    public abstract class Item : Entity
    {
        public SpriteRenderer SpriteRenderer { get; private set; }

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
