using Resources.Scripts.Entities;
using Resources.Scripts.Items.MagicScrolls;
using Resources.Scripts.ItemsData;
using Resources.Scripts.ItemsData.MagicScrolls;
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
                    MSFactory factory = new MSFactory();
                    item = factory.CreateMagicScroll(pickUpItem.Data as MSData);
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