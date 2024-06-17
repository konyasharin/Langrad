using Resources.Scripts.Actors.Player;
using Resources.Scripts.Data.ItemsData;
using Resources.Scripts.ServiceLocatorSystem;

namespace Resources.Scripts.Items
{
    public class HealthPotion : Item
    {
        private readonly HealthPotionData _data;
        private readonly PlayerCharacter _player = ServiceLocator.Instance.Get<PlayerCharacter>();

        public HealthPotion(HealthPotionData data) : base(data)
        {
            _data = data;
        }
        
        public override void Use()
        {
            _player.Heal(_data.HealValue);
        }
    }
}