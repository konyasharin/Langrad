using UnityEngine;

namespace Resources.Scripts.Data.ItemsData
{
    [CreateAssetMenu(fileName = "NewHealthPotion", menuName = "Game/Items/HealthPotion")]
    public class HealthPotionData : ItemData
    {
        [field: SerializeField, Min(1)] public int HealValue { get; private set; }
    }
}
