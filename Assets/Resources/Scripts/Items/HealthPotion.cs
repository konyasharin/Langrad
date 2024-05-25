using Resources.Scripts.Actors.Player;
using Resources.Scripts.ItemsData;

namespace Resources.Scripts.Items
{
    public class HealthPotion : Item
    {
        private readonly HealthPotionData _data;

        public HealthPotion(HealthPotionData data)
        {
            _data = data;
        }
        
        public override void Use()
        {
            PlayerCharacter.Instance.Heal(_data.HealValue);
        }
    }
}