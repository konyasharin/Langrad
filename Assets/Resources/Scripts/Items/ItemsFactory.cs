using Resources.Scripts.Data.ItemsData;
using Resources.Scripts.Entities;
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
                case ItemType.MagicScroll:
                    item = new MagicScroll(pickUpItem.Data as MSData);
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