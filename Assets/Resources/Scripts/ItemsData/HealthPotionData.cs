using Resources.Scripts.Actors.Player;
using UnityEngine;

namespace Resources.Scripts.ItemsData
{
    [CreateAssetMenu(fileName = "NewHealthPotion", menuName = "Game/Items/HealthPotion")]
    public class HealthPotionData : ItemData
    {
        [field: SerializeField, Min(1)] public int HealValue { get; private set; }
    }
}
