using System;
using Resources.Scripts.Data.ItemsData;
using UnityEngine;
using UnityEngine.Serialization;

namespace Resources.Scripts.Items
{
    public abstract class Item
    {
        public Sprite Sprite;
        public ItemData ItemData { get; }

        protected Item(ItemData itemData)
        {
            ItemData = itemData;
        }

        public override bool Equals(object obj)
        {
            if (obj is Item item) return item.ItemData.name == ItemData.name;
            return false;
        }

        public override int GetHashCode()
        {
            return ItemData.name.GetHashCode();
        }

        public abstract void Use();

        public virtual bool IsActivationAvailable()
        {
            return true;
        }
    }
}
