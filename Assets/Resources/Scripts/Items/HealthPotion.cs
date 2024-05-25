using Resources.Scripts.Actors.Player;
using Resources.Scripts.ItemsData;
using UnityEngine;

namespace Resources.Scripts.Items
{
    public class HealthPotion : Item
    {
        public HealthPotionData Data { get; set; }

        public HealthPotion(HealthPotionData data)
        {
            Data = data;
        }
        
        public override void Use()
        {
            PlayerCharacter.Instance.Heal(Data.HealValue);
        }
    }
}