using Resources.Scripts.Entities;
using Resources.Scripts.ItemsData;
using UnityEngine;

namespace Resources.Scripts.Items
{
    public class ItemsFactory
    {
        public Item CreateItem(PickUpItem pickUpItem)
        {
            Item item = null;
            switch (pickUpItem.Type)
            {
                case ItemType.HealthPotion:
                    item =  new HealthPotion(pickUpItem.Data as HealthPotionData);
                    break;
            }

            if (item != null)
            {
                item.Sprite = pickUpItem.SpriteRenderer.sprite;
            }
            
            return item;
        }
    }
}