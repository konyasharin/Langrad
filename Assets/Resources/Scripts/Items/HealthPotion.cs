using Resources.Scripts.Actors.Player;
using UnityEngine;

namespace Resources.Scripts.Items
{
    [CreateAssetMenu(fileName = "NewHealthPotion", menuName = "Game/Item/HealthPotion")]
    public class HealthPotion : Item
    {
        [SerializeField, Min(1)] private int healValue;
        
        public override void Use()
        {
            PlayerCharacter.Instance.Heal(healValue);
        }
    }
}
